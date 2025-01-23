﻿
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

        public bool Vorwaerts { get; set; } = true;

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

            if (!currentBlock.IstFrei) return false;

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

        public bool MoveToBlock(Block nextBlock)
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
            CurrentBlock.IstFrei = true;
            nextBlock.IstFrei = false;
            CurrentBlock = nextBlock;
            if (CurrentBlock.Name == Destination)
            {
                Arrived = true;                
            }


            return true;
        }

       
    }
}
