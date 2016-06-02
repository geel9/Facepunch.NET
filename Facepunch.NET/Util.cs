using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facepunch
{
    public class Util
    {
        /// <summary>
        /// Converts the given decimal number base 36
        /// Taken from: http://stackoverflow.com/a/10981113
        /// </summary>
        /// <param name="decimalNumber">The number to convert.</param>
        public static string ConvertToBase36(long decimalNumber)
        {
            const int BitsInLong = 64;
            const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            const int destBase = 36;

            if (destBase < 2 || destBase > Digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

            if (decimalNumber == 0)
                return "0";

            int index = BitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[BitsInLong];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % destBase);
                charArray[index--] = Digits[remainder];
                currentNumber = currentNumber / destBase;
            }

            string result = new String(charArray, index + 1, BitsInLong - index - 1);
            if (decimalNumber < 0)
            {
                result = "-" + result;
            }

            return result;
        }
    }
}
