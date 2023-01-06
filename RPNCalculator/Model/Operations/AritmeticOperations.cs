using System;
using System.Collections.Generic;

namespace CalculatorPRN
{
    public static class AritmeticOperations
    {
        #region Fields
        /// <summary>
        /// Операции в списке возрастания приоритетов и методов расчёта для них
        /// </summary>
        public static readonly Dictionary<char, Func<double, double, double>> Operations;
        public static readonly List<char> MathSymbols;
        #endregion

        #region
        static AritmeticOperations()
        {
            Operations = new Dictionary<char, Func<double, double, double>>();
            {
                Operations.Add('+', (x, y) => (x + y));
                Operations.Add('-', (x, y) => (x - y));
                Operations.Add('*', (x, y) => (x * y));
                Operations.Add('/', (x, y) => (x / y));
                Operations.Add('^', (x, y) => (Math.Pow(x, y)));
            }
        }

        #endregion
    }
}

