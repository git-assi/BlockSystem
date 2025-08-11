

namespace BlockSystemLib.Model.Block
{
    internal class BlockViewModel
    {

        private BlockSegment _block;

        public BlockViewModel(BlockSegment block)
        {
                _block = block;
        }

        public bool IstFrei
        {
            get
            {
                return _block.IstFrei;
            }
            
        }

       

        public void Enter(Train.Train train)
        {
            _block.Train = train;
            OnIstFreiChanged(new EventArgs());
        }

        public void Leave()
        {
            _block.Train = null;
            OnIstFreiChanged(new EventArgs());
        }


        public event EventHandler? IstFreiChanged = null;

        protected virtual void OnIstFreiChanged(EventArgs e)
        {
            IstFreiChanged?.Invoke(this, e);
        }

        public string LabelBez
        {
            get
            {
                return $"{_block.Name}";
            }
        }


        public string BlockType
        {
            get
            {
                if (_block.Ende)
                {
                    return Constants.BLOCK_TYPES.ENDE;
                }
                else if (_block.Start)
                {
                    return Constants.BLOCK_TYPES.START;
                }
                else if (_block.BlocksPreviousCount > 1)
                {
                    return Constants.BLOCK_TYPES.WEICHE;
                }
                else if (_block.BlocksNextCount > 1)
                {
                    return Constants.BLOCK_TYPES.WEICHE2;
                }
                else return Constants.BLOCK_TYPES.Gerade;
            }
        }
    }
}
