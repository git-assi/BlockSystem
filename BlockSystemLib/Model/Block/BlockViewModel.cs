

using System.ComponentModel;

namespace BlockSystemLib.Model.Block
{
    public class BlockViewModel : INotifyPropertyChanged
    {
        private BlockSegment _block;
        public BlockViewModel(BlockSegment block)
        {
            _block = block;
            _block.TrainChanged += _block_TrainChanged;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void _block_TrainChanged(object? sender, EventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("IstFrei"));
            }
        }

        public IEnumerable<BlockSegment> GetNextBlocksPainting()
        {
            return _block.GetNextBlocks(BewegungsRichtung.Vorwärts);
        }

        public bool IstFrei
        {
            get
            {
                return _block.IstFrei;
            }

        }

        public string Name => _block.Name;

        public int BlocksPreviousCount => _block.GetNextBlocks(BewegungsRichtung.Rückwärts).Count();
        public int BlocksNextCount => _block.GetNextBlocks(BewegungsRichtung.Vorwärts).Count();

        public bool Ende => !_block.GetNextBlocks(BewegungsRichtung.Vorwärts).Any();
        public bool Start => !_block.GetNextBlocks(BewegungsRichtung.Rückwärts).Any();


        public string BlockType
        {
            get
            {
                if (Ende)
                {
                    return Constants.BLOCK_TYPES.ENDE;
                }
                else if (Start)
                {
                    return Constants.BLOCK_TYPES.START;
                }
                else if (BlocksPreviousCount > 1)
                {
                    return Constants.BLOCK_TYPES.WEICHE;
                }
                else if (BlocksNextCount > 1)
                {
                    return Constants.BLOCK_TYPES.WEICHE2;
                }
                else return Constants.BLOCK_TYPES.Gerade;
            }
        }       
    }
}
