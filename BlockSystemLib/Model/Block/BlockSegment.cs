

namespace BlockSystemLib.Model.Block
{
    public class BlockSegment
    {
        public override string ToString()
        {
            return Name;
        }

        public required string Name { get; set; } = string.Empty;

        private List<BlockSegment> BlocksPrevious { get; set; } = new List<BlockSegment>();
        private List<BlockSegment> BlocksNext { get; set; } = new List<BlockSegment>();

        private Train.Train? _train;

        public event EventHandler? TrainChanged;

        public Train.Train? Train
        {
            get => _train;
            set
            {
                if (_train != value)
                {
                    _train = value;
                    OnTrainChanged(EventArgs.Empty);
                }
            }
        }

        public bool IstFrei => Train == null;

        public List<BlockSegment> GetNextBlocks(BewegungsRichtung richtung)
        {
            return richtung switch
            {                
                BewegungsRichtung.Stop => new List<BlockSegment>(),
                BewegungsRichtung.Vorwärts => BlocksNext,
                BewegungsRichtung.Rückwärts => BlocksPrevious,
                _ => throw new Exception("unbekannte Richtung"),
            };
        }

        public void AddNext(BlockSegment block)
        {
            block.BlocksPrevious.Add(this);
            BlocksNext.Add(block);
        }

        protected virtual void OnTrainChanged(EventArgs e)
        {
            TrainChanged?.Invoke(this, e);
        }
    }

}
