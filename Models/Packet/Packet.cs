using System;

namespace Feralnex.Networking
{
    public abstract partial class Packet : IDisposable
    {
        private ByteBuffer _buffer;
        private int _contentLength;

        public ByteBuffer Buffer => _buffer;
        public int ContentLength
        {
            get => _contentLength;
            private set
            {
                _contentLength = value;

                UpdateLength(_contentLength);
            }
        }
        public int Length => _buffer.Length;
        public int UnreadLength => _buffer.UnreadLength;
        public PacketType PacketType { get; private set; }
        public Enum SenderId { get; private set; }
        public Enum Id { get; private set; }

        public Packet(PacketType packetType, Enum senderId, Enum packetId)
        {
            InitializeBuffer();
            InitializeLength();

            Write(packetType);
            Write(senderId);
            Write(packetId);

            PacketType = packetType;
            SenderId = senderId;
            Id = packetId;
        }

        public Packet(int packetLength, byte[] data)
        {
            InitializeBuffer();
            InitializeLength(packetLength);

            Buffer.AddRange(data);

            PacketType = ReadPacketType();
            SenderId = ReadEnum();
            Id = ReadEnum();
        }

        public Packet(Packet packet)
        {
            _buffer = new ByteBuffer(packet._buffer);

            ReadInfo();
        }

        #region Functions
        private void InitializeBuffer()
        {
            _buffer = new ByteBuffer();
        }

        private void InitializeLength()
        {
            Buffer.Insert(0, 0);
        }

        private void InitializeLength(int length)
        {
            Buffer.Insert(0, length);
        }

        private void UpdateLength(int length)
        {
            Buffer.RemoveInt32(0);
            Buffer.Insert(0, length);
        }

        public byte[] ToArray()
        {
            return Buffer.ToArray();
        }

        public override string ToString()
        {
            return string.Format(Consts.PACKET_SCHEMA, PacketType, SenderId, Id);
        }

        public static bool TryCreateFromBytes(ByteBuffer byteBuffer, out Packet packet)
        {
            packet = default;

            try
            {
                int packetLength = byteBuffer.GetInt32();
                PacketType packetType = (PacketType)byteBuffer.GetInt32(false);
                byte[] packetDataContent = byteBuffer.GetRange(packetLength).ToArray();

                if (packetType == PacketType.Request) packet = new Request(packetLength, packetDataContent);
                else packet = new Response(packetLength, packetDataContent);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region IDisposable
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _buffer = default;
                    _contentLength = default;

                    PacketType = default;
                    SenderId = default;
                    Id = default;
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
