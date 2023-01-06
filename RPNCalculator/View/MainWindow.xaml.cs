using CalculatorPRN;
using CalculatorPRN.Interfaces;
using RPNCalculator.VievModel;
using System.Windows;

namespace RPNCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var operations = AritmeticOperations.Operations;
            ICalculateRPNStrategy strategy = new CalculateRPNStrategy(operations);
            IConverterToRPN converter = new ConverterToRPN();

            DataContext = new CalculatorViewModel(converter, strategy);
        }
    }
}
