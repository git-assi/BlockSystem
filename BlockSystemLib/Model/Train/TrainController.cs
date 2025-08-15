using BlockSystemLib.Factories;
using BlockSystemLib.Model.Block;
using System.Diagnostics;

namespace BlockSystemLib.Model.Train
{
    public class TrainController
    {
        private List<BlockSegment> GetNextBlocks(BlockSegment aktuellerBlock, BewegungsRichtung richtung)
        {
            switch (richtung.RichtungTyp)
            {
                case BewegungsRichtungTyp.VORWÄRTS:
                    return aktuellerBlock.BlocksNext;
                case BewegungsRichtungTyp.RÜCKWÄRTS:
                    return aktuellerBlock.BlocksPrevious;
                default:
                    return new List<BlockSegment>();
            }
        }

        public bool FindWay(Location destination, BlockSegment aktuellerBlock, BewegungsRichtung richtung)
        {

            if (destination.BlockSegments.Contains(aktuellerBlock))
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

        public BewegungsRichtung FindRichtung(Location destination, BlockSegment aktuellerBlock)
        {
            BewegungsRichtung result = new() { RichtungTyp = BewegungsRichtungTyp.UNBEKANNT };
            
            result.RichtungTyp = BewegungsRichtungTyp.VORWÄRTS;
            if (FindRichtung(destination, aktuellerBlock, result))
            {
                return result;
            }
            result.RichtungTyp = BewegungsRichtungTyp.RÜCKWÄRTS;
            if (FindRichtung(destination, aktuellerBlock, result))
            {
                return result;
            }
            result.RichtungTyp = BewegungsRichtungTyp.STOP;
            return result;
        }

        public bool FindRichtung(Location destination, BlockSegment aktuellerBlock, BewegungsRichtung richtung)
        {
            if (destination.BlockSegments.Contains(aktuellerBlock))
            {
                return true;
            }

            //einfache Wegfindung, erster Treffer wird genommen
            var nextBlocks = GetNextBlocks(aktuellerBlock, richtung);

            foreach (BlockSegment b in nextBlocks)
            {
                if (FindRichtung(destination, b, richtung))
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<BlockSegment> GetNextPossibleBlocks(BewegungsRichtung richtung, BlockSegment aktuellerBlock)
        {            
            return GetNextBlocks(aktuellerBlock, richtung);
        }

        public bool MoveToBlock(Train train, BlockSegment nextBlock)
        {
            if (!GetNextBlocks(train.CurrentBlockSegment, train.Richtung).Contains(nextBlock))
            {
                Debug.WriteLine($"{train.Name} in {train.CurrentBlockSegment.Name}");
                return false;
            }

            if (!nextBlock.IstFrei)
            {
                Debug.WriteLine($"{nextBlock.Name} ist gesperrt");
                return false;
            }

            Leave(train.CurrentBlockSegment);
            Enter(train, nextBlock);

            return true;
        }

        public static void Enter(Train train, BlockSegment block)
        {
            train.CurrentBlockSegment = block;
            block.Train = train;
            Debug.WriteLine($"{train.Name} enter {train.CurrentBlockSegment.Name}");
        }

        public static void Leave(BlockSegment block)
        {
            Debug.WriteLine($"{block.Train.Name} leaves {block.Name}");          
            block.Train = null;
        }
    }
}
