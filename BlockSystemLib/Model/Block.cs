
namespace BlockSystemLib
{
    public class Block
    {
        public Block(int col)
        {            
            Col = col;            
        }

        private List<Block> blocks_previous = new List<Block>();
        private List<Block> blocks_next = new List<Block>();

        public List<Block> NextBlocks => blocks_next;

        public bool IstFrei { get; set; } = true;


        public void AddNext(Block block) 
        {
            block.Row = this.blocks_next.Count;
            block.blocks_previous.Add(this);
            blocks_next.Add(block); 
        }

        public int Row = 0;
        public int Col = 0;

        public bool Ende => !blocks_next.Any();
        public bool Start => !blocks_previous.Any();

        public string Name => $"{GetBlockType()} {Row} {Col}";

        private object GetBlockType()
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

        public override string ToString()
        {
            return Name;
        }

    }
}
