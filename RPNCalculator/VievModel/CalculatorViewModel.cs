using CalculatorPRN;
using CalculatorPRN.Interfaces;
using System.ComponentModel;
using System.Windows.Input;

namespace RPNCalculator.VievModel
{
    internal class CalculatorViewModel : INotifyPropertyChanged
    {
        #region Fields

        private IRPNCalculator _calculator;

        #endregion

        #region Properties
        public RelayCommand SymbolCommand { get; private set; }
        public RelayCommand TimesCommand { get; private set; }
        public RelayCommand EqualsCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }

        public string Equation
        {
            get
            {
                if (!string.IsNullOrEmpty(_calculator.Equation))
                    return _calculator.Equation;
                return string.Empty;
            }
        }

        public string EquationRPN
        {
            get
            {
                if (!string.IsNullOrEmpty(_calculator.EquationRPN))
                    return _calculator.EquationRPN;
                return string.Empty;
            }
        }

        public string Result
        {
            get
            {
                if (!string.IsNullOrEmpty(_calculator.Result))
                    return _calculator.Result;
                return string.Empty;
            }
        }
        #endregion


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion



        #region Constructor
        public CalculatorViewModel(IConverterToRPN converter, ICalculateRPNStrategy strategy)
        {
            _calculator = new Calculator(strategy, converter);
            
            SymbolCommand = new RelayCommand(param => { _calculator.AddSymbolInEquation((string)param); UpdateDisplayInput(); });
            ClearCommand = new RelayCommand(param => { _calculator.ClearEquationsAndResult(); UpdateDisplayResult(); }); 
            EqualsCommand = new RelayCommand(param => { _calculator.CalculateRPN(); UpdateDisplayResult(); });
        }
        #endregion

        #region Methods
        private void UpdateDisplayInput()
        {
            NotifyPropertyChanged(nameof(Equation));
            CommandManager.InvalidateRequerySuggested();
        }

        private void UpdateDisplayResult()
        {
            NotifyPropertyChanged(nameof(Equation));
            NotifyPropertyChanged(nameof(EquationRPN));
            NotifyPropertyChanged(nameof(Result));
            CommandManager.InvalidateRequerySuggested();
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
