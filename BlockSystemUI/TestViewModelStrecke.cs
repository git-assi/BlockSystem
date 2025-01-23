using System.ComponentModel;
using System.Windows.Input;

namespace BlockSystemUI
{
    public class TestViewModelStrecke : INotifyPropertyChanged
    {
        private BlockSystemLib.Block block;

        public bool IsFrei
        {
            get => block.IstFrei;
        }

        public ICommand ToggleCommand { get; }

        public TestViewModelStrecke()
        {

        }

       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
