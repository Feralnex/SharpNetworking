using System;

namespace Feralnex.Networking
{
    public class Response : Packet
    {
        public long RequestTimestamp { get; private set; }
        public Enum RequestId { get; private set; }
        public long Latency { get; private set; }

        public Response(Request request, Enum senderId, Enum packetId) : base(PacketType.Response, senderId, packetId)
        {
            RequestTimestamp = request.Timestamp;
            RequestId = request.Id;
            Latency = default;

            Write(RequestTimestamp);
            Write(RequestId);
        }

        public Response(int packetLength, byte[] data) : base(packetLength, data)
        {
            RequestTimestamp = ReadLong();
            RequestId = ReadEnum();
            Latency = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - RequestTimestamp;
        }

        public Response(Packet packet) : base(packet)
        {
            RequestTimestamp = ReadLong();
            RequestId = ReadEnum();
            Latency = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - RequestTimestamp;
        }
    }
}