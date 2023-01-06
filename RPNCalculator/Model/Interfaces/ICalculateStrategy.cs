using System;
using System.Collections.Generic;

namespace CalculatorPRN.Interfaces
{
    public interface ICalculateRPNStrategy
    {
        #region Properties
        /// <summary>
        /// Операции в порядке возрастания приоритета и методы расчёта
        /// </summary>
        public IDictionary<char, Func<double, double, double>> Operations { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Рассчитывает результ выражения в формате RPN
        /// </summary>
        /// <param name="equation">Строковое представление уравнения в RPN</param>
        /// <returns></returns>
        public double Calculate(string equation);
        #endregion
    }
}
