using System.ComponentModel;
using System.Windows.Input;

namespace BlockSystemUI
{
    public class TestViewModel2 : INotifyPropertyChanged
    {
        private BlockSystemLib.Block block;

        public bool IsFrei
        {
            get => block.IstFrei;
        }

        public ICommand ToggleCommand { get; }

        public TestViewModel2()
        {

        }

       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
