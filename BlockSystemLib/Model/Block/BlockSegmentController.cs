

namespace BlockSystemLib.Model.Block
{
    public class BlockSegmentController
    {
        public List<BlockSegment> GetNextBlocks(BlockSegment block, int richtung)
        {
            return richtung switch
            {
                BewegungsRichtungTyp.STOP => new List<BlockSegment>(),
                BewegungsRichtungTyp.VORWÄRTS => block.BlocksNext,
                BewegungsRichtungTyp.RÜCKWÄRTS => block.BlocksPrevious,
                _ => throw new Exception($"unbekannte Richtung {richtung}"),
            };
        }        
    }
}
