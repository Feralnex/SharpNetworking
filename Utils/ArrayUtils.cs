using System;

namespace Feralnex.Networking
{
    public static class ArrayUtils
    {
        public static T[] Reverse<T>(this T[] array)
        {
            Array.Reverse(array);

            return array;
        }

        public static void CopyTo<T>(this T[] sourceArray, T[] destinationArray, int destinationIndex, int length)
        {
            Array.Copy(sourceArray, 0, destinationArray, destinationIndex, length);
        }

        public static void CopyTo<T>(this T[] sourceArray, int sourceIndex, T[] destinationArray, int destinationIndex, int length)
        {
            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }

        public static void Clear<T>(this T[] array)
        {
            Array.Clear(array, 0, array.Length);
        }

        public static T[] SubArray<T>(this T[] array, int startIndex)
        {
            return SubArray(array, startIndex, array.Length);
        }

        public static T[] SubArray<T>(this T[] array, int startIndex, int endIndex)
        {
            T[] subArray = new T[endIndex - startIndex];
            int sourceIndex = startIndex;

            for (int destinationIndex = 0; destinationIndex < subArray.Length; sourceIndex++, destinationIndex++)
            {
                subArray[destinationIndex] = array[sourceIndex];
            }

            return subArray;
        }

        public static T FirstElement<T>(this T[] array)
        {
            return array[0];
        }

        public static T LastElement<T>(this T[] array)
        {
            return array[array.Length - 1];
        }
    }
}
