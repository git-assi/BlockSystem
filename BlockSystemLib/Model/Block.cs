﻿
namespace BlockSystemLib
{
    public class Block
    {

        public Block()
        {                

        }

        public Block(string name)
        {
            this.name = name;
        }
        private string name = "";
        private List<Block> blocks_previous = new List<Block>();
        private List<Block> blocks_next = new List<Block>();

        public List<Block> NextBlocks => blocks_next;

        public event EventHandler IstFreiChanged;
        
        protected virtual void OnIstFreiChanged(EventArgs e)
        {
            IstFreiChanged?.Invoke(this, e);
        }

        private bool _istFrei = false;
        public bool IstFrei 
        { 
            get
            {
                return _istFrei;
            }

            set
            {
                if (_istFrei != value)
                {
                    OnIstFreiChanged(EventArgs.Empty);
                    _istFrei = value;
                }
            }
        }

        public List<Block> GetNext(bool vorwärts)
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
                return name;
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
        public void foobar()
        {
                        
        }
    }

    public class BlockXx
    {
        public BlockXx()
        {
            Block bStart = new Block();
            Block2 b2 = new Block2();

            bStart.AddNext(b2);

        }
    }
}
