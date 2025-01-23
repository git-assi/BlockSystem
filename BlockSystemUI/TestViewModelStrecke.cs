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

            ToggleCommand = new RelayCommand(() => block.Ping());

            block = new BlockSystemLib.Block();
            block.IstFreiChanged += ((object? sender, EventArgs e) => OnPropertyChanged(nameof(IsFrei)));

        }

       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
