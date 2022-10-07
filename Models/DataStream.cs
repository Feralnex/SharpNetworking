namespace Feralnex.Networking
{
    public class DataStream
    {
        private ByteBuffer _buffer;

        public int Length => _buffer.Length;
        public int UnreadLength => Length - _buffer.ReadPosition;

        public DataStream()
        {
            _buffer = new ByteBuffer();
        }

        public DataStream(byte[] bytes) : this()
        {
            AddData(bytes);
        }

        #region Functions
        public void AddData(byte[] data)
        {
            _buffer.AddRange(data);
        }

        public bool ContainsPacket()
        {
            if (UnreadLength < Consts.INT32_LENGTH_IN_BYTES) return false;

            int packetLength = _buffer.GetInt32(false);

            if (packetLength > UnreadLength) return false;

            return true;
        }

        public bool ContainsPartialData()
        {
            if (UnreadLength >= Consts.INT32_LENGTH_IN_BYTES)
            {
                int packetLength = _buffer.GetInt32(false);

                if (packetLength > UnreadLength) return true;
            }

            if (UnreadLength > 0) return true;

            return false;
        }

        public bool TryExtractPacket(out Packet packet)
        {
            bool packetExtracted = Packet.TryCreateFromBytes(_buffer, out packet);

            _buffer.RemoveRange(0, _buffer.ReadPosition);

            return packetExtracted;
        }

        public void Clear()
        {
            _buffer.Clear();
        }
        #endregion
    }
}
