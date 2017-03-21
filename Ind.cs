using System;
using System.Collections.Generic;
using binaryconverter;

namespace Individual
{
    public class Binary
    {
        public int Size;
        int[] binary;

        public Binary(int Size, int Sample)
        {
            // Console.WriteLine(Sample);
            this.Size = Size;
            binary = new int[Size];
            binary = IntToArray(Sample, Size);
        }

        public Binary(int[] Sample)
        {
            this.Size = Sample.Length;
            binary = Sample;
        }
        public void Mutate(int pos) {
            binary[pos] = binary[pos] == 1 ? 0 : 1;
        }

        public int ToInt()
        {
            return Converter.BinaryToInt(binary);
        }

        public override string ToString()
        {
            string bin = "";
            foreach (int i in binary)
            {
                bin += i.ToString();
            }
            return bin;
        }

        public int[] GetSubArray(int left, int right)
        {
            List<int> tempList = new List<int>();

            for (int i = left; i <= right; i++)
            {
                tempList.Add(binary[i]);
            }

            return tempList.ToArray();
        }

        private int[] IntToArray(int Num, int size)
        {
            return Converter.IntToArray(Num, size);
        }

    }
}