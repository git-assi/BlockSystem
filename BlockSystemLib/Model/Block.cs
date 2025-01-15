
namespace BlockSystemLib
{
    public class Block
    {     
        private List<Block> blocks_previous = new List<Block>();
        private List<Block> blocks_next = new List<Block>();

        public List<Block> NextBlocks => blocks_next;

        public bool IstFrei { get; set; } = true;

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
}
