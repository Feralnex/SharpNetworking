using System;
using System.Numerics;

namespace Feralnex.Networking
{
    public abstract partial class Packet : IDisposable
    {
        public void Write(byte value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(byte[] value)
        {
            int bytesCount = Buffer.AddRange(value);

            ContentLength += bytesCount;
        }

        public void Write(bool value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(ushort value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(short value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(uint value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(int value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(ulong value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(long value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(float value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(double value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(BigInteger value)
        {
            Write(value.ToByteArray());
        }

        public void Write(string value)
        {
            int bytesCount = Buffer.Add(value);

            ContentLength += bytesCount;
        }

        public void Write(Enum value)
        {
            Type type = value.GetType();

            Write(type.Name);
            Write(value.ToString());
        }

        public void Write<T>(T value) where T: Enum
        {
            Write(value.ToString());
        }

        public void Write(DateTime value)
        {
            Write(value.Year);
            Write(value.Month);
            Write(value.Day);
            Write(value.Hour);
            Write(value.Minute);
            Write(value.Second);
            Write(value.Millisecond);
            Write(value.Kind);
        }
    }
}
