using System;
using System.Net;

namespace Feralnex.Networking
{
    public class UDP
    {
        public event PacketReceivedHandler<UDP, Packet> PacketReceived;
        public event FailedToSendHandler<UDP> FailedToSend;
        public event FailedToReceiveHandler<UDP> FailedToReceive;

        private IPEndPoint EndPoint { get; set; }

        public UdpListener Listener { get; private set; }
        public bool IsConnected => EndPoint != null;

        public UDP(UdpListener udpListener)
        {
            Listener = udpListener;
        }

        public void Bind(IPEndPoint endPoint)
        {
            if (EndPoint != null) RemoveEventListeners();

            EndPoint = endPoint;

            AddEventListeners();
        }

        public void Bind(IPEndPoint localEndPoint, IPEndPoint remoteEndPoint)
        {
            if (EndPoint != null) RemoveEventListeners();

            Listener.Bind(localEndPoint);
            EndPoint = remoteEndPoint;

            AddEventListeners();
        }

        public void Send(Packet packet)
        {
            if (IsConnected)
            {
                UdpPacketState packetState = new UdpPacketState(EndPoint, packet);

                Listener.BeginSend(packet.ToArray(), packet.Length, EndPoint, SendCallback, packetState);
            }
        }

        public void Disconnect()
        {
            RemoveEventListeners();

            EndPoint = null;
        }

        private void AddEventListeners()
        {
            Listener.PacketReceived[EndPoint] += OnPacketReceived;
            Listener.FailedToSend[EndPoint] += OnFailedToSend;
            Listener.FailedToReceive[EndPoint] += OnFailedToReceive;
        }

        private void RemoveEventListeners()
        {
            Listener.PacketReceived[EndPoint] -= OnPacketReceived;
            Listener.FailedToSend[EndPoint] -= OnFailedToSend;
            Listener.FailedToReceive[EndPoint] -= OnFailedToReceive;
        }

        private void SendCallback(IAsyncResult result)
        {
            UdpPacketState packetState = result.AsyncState as UdpPacketState;

            try
            {
                Listener.Socket.EndSend(result);
            }
            catch (Exception exception)
            {
                OnFailedToSend(packetState.EndPoint, packetState.Packet, exception);
            }
        }

        private void OnPacketReceived(object sender, Packet packet)
        {
            PacketReceived?.Invoke(this, packet);
        }

        private void OnFailedToSend(IPEndPoint endPoint, Packet packet, Exception exception)
        {
            FailedToSend?.Invoke(this, packet, exception);
        }

        private void OnFailedToReceive(Exception exception)
        {
            FailedToReceive?.Invoke(this, exception);
        }
    }
}
