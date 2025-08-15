

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
            return ctrl.GetNextBlocks(_block, BewegungsRichtungTyp.VORWÄRTS);
        }

        public bool IstFrei
        {
            get
            {
                return _block.IstFrei;
            }

        }

        public string Name => _block.Name;

        public string Train => _block.Train == null ? string.Empty : _block.Train.Name;

        public BlockSegmentController ctrl = new BlockSegmentController();

        public int BlocksPreviousCount => ctrl.GetNextBlocks(_block, BewegungsRichtungTyp.RÜCKWÄRTS).Count();
        public int BlocksNextCount => ctrl.GetNextBlocks(_block, BewegungsRichtungTyp.VORWÄRTS).Count();

        public bool Ende => !ctrl.GetNextBlocks(_block, BewegungsRichtungTyp.VORWÄRTS).Any();
        public bool Start => !ctrl.GetNextBlocks(_block, BewegungsRichtungTyp.RÜCKWÄRTS).Any();


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

        public string Label 
        { 
            get
            {
                return string.IsNullOrEmpty(Train) ? Name : Train;
            }
             
        }
    }
}
