using System;
using System.Numerics;

namespace Feralnex.Networking
{
    public abstract partial class Packet : IDisposable
    {
        private void ReadInfo()
        {
            try
            {
                ContentLength = ReadInt();
                PacketType = ReadPacketType();
                SenderId = ReadEnum();
                Id = ReadEnum();
            }
            catch (MissingDataException)
            {
                throw new IncompletePacketException();
            }
        }

        public byte ReadByte()
        {
            if (UnreadLength >= Consts.BYTE_LENGTH_IN_BYTES)
            {
                byte value = Buffer.Get();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out byte value)
        {
            value = default;

            try
            {
                value = ReadByte();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public byte[] ReadBytes()
        {
            if (!TryRead(out int length)) throw new MissingDataException();

            if (UnreadLength >= length)
            {
                byte[] value = Buffer.GetRange(length).ToArray();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out byte[] value)
        {
            value = default;

            try
            {
                value = ReadBytes();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public bool ReadBool()
        {
            if (UnreadLength >= Consts.BOOL_LENGTH_IN_BYTES)
            {
                bool value = Buffer.GetBool();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out bool value)
        {
            value = default;

            try
            {
                value = ReadBool();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public ushort ReadUShort()
        {
            if (UnreadLength >= Consts.INT16_LENGTH_IN_BYTES)
            {
                ushort value = Buffer.GetUInt16();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out ushort value)
        {
            value = default;

            try
            {
                value = ReadUShort();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public short ReadShort()
        {
            if (UnreadLength >= Consts.INT16_LENGTH_IN_BYTES)
            {
                short value = Buffer.GetInt16();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out short value)
        {
            value = default;

            try
            {
                value = ReadShort();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public uint ReadUInt()
        {
            if (UnreadLength >= Consts.INT32_LENGTH_IN_BYTES)
            {
                uint value = Buffer.GetUInt32();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out uint value)
        {
            value = default;

            try
            {
                value = ReadUInt();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public int ReadInt()
        {
            if (UnreadLength >= Consts.INT32_LENGTH_IN_BYTES)
            {
                int value = Buffer.GetInt32();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out int value)
        {
            value = default;

            try
            {
                value = ReadInt();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public ulong ReadULong()
        {
            if (UnreadLength >= Consts.INT64_LENGTH_IN_BYTES)
            {
                ulong value = Buffer.GetUInt64();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out ulong value)
        {
            value = default;

            try
            {
                value = ReadULong();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public long ReadLong()
        {
            if (UnreadLength >= Consts.INT64_LENGTH_IN_BYTES)
            {
                long value = Buffer.GetInt64();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out long value)
        {
            value = default;

            try
            {
                value = ReadLong();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public float ReadFloat()
        {
            if (UnreadLength >= Consts.FLOAT_LENGTH_IN_BYTES)
            {
                float value = Buffer.GetFloat();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out float value)
        {
            value = default;

            try
            {
                value = ReadFloat();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public double ReadDouble()
        {
            if (UnreadLength >= Consts.DOUBLE_LENGTH_IN_BYTES)
            {
                double value = Buffer.GetDouble();

                return value;
            }
            else
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out double value)
        {
            value = default;

            try
            {
                value = ReadDouble();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public BigInteger ReadBigInteger()
        {
            byte[] bytes = ReadBytes();
            BigInteger value = new BigInteger(bytes);

            return value;
        }

        public bool TryRead(out BigInteger value)
        {
            value = default;

            try
            {
                value = ReadBigInteger();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public string ReadString()
        {
            try
            {
                int length = ReadInt();
                string value = Buffer.GetString(length);

                return value;
            }
            catch
            {
                throw new MissingDataException();
            }
        }

        public bool TryRead(out string value)
        {
            value = default;

            try
            {
                value = ReadString();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public Enum ReadEnum()
        {
            string name = ReadString();
            string value = ReadString();
            Type type = EnumUtils.GetType(name);

            return Enum.Parse(type, value) as Enum;
        }

        public bool TryRead(out Enum value)
        {
            value = default;

            try
            {
                value = ReadEnum();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public T ReadEnum<T>() where T: Enum
        {
            string value = ReadString();
            Type type = typeof(T);

            return (T)Enum.Parse(type, value);
        }

        public bool TryRead<T>(out T value) where T : Enum
        {
            value = default;

            try
            {
                value = ReadEnum<T>();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public PacketType ReadPacketType()
        {
            return (PacketType)ReadInt();
        }

        public bool TryRead(out PacketType value)
        {
            value = default;

            try
            {
                value = ReadPacketType();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public DateTimeKind ReadDateTimeKind()
        {
            return (DateTimeKind)ReadInt();
        }

        public bool TryRead(out DateTimeKind value)
        {
            value = default;

            try
            {
                value = ReadDateTimeKind();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }

        public DateTime ReadDateTime()
        {
            int year = ReadInt();
            int month = ReadInt();
            int day = ReadInt();
            int hour = ReadInt();
            int minute = ReadInt();
            int second = ReadInt();
            int millisecond = ReadInt();
            DateTimeKind dateTimeKind = ReadDateTimeKind();
            DateTime dateTime = new DateTime(year, month, day, hour, minute, second, millisecond, dateTimeKind);

            return dateTime;
        }

        public bool TryRead(out DateTime value)
        {
            value = default;

            try
            {
                value = ReadDateTime();

                return true;
            }
            catch (MissingDataException)
            {
                return false;
            }
        }
    }
}
