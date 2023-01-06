namespace RPNCalculator.Model.Interfaces
{
    internal interface IEquationChecker
    {
        /// <summary>
        /// Проверка уравнение на корректность
        /// </summary>
        /// <param name="equation"></param>
        public bool Check(string equation);

    }
}
