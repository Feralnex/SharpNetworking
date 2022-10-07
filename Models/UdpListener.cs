using System;
using System.Net;
using System.Net.Sockets;

namespace Feralnex.Networking
{
    public class UdpListener
    {
        public Event<IPEndPoint, PacketReceivedHandler<IPEndPoint, Packet>> PacketReceived;
        public Event<IPEndPoint, FailedToSendHandler<IPEndPoint>> FailedToSend;
        public Event<IPEndPoint, FailedToReceiveHandler> FailedToReceive;

        public UdpClient Socket { get; private set; }

        public bool IsReceiving { get; private set; }
        public IPEndPoint LocalEndPoint => Socket.Client.LocalEndPoint as IPEndPoint;
        public IPEndPoint RemoteEndPoint => Socket.Client.RemoteEndPoint as IPEndPoint;

        public UdpListener()
        {
            Socket = new UdpClient(0);
        }

        public UdpListener(int port)
        {
            Socket = new UdpClient(port);
        }

        public void Start()
        {
            IsReceiving = true;

            Socket.BeginReceive(ReceiveCallback, null);
        }

        public void Stop()
        {
            IsReceiving = false;

            Socket.Close();
        }

        public void Bind(int port)
        {
            Stop();

            Socket = new UdpClient(port);

            Start();
        }

        public void Bind(IPEndPoint endPoint)
        {
            Stop();

            Socket = new UdpClient(endPoint);

            Start();
        }

        public void Send(IPEndPoint endPoint, Packet packet)
        {
            if (endPoint != null)
            {
                UdpPacketState packetState = new UdpPacketState(endPoint, packet);

                Socket.BeginSend(packet.ToArray(), packet.Length, endPoint, SendCallback, packetState);
            }
        }

        public IAsyncResult BeginSend(byte[] datagram, int bytes, IPEndPoint endPoint, AsyncCallback requestCallback, object state)
        {
            return Socket.BeginSend(datagram, bytes, endPoint, requestCallback, state);
        }

        private void SendCallback(IAsyncResult result)
        {
            UdpPacketState packetState = result.AsyncState as UdpPacketState;

            try
            {
                Socket.EndSend(result);
            }
            catch (Exception exception)
            {
                if (FailedToSend.TryGet(packetState.EndPoint, out FailedToSendHandler<IPEndPoint> handler))
                {
                    handler?.Invoke(packetState.EndPoint, packetState.Packet, exception);
                }
                else FailedToSend.Handler?.Invoke(packetState.EndPoint, packetState.Packet, exception);
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                if (!IsReceiving) return;

                IPEndPoint client = new IPEndPoint(IPAddress.Any, 0);
                byte[] packetContent = Socket.EndReceive(result, ref client);
                DataStream dataStream = new DataStream(packetContent);

                Socket.BeginReceive(ReceiveCallback, null);

                while (dataStream.ContainsPacket())
                {
                    if (dataStream.TryExtractPacket(out Packet packet))
                    {
                        if (PacketReceived.TryGet(client, out PacketReceivedHandler<IPEndPoint, Packet> handler))
                        {
                            handler?.Invoke(client, packet);
                        }
                        else PacketReceived.Handler?.Invoke(client, packet);
                    }
                }
            }
            catch (Exception exception)
            {
                OnFailedToReceive(exception);
            }
        }

        private void OnFailedToReceive(Exception exception)
        {
            FailedToReceive.Handler?.Invoke(exception);

            IsReceiving = false;
        }
    }
}
