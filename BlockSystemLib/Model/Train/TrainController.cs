using BlockSystemLib.Model.Block;
using System.Diagnostics;

namespace BlockSystemLib.Model.Train
{
    public class TrainController
    {
        public bool FindWay(string destination, BlockSegment aktuellerBlock, BewegungsRichtung richtung)
        {
            if (string.IsNullOrEmpty(destination))
            {
                return false;
            }

            if (aktuellerBlock.Name == destination)
            {
                return true;
            }            

            //einfache Wegfindung, erster Treffer wird genommen
            foreach (BlockSegment b in aktuellerBlock.GetNextBlocks(richtung))
            {
                if (FindWay(destination, b, richtung))
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Block.BlockSegment> GetNextPossibleBlocks(BewegungsRichtung richtung, BlockSegment aktuellerBlock)
        {
            return aktuellerBlock.GetNextBlocks(richtung);            
        }

        public bool MoveToBlock(Train train, BlockSegment nextBlock)
        {

            if (!train.CurrentLocation.GetNextBlocks(train.Richtung).Contains(nextBlock))
            {
                throw new Exception($"keine Verbindung von {train.CurrentLocation.Name} zu {nextBlock.Name}");
            }

            if (!nextBlock.IstFrei)
            {
                Debug.WriteLine($"{nextBlock.Name} ist gesperrt");
                return false;
            }

            Leave(train.CurrentLocation);
            Enter(train, nextBlock);

            return true;
        }

        public void Enter(Train train, BlockSegment block)
        {
            train.CurrentLocation = block;
            block.Train = train;
        }

        public void Leave(BlockSegment block)
        {
            block.Train = null;
        }
    }
}
