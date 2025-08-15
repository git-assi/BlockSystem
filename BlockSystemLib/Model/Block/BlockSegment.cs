

namespace BlockSystemLib.Model.Block
{
    public class BlockSegment
    {        
        public override string ToString()
        {
            return Name;
        }

        public required string Name { get; set; } = string.Empty;

        public List<BlockSegment> BlocksPrevious { get; set; } = new List<BlockSegment>();
        public List<BlockSegment> BlocksNext { get; set; } = new List<BlockSegment>();

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

        public bool IstFrei => Train == null || Train.Richtung.RichtungTyp == BewegungsRichtungTyp.STOP;        

        protected virtual void OnTrainChanged(EventArgs e)
        {
            TrainChanged?.Invoke(this, e);
        }


    }

}
