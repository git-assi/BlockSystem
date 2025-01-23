
using BlockSystemLib.Model;

namespace BlockSystemLib
{
    public class Block
    {
        public Block(string name)
        {
            Name = name;
        }
        public string Name { get; private set; } = string.Empty;

        public string LabelBez
        {
            get
            {
                return $"{Name} {_train?.Name}";
            }
        }

        private Train? _train;
        public void Enter(Train train)
        {
            _train = train;
            IstFrei = _train == null;
        }

        public void Leave()
        {
            _train = null;
            IstFrei = _train == null;
        }

        private List<Block> blocks_previous = new List<Block>();
        private List<Block> blocks_next = new List<Block>();


        public event EventHandler IstFreiChanged;

        protected virtual void OnIstFreiChanged(EventArgs e)
        {
            IstFreiChanged?.Invoke(this, e);
        }

        private bool _istFrei = true;
        public bool IstFrei
        {
            get
            {
                return _istFrei;
            }

            private set
            {
                if (_istFrei != value)
                {
                    OnIstFreiChanged(EventArgs.Empty);
                    _istFrei = value;
                }
            }
        }

        public List<Block> GetNextBlocks(bool vorwärts)
        {
            return vorwärts ? blocks_next : blocks_previous;
        }

        public void AddNext(Block block)
        {
            block.blocks_previous.Add(this);
            blocks_next.Add(block);
        }

        public bool Ende => !blocks_next.Any();
        public bool Start => !blocks_previous.Any();

        public string BlockType
        {
            get
            {
                if (Ende)
                    return Constants.BLOCK_TYPES.ENDE;
                else if (Start)
                    return Constants.BLOCK_TYPES.START;
                else if (blocks_previous.Count > 1 || blocks_next.Count > 1)
                {
                    return Constants.BLOCK_TYPES.WEICHE;
                }
                else return Constants.BLOCK_TYPES.Gerade;
            }
        }

    }

    public class Block2 : Block
    {
        public Block2() : base("")
        {

        }
        public void Ping()
        {
           
        }
    }

    public class BlockXx
    {
        public BlockXx()
        {
            Block bStart = new Block("");
            Block2 b2 = new Block2();

            bStart.AddNext(b2);

        }
    }
}
