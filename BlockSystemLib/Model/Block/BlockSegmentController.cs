

namespace BlockSystemLib.Model.Block
{
    public class BlockSegmentController
    {
        public List<BlockSegment> GetNextBlocks(BlockSegment block, BewegungsRichtung richtung)
        {
            return richtung switch
            {
                BewegungsRichtung.Stop => new List<BlockSegment>(),
                BewegungsRichtung.Vorwärts => block.BlocksNext,
                BewegungsRichtung.Rückwärts => block.BlocksPrevious,
                _ => throw new Exception("unbekannte Richtung"),
            };
        }        
    }
}
