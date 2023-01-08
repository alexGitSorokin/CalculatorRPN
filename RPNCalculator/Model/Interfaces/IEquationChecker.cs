namespace RPNCalculator.Model.Interfaces
{
    internal interface IEquationChecker
    {
        #region Methods
        /// <summary>
        /// Проверка уравнение на корректность
        /// </summary>
        /// <param name="equation"></param>
        public bool Check(string equation);
        #endregion
    }
}
