using RPNCalculator.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RPNCalculator.Model
{
    public class EquationChecker : IEquationChecker
    {
        #region Fields

        private List<char> operations;

        #endregion

        #region Methods
        public EquationChecker()
        {
            operations = new List<char>() {

                '+',
                '-',
                '*',
                '/',
                '^',
                ',',
                '.',
            };
        }

        public bool Check(string equation)
        {
            if (!IsBracketsPostionsCorrect(equation))
                return false;
            if (!IsOperationOrderCorrect(equation))
                return false;
            return true;
        }

        /// <summary>
        /// Проверяет корректность расположения скобок
        /// </summary>
        /// <param name="equation"></param>
        /// <returns></returns>
        private bool IsBracketsPostionsCorrect(string equation)
        {
            if (equation.EndsWith("("))
                return false;

            if (equation.Contains("()"))
                return false;

            var openBrackets = equation.Select(x => x).Where(x => x == ')');
            var clesedBrackets = equation.Select(x => x).Where(x => x == '(');

            if (openBrackets.Count() != clesedBrackets.Count())
                return false;
            return true;
        }

        /// <summary>
        /// Проверяет есть ли две операции следующие друг за другом
        /// </summary>
        /// <param name="equation"></param>
        /// <returns></returns>
        private bool IsOperationOrderCorrect(string equation)
        {
            var operationIndexes = new List<int>();

            for (int i = 0; i < equation.Length; i++)
            {
                foreach (var operation in operations)
                {
                    if (equation[i] == operation)
                    {
                        operationIndexes.Add(i);
                        continue;
                    }
                }
            }

            for (int i = 0; i < operationIndexes.Count() - 1; i++)
            {
                if (i == operationIndexes.Count())
                    return true;
                if (operationIndexes[i + 1] - operationIndexes[i] == 1)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
