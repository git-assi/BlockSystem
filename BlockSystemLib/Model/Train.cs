
using System.Diagnostics;
using System.Transactions;

namespace BlockSystemLib.Model
{
    public class Train
    {
        public string Name { get; private set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public Block CurrentBlock { get; private set; }
        public bool Arrived { get; private set; } = false;

        public Train(string name, Block startBlock)
        {
            Name = name;
            CurrentBlock = startBlock;
            CurrentBlock.IstFrei = false;
        }
        public bool FindWay(Block currentBlock)
        {
            if (string.IsNullOrEmpty(Destination)) 
                return false;

            if (currentBlock.Name == Destination)
            {
                return true;
            }
            if (currentBlock.Ende)
            {
                return false;
            }

            //einfache Wegfindung, erster Treffer wird genommen
            foreach (Block b in currentBlock.NextBlocks)
            {
                return FindWay(b);

            }
            return false;
        }

        public bool MoveToBlock(Block nextBlock)
        {
            if (!CurrentBlock.NextBlocks.Contains(nextBlock))
            {
                Debug.WriteLine($"keine Verbindung von {CurrentBlock.Name} zu {nextBlock.Name}");
                return false;
            }
            if (!nextBlock.IstFrei)
            {
                Debug.WriteLine($"{nextBlock.Name} ist gesperrt");
                return false;
            }
            CurrentBlock.IstFrei = true;
            nextBlock.IstFrei = false;
            CurrentBlock = nextBlock;
            Arrived = CurrentBlock.Name == Destination;

            return true;
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
