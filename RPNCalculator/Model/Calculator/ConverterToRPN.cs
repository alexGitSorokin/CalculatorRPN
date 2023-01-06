using CalculatorPRN.Interfaces;
using CalculatorRPN.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorPRN
{
    public class ConverterToRPN : IConverterToRPN
    {
        #region Fields
        private IList<char> _operations = new List<char>()
        {
            {'+'},
            {'-'},
            {'*'},
            {'/'},
            {'^'}
        };
        #endregion

        #region Methods
        /// <summary>
        /// Возвращает запись инфиксной формы уравнения в формате обратной Польской нотации
        /// </summary>
        /// <param name="equation"></param>
        /// <returns></returns>
        public string GetFormatRPN(string equation)
        {
            if (string.IsNullOrEmpty(equation))
                return string.Empty;

            var RPNequation = ConvertToRPNB(equation);
            return RPNequation;
        }

        /// <summary>
        /// Преобразовать инфиксную форму уравнения в форму обратной Польской нотации
        /// </summary>
        /// <param name="equation"></param>
        /// <returns></returns>
        private string ConvertToRPNB(string equation)
        {
            if (string.IsNullOrEmpty(equation))
                return string.Empty;
            string equationRPN = string.Empty;

            equation = ReplaceNegativeNumbers(equation);

            if (string.IsNullOrEmpty(equation))
                return String.Empty;

            foreach (var operation in _operations)
            {
                if (equation.Contains(operation))
                {
                    var operationIndex = FindOperationNotInBrackets(equation, operation);
                    if (operationIndex == -1)
                        continue;

                    string[] operands = SplitToTwoOperands(equation, operationIndex);
                    if (operands.Length < 2)
                        continue;

                    var leftOperand = operands[0];
                    var rightOperand = operands[1];

                    if (!IsSimpleEquation(leftOperand))
                        leftOperand = ConvertToRPNB(leftOperand);

                    if (!IsSimpleEquation(rightOperand))
                        rightOperand = ConvertToRPNB(rightOperand);


                    if (!leftOperand.Contains(' '))
                        leftOperand = leftOperand + " ";
                    equationRPN = $"{leftOperand}{rightOperand}{operation}";
                    break;
                }
            }
            return equationRPN;
        }

        /// <summary>
        /// Найти операцию, которая не входит в скобки
        /// </summary>
        /// <param name="equation"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private int FindOperationNotInBrackets(string equation, char operation)
        {
            var indexesOfOperation = StringExtension.IndexesOf(equation, operation);
            foreach (var index in indexesOfOperation)
            {
                var isInBrackets = StringExtension.IsIndexInBrackets(equation, index);
                if (!isInBrackets)
                    return index;
            }
            return -1;
        }

        /// <summary>
        /// Разделяет выражение на два операнда по индексу операции
        /// </summary>
        /// <param name="equation"></param>
        /// <param name="indexOperation"></param>
        /// <returns></returns>
        private string[] SplitToTwoOperands(string equation, int indexOperation)
        {
            string[] operands = StringExtension.Split(equation, indexOperation);

            for (int i = 0; i < operands.Length; i++)
            {
                if (operands[i].EndsWith(')') && operands[i].StartsWith('('))
                    operands[i] = RemoveBracketsFromStartEndOfEquation(operands[i]);
            }
            return operands;
        }

        /// <summary>
        /// Удаляет скобки из начала и конца уравнения. 
        /// Если после удаления скобок уравнение становится некорректным - возвращается изначальная строка
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveBracketsFromStartEndOfEquation(string str)
        {
            var result = str;

            if (result.EndsWith(')'))
                result = result.Remove(str.Length - 1, 1);

            if (result.StartsWith('('))
                result = result.Remove(0, 1);

            //Проверка на корректность уравнения после удаления скобок
            for (int i = result.Length - 1; i >= 0; i--)
            {
                if (result[i] == '(')
                    return str;
                if (result[i] == ')')
                    return result;
            }
            return result;
        }

        /// <summary>
        /// Проверяет состоил ли уравнение из одной операции и двух операндов
        /// </summary>
        /// <param name="equation"></param>
        /// <returns></returns>
        private bool IsSimpleEquation(string equation)
        {
            if (string.IsNullOrEmpty(equation))
                return true;

            int countOperations = 0;
            foreach (var operation in _operations)
            {
                int countOperation = equation.Count(c => c == operation);
                countOperations += countOperation;
                if (countOperation > 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Заменяет каждое отрицательное число на разность нуля и числа 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ReplaceNegativeNumbers(string str)
        {
            if (str.Length < 2)
                return string.Empty;

            string result = str;

            if (str[0] == '-')
            {
                char operand = str[1];
                result = str.Substring(2);
                result = $"(0-{operand}){result}";
            }

            result = result.Replace("(-", "(0-");
            return result;
        }
        #endregion
    }
}
