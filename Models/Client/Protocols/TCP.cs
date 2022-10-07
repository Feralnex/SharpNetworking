using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Feralnex.Networking
{
    public class TCP
    {
        public event Action<TCP, int> IdProvided;
        public event PacketReceivedHandler<TCP, Packet> PacketReceived;
        public event ConnectedHandler<TCP> Connected;
        public event BoundHandler<TCP> Bound;
        public event DisconnectedHandler<TCP> Disconnected;
        public event FailedToConnectHandler<TCP> FailedToConnect;
        public event FailedToBindHandler<TCP> FailedToBind;
        public event FailedToSendHandler<TCP> FailedToSend;
        public event FailedToReceiveHandler<TCP> FailedToReceive;

        private NetworkStream Stream { get; set; }
        private byte[] ReceiveBuffer { get; set; }
        private DataStream ReceivedData { get; set; }
        private int TCPBufferSize { get; set; }

        public TcpClient Socket { get; private set; }
        public bool IsReceiving { get; private set; }
        public bool IsConnected => Socket.Connected;

        public TCP(int tcpBufferSize)
        {
            Socket = new TcpClient()
            {
                ReceiveBufferSize = tcpBufferSize,
                SendBufferSize = tcpBufferSize
            };
            ReceiveBuffer = new byte[tcpBufferSize];
            ReceivedData = new DataStream();
            TCPBufferSize = tcpBufferSize;
            IsReceiving = false;
        }

        public void Connect(IPAddress address, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(address, port);

            try
            {
                Socket.BeginConnect(address, port, ConnectCallback, endPoint);
            }
            catch (Exception exception)
            {
                FailedToConnect?.Invoke(this, endPoint, exception);
            }
        }

        public void Bind(Socket socket)
        {
            Socket.Client = socket;

            try
            {
                Stream = Socket.GetStream();
            }
            catch (Exception exception)
            {
                Socket.Client = null;

                FailedToBind?.Invoke(this, socket, exception);

                return;
            }

            try
            {
                BeginReceive();
            }
            catch (Exception exception)
            {
                OnFailedToReceive(exception);
            }

            Bound?.Invoke(this, Socket.Client);
        }

        public void Send(Packet packet)
        {
            try
            {
                if (IsConnected)
                {
                    Stream.BeginWrite(packet.ToArray(), 0, packet.Length, SendCallback, packet);
                }
            }
            catch (Exception exception)
            {
                FailedToSend?.Invoke(this, packet, exception);
            }
        }

        public void Disconnect()
        {
            Socket.Close();

            Socket = new TcpClient()
            {
                ReceiveBufferSize = TCPBufferSize,
                SendBufferSize = TCPBufferSize
            };
            Stream = null;
            ReceiveBuffer.Clear();
            ReceivedData.Clear();
            IsReceiving = false;

            Disconnected?.Invoke(this);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            IPEndPoint endPoint = result.AsyncState as IPEndPoint;

            try
            {
                Socket.EndConnect(result);
            }
            catch (Exception exception)
            {
                FailedToConnect?.Invoke(this, endPoint, exception);

                return;
            }

            Stream = Socket.GetStream();

            BeginReceive();

            Connected?.Invoke(this, Socket.Client);
        }

        private void SendCallback(IAsyncResult result)
        {
            Packet packet = result.AsyncState as Packet;

            try
            {
                Stream.EndWrite(result);
            }
            catch (Exception exception)
            {
                FailedToSend?.Invoke(this, packet, exception);
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                if (!IsConnected) return;

                int receivedBytesLength = Stream.EndRead(result);

                if (receivedBytesLength <= 0)
                {
                    Disconnect();

                    return;
                }

                byte[] receivedBytes = new byte[receivedBytesLength];

                ReceiveBuffer.CopyTo(receivedBytes, 0, receivedBytesLength);
                Stream.BeginRead(ReceiveBuffer, 0, ReceiveBuffer.Length, ReceiveCallback, null);

                HandleData(receivedBytes);
            }
            catch (IOException)
            {
                Disconnect();
            }
            catch (Exception exception)
            {
                OnFailedToReceive(exception);
            }
        }

        private void HandleData(byte[] receivedBytes)
        {
            ReceivedData.AddData(receivedBytes);

            while (ReceivedData.ContainsPacket())
            {
                if (ReceivedData.TryExtractPacket(out Packet packet))
                {
                    switch (packet.Id)
                    {
                        case ServerPacket.Id:
                            if (packet.TryRead(out int id)) IdProvided?.Invoke(this, packet.ReadInt());

                            break;
                        default:
                            PacketReceived?.Invoke(this, packet);

                            break;
                    }
                }
            }

            if (!ReceivedData.ContainsPartialData()) ReceivedData.Clear();
        }

        private void BeginReceive()
        {
            Stream.BeginRead(ReceiveBuffer, 0, ReceiveBuffer.Length, ReceiveCallback, null);

            IsReceiving = true;
        }

        private void OnFailedToReceive(Exception exception)
        {
            FailedToReceive?.Invoke(this, exception);

            IsReceiving = false;
        }
    }
}
