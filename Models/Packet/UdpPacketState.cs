using System.Net;

namespace Feralnex.Networking
{
    class UdpPacketState
    {
        public IPEndPoint EndPoint { get; private set; }
        public Packet Packet { get; private set; }

        public UdpPacketState(IPEndPoint endPoint, Packet packet)
        {
            EndPoint = endPoint;
            Packet = packet;
        }
    }
}
