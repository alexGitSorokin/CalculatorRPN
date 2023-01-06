namespace CalculatorPRN.Interfaces
{
    public interface IConverterToRPN
    {
        #region Methods
        /// <summary>
        /// Возвращает строку в формате обратной польской нотации
        /// </summary>
        /// <param name="equation">Инфиксная форма уравнения</param>
        /// <returns></returns>
        public string GetFormatRPN(string equation);
        #endregion
    }
}
