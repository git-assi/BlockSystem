

namespace BlockSystemLib.Model.Block
{
    public class BlockSegment
    {
        public required string Name { get; set; } = string.Empty;

        public int BlocksPreviousCount => _blocks_previous.Count();
        public int BlocksNextCount => _blocks_next.Count();

        private List<BlockSegment> _blocks_previous = new List<BlockSegment>();
        private List<BlockSegment> _blocks_next = new List<BlockSegment>();
        public bool Ende => !_blocks_next.Any();
        public bool Start => !_blocks_previous.Any();

        public Train.Train? Train { get; set; } = null;

        public int Length { get; set; } = 1;

        public bool IstFrei => Train == null;

        public List<BlockSegment> GetNextBlocks(BewegungsRichtung richtung)
        {
            switch (richtung)
            {
                case BewegungsRichtung.Unbekannt:
                    return new List<BlockSegment>();

                case BewegungsRichtung.Vorwärts:
                    return _blocks_next;

                case BewegungsRichtung.Rückwärts:
                    return _blocks_previous;
            }    
            
            throw new Exception("unbekannte Richtung");
        }

        public void AddNext(BlockSegment block)
        {
            block.AddPrevious(this);
            _blocks_next.Add(block);
        }

        private void AddPrevious(BlockSegment block)
        {
            _blocks_previous.Add(block);
        }
    }
}
