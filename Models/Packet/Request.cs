using System;

namespace Feralnex.Networking
{
    public class Request : Packet
    {
        public long Timestamp { get; private set; }
        public long Latency { get; private set; }

        public Request(Enum senderId, Enum packetId) : base(PacketType.Request, senderId, packetId)
        {
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            Latency = default;

            Write(Timestamp);
        }

        public Request(int packetLength, byte[] data) : base(packetLength, data)
        {
            Timestamp = ReadLong();
            Latency = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - Timestamp;
        }

        public Request(Packet packet) : base(packet)
        {
            Timestamp = packet.ReadLong();
            Latency = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - Timestamp;
        }
    }
}
