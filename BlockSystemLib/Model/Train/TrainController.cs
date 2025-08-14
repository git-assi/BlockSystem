using BlockSystemLib.Model.Block;
using System.Diagnostics;

namespace BlockSystemLib.Model.Train
{
    public class TrainController
    {
        private List<BlockSegment> GetNextBlocks(BlockSegment aktuellerBlock, BewegungsRichtung richtung)
        {
            switch (richtung)
            {
                case BewegungsRichtung.Vorwärts:
                    return aktuellerBlock.BlocksNext;                    
                case BewegungsRichtung.Rückwärts:
                    return aktuellerBlock.BlocksPrevious;                    
                default:
                    return new List<BlockSegment>();
            }
        }

        public bool FindWay(string destination, BlockSegment aktuellerBlock, BewegungsRichtung richtung)
        {
           
            if (aktuellerBlock.Name == destination)
            {
                return true;
            }

            //einfache Wegfindung, erster Treffer wird genommen
            var nextBlocks = GetNextBlocks(aktuellerBlock, richtung).Where(b => b.IstFrei).ToList();

            foreach (BlockSegment b in nextBlocks)
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
            return GetNextBlocks(aktuellerBlock, richtung);
        }

        public bool MoveToBlock(Train train, BlockSegment nextBlock)
        {

            if (!GetNextBlocks(train.CurrentLocation, train.Richtung).Contains(nextBlock))
            {
                return false;
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
