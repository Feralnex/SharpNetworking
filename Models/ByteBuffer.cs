using System;
using System.Collections.Generic;

namespace Feralnex.Networking
{
    public class ByteBuffer
    {
        private readonly List<byte> _bytes;
        private int _writePosition;
        private int _readPosition;
        private bool _inBigEndian;

        public List<byte> Bytes => _bytes;
        public int WritePosition
        {
            get => _writePosition;
            set
            {
                if (value < 0 || value > Length) throw new IndexOutOfRangeException();

                _writePosition = value;
            }
        }
        public int ReadPosition
        {
            get => _readPosition;
            set
            {
                if (value < 0 || value > Length) throw new IndexOutOfRangeException();

                _readPosition = value;
            }
        }
        public bool InBigEndian => _inBigEndian;

        public int UnreadLength => Length - _readPosition;
        public int Length => _bytes.Count;

        public byte this[int index] => _bytes[index];

        public ByteBuffer(bool inBigEndian = false)
        {
            _bytes = new List<byte>();
            _writePosition = 0;
            _readPosition = 0;
            _inBigEndian = inBigEndian;
        }

        public ByteBuffer(ICollection<byte> bytes, bool inBigEndian = false) : this(inBigEndian)
        {
            AddRange(bytes);
        }

        public ByteBuffer(ByteBuffer bytes)
        {
            _bytes = new List<byte>();
            _writePosition = bytes.WritePosition;
            _readPosition = bytes.ReadPosition;
            _inBigEndian = bytes.InBigEndian;

            Bytes.AddRange(bytes.Bytes);
        }

        public int Add(byte value, bool moveWritePosition = true)
        {
            Bytes.Insert(WritePosition, value);
            if (moveWritePosition) WritePosition += Consts.BYTE_LENGTH_IN_BYTES;

            return Consts.BYTE_LENGTH_IN_BYTES;
        }

        public int Add(bool value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(WritePosition, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Add(ushort value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(WritePosition, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Add(short value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(WritePosition, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Add(uint value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(WritePosition, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Add(int value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(WritePosition, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Add(ulong value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(WritePosition, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Add(long value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(WritePosition, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Add(float value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(WritePosition, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Add(double value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(WritePosition, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Add(string value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);
            byte[] encodedValueLength = encodedValue.Length.ToBytes(InBigEndian);

            Bytes.InsertRange(WritePosition, encodedValueLength);
            Bytes.InsertRange(WritePosition, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length + encodedValueLength.Length;

            return encodedValue.Length + encodedValueLength.Length;
        }

        public int AddRange(ICollection<byte> values, bool moveWritePosition = true)
        {
            Bytes.InsertRange(WritePosition, values);
            if (moveWritePosition) WritePosition += values.Count;

            return values.Count;
        }

        public int AddRange(ICollection<byte> values, int length, bool moveWritePosition = true)
        {
            int count = 0;

            foreach (byte value in values)
            {
                Insert(WritePosition, value, moveWritePosition);

                count++;

                if (count == length) break;
            }

            return length;
        }

        public int AddRange(ICollection<byte> values, int startIndex, int length, bool moveWritePosition = true)
        {
            int count = 0;

            foreach (byte value in values)
            {
                if (count >= startIndex) Insert(WritePosition, value, moveWritePosition);

                count++;

                if (count - startIndex == length) break;
            }

            return length;
        }

        public int Insert(int index, byte value, bool moveWritePosition = true)
        {
            Bytes.Insert(index, value);
            if (moveWritePosition) WritePosition += Consts.BYTE_LENGTH_IN_BYTES;

            return Consts.BYTE_LENGTH_IN_BYTES;
        }

        public int Insert(int index, bool value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(index, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Insert(int index, ushort value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(index, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Insert(int index, short value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(index, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Insert(int index, uint value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(index, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Insert(int index, int value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(index, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Insert(int index, ulong value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(index, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Insert(int index, long value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(index, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Insert(int index, float value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(index, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Insert(int index, double value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);

            Bytes.InsertRange(index, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length;

            return encodedValue.Length;
        }

        public int Insert(int index, string value, bool moveWritePosition = true)
        {
            byte[] encodedValue = value.ToBytes(InBigEndian);
            byte[] encodedValueLength = encodedValue.Length.ToBytes();

            Bytes.InsertRange(index, encodedValueLength);
            Bytes.InsertRange(index + encodedValueLength.Length, encodedValue);
            if (moveWritePosition) WritePosition += encodedValue.Length + encodedValueLength.Length;

            return encodedValue.Length + encodedValueLength.Length;
        }

        public int InsertRange(int index, ICollection<byte> values, bool moveWritePosition = true)
        {
            Bytes.InsertRange(index, values);
            if (moveWritePosition) WritePosition += values.Count;

            return values.Count;
        }

        public int InsertRange(int index, ICollection<byte> values, int length, bool moveWritePosition = true)
        {
            int count = 0;

            foreach (byte value in values)
            {
                Insert(index++, value, moveWritePosition);

                count++;

                if (count == length) break;
            }

            return length;
        }

        public int InsertRange(int index, ICollection<byte> values, int startIndex, int length, bool moveWritePosition = true)
        {
            int count = 0;

            foreach (byte value in values)
            {
                if (count >= startIndex) Insert(index++, value, moveWritePosition);

                count++;

                if (count - startIndex == length) break;
            }

            return length;
        }

        public int Remove(int index)
        {
            Bytes.RemoveAt(index);

            if (WritePosition - Consts.BYTE_LENGTH_IN_BYTES > 0) WritePosition -= Consts.BYTE_LENGTH_IN_BYTES;
            if (ReadPosition - Consts.BYTE_LENGTH_IN_BYTES > 0) ReadPosition -= Consts.BYTE_LENGTH_IN_BYTES;

            return Consts.BYTE_LENGTH_IN_BYTES;
        }

        public int RemoveBool(int index)
        {
            Bytes.RemoveRange(index, Consts.BOOL_LENGTH_IN_BYTES);

            if (WritePosition - Consts.BOOL_LENGTH_IN_BYTES > 0) WritePosition -= Consts.BOOL_LENGTH_IN_BYTES;
            if (ReadPosition - Consts.BOOL_LENGTH_IN_BYTES > 0) ReadPosition -= Consts.BOOL_LENGTH_IN_BYTES;

            return Consts.BOOL_LENGTH_IN_BYTES;
        }

        public int RemoveUInt16(int index)
        {
            Bytes.RemoveRange(index, Consts.INT16_LENGTH_IN_BYTES);

            if (WritePosition - Consts.INT16_LENGTH_IN_BYTES > 0) WritePosition -= Consts.INT16_LENGTH_IN_BYTES;
            if (ReadPosition - Consts.INT16_LENGTH_IN_BYTES > 0) ReadPosition -= Consts.INT16_LENGTH_IN_BYTES;

            return Consts.INT16_LENGTH_IN_BYTES;
        }

        public int RemoveInt16(int index)
        {
            Bytes.RemoveRange(index, Consts.INT16_LENGTH_IN_BYTES);

            if (WritePosition - Consts.INT16_LENGTH_IN_BYTES > 0) WritePosition -= Consts.INT16_LENGTH_IN_BYTES;
            if (ReadPosition - Consts.INT16_LENGTH_IN_BYTES > 0) ReadPosition -= Consts.INT16_LENGTH_IN_BYTES;

            return Consts.INT16_LENGTH_IN_BYTES;
        }

        public int RemoveUInt32(int index)
        {
            Bytes.RemoveRange(index, Consts.INT32_LENGTH_IN_BYTES);

            if (WritePosition - Consts.INT32_LENGTH_IN_BYTES > 0) WritePosition -= Consts.INT32_LENGTH_IN_BYTES;
            if (ReadPosition - Consts.INT32_LENGTH_IN_BYTES > 0) ReadPosition -= Consts.INT32_LENGTH_IN_BYTES;

            return Consts.INT32_LENGTH_IN_BYTES;
        }

        public int RemoveInt32(int index)
        {
            Bytes.RemoveRange(index, Consts.INT32_LENGTH_IN_BYTES);

            if (WritePosition - Consts.INT32_LENGTH_IN_BYTES > 0) WritePosition -= Consts.INT32_LENGTH_IN_BYTES;
            if (ReadPosition - Consts.INT32_LENGTH_IN_BYTES > 0) ReadPosition -= Consts.INT32_LENGTH_IN_BYTES;

            return Consts.INT32_LENGTH_IN_BYTES;
        }

        public int RemoveUInt64(int index)
        {
            Bytes.RemoveRange(index, Consts.INT64_LENGTH_IN_BYTES);

            if (WritePosition - Consts.INT64_LENGTH_IN_BYTES > 0) WritePosition -= Consts.INT64_LENGTH_IN_BYTES;
            if (ReadPosition - Consts.INT64_LENGTH_IN_BYTES > 0) ReadPosition -= Consts.INT64_LENGTH_IN_BYTES;

            return Consts.INT64_LENGTH_IN_BYTES;
        }

        public int RemoveInt64(int index)
        {
            Bytes.RemoveRange(index, Consts.INT64_LENGTH_IN_BYTES);

            if (WritePosition - Consts.INT64_LENGTH_IN_BYTES > 0) WritePosition -= Consts.INT64_LENGTH_IN_BYTES;
            if (ReadPosition - Consts.INT64_LENGTH_IN_BYTES > 0) ReadPosition -= Consts.INT64_LENGTH_IN_BYTES;

            return Consts.INT64_LENGTH_IN_BYTES;
        }

        public int RemoveFloat(int index)
        {
            Bytes.RemoveRange(index, Consts.FLOAT_LENGTH_IN_BYTES);

            if (WritePosition - Consts.FLOAT_LENGTH_IN_BYTES > 0) WritePosition -= Consts.FLOAT_LENGTH_IN_BYTES;
            if (ReadPosition - Consts.FLOAT_LENGTH_IN_BYTES > 0) ReadPosition -= Consts.FLOAT_LENGTH_IN_BYTES;

            return Consts.FLOAT_LENGTH_IN_BYTES;
        }

        public int RemoveDouble(int index)
        {
            Bytes.RemoveRange(index, Consts.DOUBLE_LENGTH_IN_BYTES);

            if (WritePosition - Consts.DOUBLE_LENGTH_IN_BYTES > 0) WritePosition -= Consts.DOUBLE_LENGTH_IN_BYTES;
            if (ReadPosition - Consts.DOUBLE_LENGTH_IN_BYTES > 0) ReadPosition -= Consts.DOUBLE_LENGTH_IN_BYTES;

            return Consts.DOUBLE_LENGTH_IN_BYTES;
        }

        public int RemoveRange(int index, int count)
        {
            Bytes.RemoveRange(index, count);

            if (WritePosition - count > 0) WritePosition -= count;
            if (ReadPosition - count > 0) ReadPosition -= count;

            return count;
        }

        public byte Get(bool moveReadPosition = true)
        {
            byte value = Bytes[ReadPosition];

            if (moveReadPosition) ReadPosition += Consts.BYTE_LENGTH_IN_BYTES;

            return value;
        }

        public bool GetBool(bool moveReadPosition = true)
        {
            bool value = Bytes.ToBoolean(ReadPosition, InBigEndian);

            if (moveReadPosition) ReadPosition += Consts.BOOL_LENGTH_IN_BYTES;

            return value;
        }

        public ushort GetUInt16(bool moveReadPosition = true)
        {
            ushort value = Bytes.ToUInt16(ReadPosition, InBigEndian);

            if (moveReadPosition) ReadPosition += Consts.INT16_LENGTH_IN_BYTES;

            return value;
        }

        public short GetInt16(bool moveReadPosition = true)
        {
            short value = Bytes.ToInt16(ReadPosition, InBigEndian);

            if (moveReadPosition) ReadPosition += Consts.INT16_LENGTH_IN_BYTES;

            return value;
        }

        public uint GetUInt32(bool moveReadPosition = true)
        {
            uint value = Bytes.ToUInt32(ReadPosition, InBigEndian);

            if (moveReadPosition) ReadPosition += Consts.INT32_LENGTH_IN_BYTES;

            return value;
        }

        public int GetInt32(bool moveReadPosition = true)
        {
            int value = Bytes.ToInt32(ReadPosition, InBigEndian);

            if (moveReadPosition) ReadPosition += Consts.INT32_LENGTH_IN_BYTES;

            return value;
        }

        public ulong GetUInt64(bool moveReadPosition = true)
        {
            ulong value = Bytes.ToUInt64(ReadPosition, InBigEndian);

            if (moveReadPosition) ReadPosition += Consts.INT64_LENGTH_IN_BYTES;

            return value;
        }

        public long GetInt64(bool moveReadPosition = true)
        {
            long value = Bytes.ToInt64(ReadPosition, InBigEndian);

            if (moveReadPosition) ReadPosition += Consts.INT64_LENGTH_IN_BYTES;

            return value;
        }

        public float GetFloat(bool moveReadPosition = true)
        {
            float value = Bytes.ToFloat(ReadPosition, InBigEndian);

            if (moveReadPosition) ReadPosition += Consts.FLOAT_LENGTH_IN_BYTES;

            return value;
        }

        public double GetDouble(bool moveReadPosition = true)
        {
            double value = Bytes.ToDouble(ReadPosition, InBigEndian);

            if (moveReadPosition) ReadPosition += Consts.DOUBLE_LENGTH_IN_BYTES;

            return value;
        }

        public string GetString(int length, bool moveReadPosition = true)
        {
            string value = Bytes.ToString(ReadPosition, length, InBigEndian);

            if (moveReadPosition) ReadPosition += length;

            return value;
        }

        public List<byte> GetRange(int count, bool moveReadPosition = true)
        {
            List<byte> value = Bytes.GetRange(ReadPosition, count);

            if (moveReadPosition) ReadPosition += count;

            return value;
        }

        public void Clear()
        {
            Bytes.Clear();

            ReadPosition = 0;
            WritePosition = 0;
        }

        public byte[] ToArray()
        {
            return Bytes.ToArray();
        }
    }
}
