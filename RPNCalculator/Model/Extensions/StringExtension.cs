using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorRPN.Extensions
{
    public static class StringExtension
    {
        #region Methods
        public static bool IsIndexInBrackets(this string str, int index)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            var leftStringPart = str.Substring(0, index);

            if (!HasBracket(leftStringPart))
                return false;
            if (!IsCountOfSymbolsEquals(leftStringPart, '(', ')'))
                    return true;

            return false;
        }

        private static bool IsCountOfSymbolsEquals(this string str1, char firstSymbol, char secondSymbol)
        {
            int firstSymbolCount = str1.Count(x => x == firstSymbol);
            int secondSymbolCount = str1.Count(x => x == secondSymbol);

            if (firstSymbolCount == secondSymbolCount)
                    return true;
            return false;
        }

        private static bool HasBracket(this string str)
        {
            if (str.Contains('(') || str.Contains(')'))
                return true;
            return false;
        }


        public static string[] Split(this string str, int index)
        {
            if (index <=0)
                return Array.Empty<string>();

            if (string.IsNullOrEmpty(str))
                return Array.Empty<string>();

            string[] operands = new string[2];
            operands[0] = str.Substring(0, index);
            operands[1] = str.Substring(index + 1);
            return operands;
        }

        public static IList<int> IndexesOf(string str, char symbol)
        {
            IList<int> indexes = new List<int>();

            int index = str.IndexOf(symbol);
            if (index != -1)
                indexes.Add(index);

            while (index != -1)
            {
                index = str.IndexOf(symbol, index + 1);
                if (index != -1)
                    indexes.Add(index);
            }
            return indexes;
        }
    }
    #endregion
}
