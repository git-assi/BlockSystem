using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace BlockSystemUI
{
    /// <summary>
    /// Interaction logic for WindowDataBind.xaml
    /// </summary>
    public partial class WindowDataBind : Window
    {
        
        public WindowDataBind()
        {
            InitializeComponent();
            DataContext = new MyViewModel();
        }
 
    }

    public class MyViewModel : INotifyPropertyChanged
    {
        private bool _isSpecial;

        public bool IsSpecial
        {
            get => _isSpecial;
            set
            {
                if (_isSpecial != value)
                {
                    _isSpecial = value;
                    OnPropertyChanged(nameof(IsSpecial));
                }
            }
        }

        public ICommand ToggleCommand { get; }

        public MyViewModel()
        {
            ToggleCommand = new RelayCommand(() => IsSpecial = !IsSpecial);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

  
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
