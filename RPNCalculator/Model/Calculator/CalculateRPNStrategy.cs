using CalculatorPRN.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorPRN
{
    public class CalculateRPNStrategy : ICalculateRPNStrategy
    {
        #region Fields
        private List<string> _operands = new List<string>();

        IDictionary<char, Func<double, double, double>> _operations;
        #endregion

        #region Constructor
        public CalculateRPNStrategy(IDictionary<char, Func<double, double, double>> operations)
        {
            _operations = operations;
        }
        #endregion

        #region Properties
        public IDictionary<char, Func<double, double, double>> Operations { get => _operations; set => _operations=value; }
        #endregion

        #region Methods
        public double Calculate(string equationRPN)
        {
            if (string.IsNullOrEmpty(equationRPN))
                throw new ArgumentException();

            string operand = string.Empty;

            for (int i = 0; i < equationRPN.Length; i++)
            {
                char currentSymbol = equationRPN[i];
                if (char.IsWhiteSpace(currentSymbol))
                {
                    _operands.Add(operand);
                    operand = string.Empty;
                    continue;
                }

                if (!IsMathOperation(currentSymbol))
                {
                    operand += equationRPN[i];
                    continue;
                }
                else
                {
                    if (operand != string.Empty)
                    {
                        _operands.Add(operand);
                        operand = string.Empty;
                    }
                    ExecuteOperationForTwoLastOperands(currentSymbol);
                }
            }
            if (_operands.Count() == 1)
            {
                 var result = Convert.ToDouble(_operands[_operands.Count - 1]);
                _operands.Clear();
                return result;
            }
            throw new Exception("Incorrect calculation");
        }

        private bool IsMathOperation(char symbol)
        {
            var isOperation = _operations.Keys.Contains(symbol);
            if (isOperation)
                return true;
            return false;
        }

        private void ExecuteOperationForTwoLastOperands(char symbol)
        {
            if (_operands.Count < 2 || _operands == null)
                return;

            var firstOperand = Convert.ToDouble(_operands[_operands.Count - 2]);
            var secondOperand = Convert.ToDouble(_operands[_operands.Count - 1]);

            var operation = _operations.FirstOrDefault(x => x.Key == symbol);
            var result = operation.Value.Invoke(firstOperand, secondOperand);

            _operands.RemoveAt(_operands.Count - 2);
            _operands.RemoveAt(_operands.Count - 1);
            _operands.Add(result.ToString());
        }
    }
    #endregion
}
