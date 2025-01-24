using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace BlockSystemLib.Model
{
    public class Train
    {
        public int cnt => moveCnt;
        public string Name { get; private set; } = string.Empty;
        private string Destination { get; set; } = string.Empty;
        private Block _block;
        
        private Block CurrentBlock 
        { 
            get
            {
                return _block;
            }
            set
            {
                _block = value;
                this.moveCnt = _block.Length;
            }
        }
        public bool Arrived => CurrentBlock?.Name == Destination && !String.IsNullOrWhiteSpace(Destination);

        public bool Vorwaerts { get; set; } = true;

        public Train(string name, Block startBlock, string destination)
        {
            Name = name;
            CurrentBlock = startBlock;
            moveCnt = CurrentBlock.Length;
            startBlock.Enter(this);
            Destination = destination;
        }
        public bool FindWay(Block currentBlock)
        {
            if (string.IsNullOrEmpty(Destination))
                return false;

            //if (!currentBlock.IstFrei) return false;

            Debug.WriteLine($"{currentBlock.Name} == {Destination} {currentBlock.Name == Destination}");
            if (currentBlock.Name == Destination)
            {
                return true;
            }
            if (currentBlock.Ende)
            {
                return false;
            }

            //einfache Wegfindung, erster Treffer wird genommen
            foreach (Block b in currentBlock.GetNextBlocks(Vorwaerts))
            {
                if (FindWay(b))
                {
                    return true;
                }
            }
            return false;
        }
        private int moveCnt = 0;
        public bool MoveToBlock(Block nextBlock)
        {
            if (--moveCnt <= 0)
            {
                if (!CurrentBlock.GetNextBlocks(Vorwaerts).Contains(nextBlock))
                {
                    Debug.WriteLine($"keine Verbindung von {CurrentBlock.Name} zu {nextBlock.Name}");
                    return false;
                }
                if (!nextBlock.IstFrei)
                {
                    Debug.WriteLine($"{nextBlock.Name} ist gesperrt");
                    return false;
                }

                CurrentBlock.Leave();
                nextBlock.Enter(this);
                CurrentBlock = nextBlock;
                moveCnt = CurrentBlock.Length;
            }
            return true;
        }

        public void Leave()
        {
            CurrentBlock?.Leave();
        }

        public IEnumerable<Block> GetNextBlocks()
        {
            return CurrentBlock.GetNextBlocks(Vorwaerts);
        }
    }
}
