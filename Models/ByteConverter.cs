using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Feralnex.Networking
{
    public static class ByteConverter
    {
        public static byte[] ToBytes(this bool value, bool inBigEndian = false)
        {
            if (inBigEndian && BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();

            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this ushort value, bool inBigEndian = false)
        {
            if (inBigEndian && BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();

            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this short value, bool inBigEndian = false)
        {
            if (inBigEndian && BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();

            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this uint value, bool inBigEndian = false)
        {
            if (inBigEndian && BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();

            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this int value, bool inBigEndian = false)
        {
            if (inBigEndian && BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();

            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this long value, bool inBigEndian = false)
        {
            if (inBigEndian && BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();

            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this ulong value, bool inBigEndian = false)
        {
            if (inBigEndian && BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();

            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this float value, bool inBigEndian = false)
        {
            if (inBigEndian && BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();

            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this double value, bool inBigEndian = false)
        {
            if (inBigEndian && BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) return BitConverter.GetBytes(value).Reverse();

            return BitConverter.GetBytes(value);
        }

        public static byte[] ToBytes(this BigInteger value, bool inBigEndian = false)
        {
            if (inBigEndian && BitConverter.IsLittleEndian) return value.ToByteArray().Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) return value.ToByteArray().Reverse();

            return value.ToByteArray();
        }

        public static byte[] ToBytes(this string value, bool inBigEndian = false)
        {
            byte[] encodedValue = Encoding.UTF8.GetBytes(value);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return encodedValue;
        }

        public static bool ToBoolean(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, Consts.BOOL_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToBoolean(encodedValue, 0);
        }

        public static bool ToBoolean(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, Consts.BOOL_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToBoolean(encodedValue, 0);
        }

        public static bool ToBoolean(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, Consts.BOOL_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToBoolean(encodedValue, 0);
        }

        public static bool ToBoolean(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, Consts.BOOL_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToBoolean(encodedValue, 0);
        }

        public static ushort ToUInt16(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, Consts.INT16_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt16(encodedValue, 0);
        }

        public static ushort ToUInt16(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, Consts.INT16_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt16(encodedValue, 0);
        }

        public static ushort ToUInt16(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, Consts.INT16_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt16(encodedValue, 0);
        }

        public static ushort ToUInt16(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, Consts.INT16_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt16(encodedValue, 0);
        }

        public static short ToInt16(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, Consts.INT16_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt16(encodedValue, 0);
        }

        public static short ToInt16(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, Consts.INT16_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt16(encodedValue, 0);
        }

        public static short ToInt16(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, Consts.INT16_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt16(encodedValue, 0);
        }

        public static short ToInt16(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, Consts.INT16_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt16(encodedValue, 0);
        }

        public static uint ToUInt32(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, Consts.INT32_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt32(encodedValue, 0);
        }

        public static uint ToUInt32(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, Consts.INT32_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt32(encodedValue, 0);
        }

        public static uint ToUInt32(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, Consts.INT32_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt32(encodedValue, 0);
        }

        public static uint ToUInt32(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, Consts.INT32_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt32(encodedValue, 0);
        }

        public static int ToInt32(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, Consts.INT32_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt32(encodedValue, 0);
        }

        public static int ToInt32(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, Consts.INT32_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt32(encodedValue, 0);
        }

        public static int ToInt32(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, Consts.INT32_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt32(encodedValue, 0);
        }

        public static int ToInt32(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, Consts.INT32_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt32(encodedValue, 0);
        }

        public static ulong ToUInt64(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, Consts.INT64_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt64(encodedValue, 0);
        }

        public static ulong ToUInt64(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, Consts.INT64_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt64(encodedValue, 0);
        }

        public static ulong ToUInt64(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, Consts.INT64_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt64(encodedValue, 0);
        }

        public static ulong ToUInt64(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, Consts.INT64_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToUInt64(encodedValue, 0);
        }

        public static long ToInt64(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, Consts.INT64_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt64(encodedValue, 0);
        }

        public static long ToInt64(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, Consts.INT64_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt64(encodedValue, 0);
        }

        public static long ToInt64(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, Consts.INT64_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt64(encodedValue, 0);
        }

        public static long ToInt64(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, Consts.INT64_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToInt64(encodedValue, 0);
        }

        public static float ToFloat(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, Consts.FLOAT_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToSingle(encodedValue, 0);
        }

        public static float ToFloat(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, Consts.FLOAT_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToSingle(encodedValue, 0);
        }

        public static float ToFloat(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, Consts.FLOAT_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToSingle(encodedValue, 0);
        }

        public static float ToFloat(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, Consts.FLOAT_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToSingle(encodedValue, 0);
        }

        public static double ToDouble(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, Consts.DOUBLE_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToDouble(encodedValue, 0);
        }

        public static double ToDouble(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, Consts.DOUBLE_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToDouble(encodedValue, 0);
        }

        public static double ToDouble(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, Consts.DOUBLE_LENGTH_IN_BYTES);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToDouble(encodedValue, 0);
        }

        public static double ToDouble(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, Consts.DOUBLE_LENGTH_IN_BYTES).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return BitConverter.ToDouble(encodedValue, 0);
        }

        public static BigInteger ToBigInteger(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, bytes.Length);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return new BigInteger(encodedValue);
        }

        public static BigInteger ToBigInteger(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, bytes.Count).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return new BigInteger(encodedValue);
        }

        public static BigInteger ToBigInteger(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, bytes.Length);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return new BigInteger(encodedValue);
        }

        public static BigInteger ToBigInteger(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, bytes.Count).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return new BigInteger(encodedValue);
        }

        public static BigInteger ToBigInteger(this byte[] bytes, int startIndex, int length, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, length);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return new BigInteger(encodedValue);
        }

        public static BigInteger ToBigInteger(this List<byte> bytes, int startIndex, int length, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, length).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return new BigInteger(encodedValue);
        }

        public static string ToString(this byte[] bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(0, bytes.Length);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return Encoding.UTF8.GetString(encodedValue);
        }

        public static string ToString(this List<byte> bytes, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(0, bytes.Count).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return Encoding.UTF8.GetString(encodedValue);
        }

        public static string ToString(this byte[] bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, bytes.Length);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return Encoding.UTF8.GetString(encodedValue);
        }

        public static string ToString(this List<byte> bytes, int startIndex, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, bytes.Count).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return Encoding.UTF8.GetString(encodedValue);
        }

        public static string ToString(this byte[] bytes, int startIndex, int length, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.SubArray(startIndex, length);

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return Encoding.UTF8.GetString(encodedValue);
        }

        public static string ToString(this List<byte> bytes, int startIndex, int length, bool inBigEndian = false)
        {
            byte[] encodedValue = bytes.GetRange(startIndex, length).ToArray();

            if (inBigEndian && BitConverter.IsLittleEndian) encodedValue.Reverse();
            else if (!inBigEndian && !BitConverter.IsLittleEndian) encodedValue.Reverse();

            return Encoding.UTF8.GetString(encodedValue);
        }
    }
}
