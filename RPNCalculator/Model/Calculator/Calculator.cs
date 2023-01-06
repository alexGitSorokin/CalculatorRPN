using CalculatorPRN.Interfaces;
using RPNCalculator.Model;
using RPNCalculator.Model.Interfaces;

namespace CalculatorPRN
{
    public class Calculator : IRPNCalculator
    {
        #region Fields
        public ICalculateRPNStrategy _calculateStrategy;
        public IConverterToRPN _converterRPN;
        private IEquationChecker _equationChecker;

        private string _equattion;
        private string _equattionRPN;
        private string _result = string.Empty;
        #endregion

        #region Properties
        public string Equation { get => _equattion; }
        public string EquationRPN { get => _equattionRPN; }
        public string Result { get => _result; set => _result = value; }
        #endregion

        #region Constructor
        public Calculator(ICalculateRPNStrategy strategy, IConverterToRPN converterRPN)
        {
            _equattion = string.Empty;
            _calculateStrategy = strategy;
            _converterRPN = converterRPN;

            _equationChecker = new EquationChecker();
        }
        #endregion

        #region Methods
        public void CalculateRPN()
        {
            if (!IsEquationCorrect(Equation))
                return;
            var equattionRPN = _converterRPN.GetFormatRPN(Equation);
            try
            {
                double resultDouble = _calculateStrategy.Calculate(equattionRPN);
                _result = resultDouble.ToString();
                _equattionRPN = equattionRPN;
            }
            catch
            {
                _result = string.Empty;
            }
        }

        public void AddSymbolInEquation(string symbol)
        {
            if (!string.IsNullOrEmpty(symbol))
                _equattion += symbol;
        }

        private void СlearEquation() => _equattion = string.Empty;
        private void ClearRPNEquation() => _equattionRPN = string.Empty;
        private void ClearResult() => _result = string.Empty;

        public void ClearEquationsAndResult()
        {
            СlearEquation();
            ClearRPNEquation();
            ClearResult();
        }

        private bool IsEquationCorrect(string equation)
        {
            if (_equationChecker.Check(equation))
                return true;
            return false;
        }
        #endregion
    }
}
