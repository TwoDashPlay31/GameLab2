using System;
using System.Collections.Generic;
using System.Text;

namespace GameLab2
{


    class BigNumber
    {

        private int[] number;
        private const int Base = 1000;
        public int ArrayLength => number.Length;

        public BigNumber(string numberString)
        {
            if (string.IsNullOrWhiteSpace(numberString))
                numberString = "0";

            numberString = numberString.Replace(" ", "").TrimStart('0');
            if (numberString == "") numberString = "0";

            foreach (char c in numberString)
            {
                if (!char.IsDigit(c)) throw new ArgumentException("Строка должна содержать только цифры.");
            }

            int length = numberString.Length;
            int blockCount = (length + 2) / 3;
            number = new int[blockCount];

            int blockIdx = blockCount - 1;
            for (int i = length; i > 0; i -= 3)
            {
                int start = Math.Max(0, i - 3);
                int len = i - start;
                string part = numberString.Substring(start, len);
                number[blockIdx--] = int.Parse(part);
            }
        }


        private BigNumber(int[] arr)
        {
            number = TrimLeadingZeros(arr);
        }


        public BigNumber Clone()
        {
            int[] clonedArray = new int[this.number.Length];
            Array.Copy(this.number, clonedArray, this.number.Length);
            return new BigNumber(clonedArray);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < number.Length; i++)
            {
                if (i == 0)
                    sb.Append(number[i].ToString());
                else
                    sb.Append(number[i].ToString("D3"));
            }
            return sb.ToString();
        }


        private int[] TrimLeadingZeros(int[] arr)
        {
            int startIdx = 0;
            while (startIdx < arr.Length - 1 && arr[startIdx] == 0)
            {
                startIdx++;
            }

            if (startIdx == 0) return arr;

            int[] trimmed = new int[arr.Length - startIdx];
            Array.Copy(arr, startIdx, trimmed, 0, trimmed.Length);
            return trimmed;
        }


        private int CompareTo(BigNumber bnum)
        {
            if (this.number.Length != bnum.number.Length)
                return this.number.Length.CompareTo(bnum.number.Length);

            for (int i = 0; i < this.number.Length; i++)
            {
                if (this.number[i] != bnum.number[i])
                    return this.number[i].CompareTo(bnum.number[i]);
            }
            return 0;
        }


        public static bool operator >(BigNumber a, BigNumber b) => a.CompareTo(b) > 0;
        public static bool operator <(BigNumber a, BigNumber b) => a.CompareTo(b) < 0;
    }
}
