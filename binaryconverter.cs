using System;

namespace binaryconverter
{
    public static class Converter
    {
        public static int BinaryToInt(int[] arr)
        {
            int value = 0;
            int length = arr.Length;
            for (int i = length - 1; i >= 0; i--)
            {
                if (arr[Math.Abs(i + 1 - length)] == 1)
                    value += (int)Math.Pow(2, i);
            }
            return value;
        }

        public static int[] IntToArray(int number, int size)
        {
            int[] bits = new int[size];

            for (int i = size - 1; i >= 0; i--)
            {
                int res = number - (int)Math.Pow(2, i);
                bits[Math.Abs(i + 1 - size)] = res >= 0 ? 1 : 0;
                number = res >= 0 ? res : number;
            }
            return bits;

        }
    }
}