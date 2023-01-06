
namespace CalculatorPRN.Interfaces
{
    internal interface IRPNCalculator
    {
        #region Properties
        /// <summary>
        /// Инфиксная запись уравнения
        /// </summary>
        public string Equation { get; }

        /// <summary>
        /// Запись уравнение в обратной польской нотации
        /// </summary>
        public string EquationRPN { get; }

        /// <summary>
        /// Результат расчёта уравнения
        /// </summary>
        public string Result { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Расчитывает результат выражения в форме обратной Польской записи
        /// </summary>
        /// <param name="equationRPN"></param>
        /// <returns></returns>
        public void CalculateRPN();

        /// <summary>
        /// Добавляет символ в уравнение
        /// </summary>
        /// <param name="symbol"></param>
        public void AddSymbolInEquation(string symbol);

        /// <summary>
        /// Очистить инфиксную и польскую запись выражения и результат расчёта
        /// </summary>
        public void ClearEquationsAndResult();
        #endregion
    }
}
