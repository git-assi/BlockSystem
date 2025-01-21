using BlockSystemLib;
using System.ComponentModel;

namespace BlockSystemUI
{
    internal class BlockViewModel : INotifyPropertyChanged
    {
        private Block _block;
        public BlockViewModel(Block block)
        {
            _block = block;
            _block.IstFreiChanged += _block_IstFreiChanged;
        }

        private void _block_IstFreiChanged(object? sender, EventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("IstFrei"));
            }
        }

        public bool IstFrei
        {
            get
            {
                return _block.IstFrei;
            }
            set
            {
                _block.IstFrei = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
