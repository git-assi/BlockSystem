using BlockSystemLib.Model.Block;

namespace BlockSystemLib.Model.Train
{
    public class TrainViewModel
    {
        private TrainController _trainController;

        private Train _train;
        public TrainViewModel(Train train)
        {
            _train = train;
            _trainController = new TrainController();
        }

        public Location NächsterHalt
        {
            get
            {
                if (_train.Streckenplan.Count > 0)
                {
                    return _train.Streckenplan.First();
                }
                return null;
            }
        }

        public bool Arrived => NächsterHalt == null || NächsterHalt.BlockSegments.Contains(_train.CurrentBlockSegment);

        public bool HasStopped => _train.Richtung.RichtungTyp == BewegungsRichtungTyp.STOP;
        public bool IsNew => _train.Richtung.RichtungTyp == BewegungsRichtungTyp.UNBEKANNT;
       
        public int Richtung 
        { 
            get
            {
                return _train.Richtung.RichtungTyp;
            }
            set
            {
                _train.Richtung.RichtungTyp = value;
            }
        }

        public void AddZwischenStop(Location haltestelle)
        {
            _train.Streckenplan.Insert(0, haltestelle);
        }

        public void SetDestinationReached()
        {
            _train.Streckenplan.RemoveAt(0);
            if (_train.Streckenplan.Count == 0)
            {
                _train.Richtung.RichtungTyp = BewegungsRichtungTyp.STOP;
            }
        }

        public void FindRichtung()
        {
            _train.Richtung.RichtungTyp = _trainController.FindRichtung(NächsterHalt, _train.CurrentBlockSegment);
        }

        public void Move()
        {
            foreach (var possibleNextBlock in _trainController.GetNextPossibleBlocks(Richtung, CurrentBlockSegment))
            {
                if (_trainController.FindWay(NächsterHalt, possibleNextBlock, Richtung))
                {
                    if (_trainController.MoveToBlock(this, possibleNextBlock))
                    {
                        break;
                    }
                }
            }
        }

        public BlockSegment CurrentBlockSegment 
        { 
            get
            {
                return _train.CurrentBlockSegment;
            }
            set
            {
                _train.CurrentBlockSegment = value;
            }
        }

        public string Name
        {
            get
            {
                return _train.Name;
            }
        }

        public Location ZielDestination
        {
            get
            {
                return _train.Streckenplan.Last();
            }
            set
            {
                _train.Streckenplan.Clear();
                _train.Streckenplan.Add(value);
            }
        }
    }

}
