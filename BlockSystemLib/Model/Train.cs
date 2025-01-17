
using System.Diagnostics;

namespace BlockSystemLib.Model
{
    public class Train
    {
        public string Name { get; set; }
        public Block CurrentBlock { get; set; }

        public Train(string name, Block startBlock)
        {
            Name = name;
            CurrentBlock = startBlock;
            CurrentBlock.IstFrei = false;
        }

        public bool MoveToNextBlock()
        {
            if (CurrentBlock.Ende)
            {
                Debug.WriteLine($"{Name} has reached the end of the line.");
                return false;
            }

            var nextBlock = CurrentBlock.NextBlocks.FirstOrDefault(b => b.IstFrei);
            if (nextBlock != null)
            {
                CurrentBlock.IstFrei = true;
                nextBlock.IstFrei = false;
                CurrentBlock = nextBlock;
                Debug.WriteLine($"{Name} moved to the next block {CurrentBlock.BlockType}.");
            }
            else
            {
                Debug.WriteLine($"{Name} cannot move to the next block. It's occupied.");
            }
            return true;
        }
    }
}
