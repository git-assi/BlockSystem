


namespace BlockSystemLib.Model
{
    public class TrainCollection
    {
        private List<Train.TrainViewModel> _allTrains = new();

        // Event deklarieren
        public event EventHandler<TrainAddedEventArgs>? TrainAdded;

        public void Add(Train.TrainViewModel train)
        {
            _allTrains.Add(train);
            OnTrainAdded(train);
        }

        public IEnumerable<Train.TrainViewModel> NewTrains()
        {
            var result = _allTrains.Where(t => !t.Arrived && t.IsNew);
            if (result.Any())
            {
                result = result.OrderByDescending(t => t.Prio);
            }
            return result;
        }

        public IEnumerable<Train.TrainViewModel> ArrviedTrains()
        {
            var result = _allTrains.Where(t => t.Arrived && !t.HasStopped);
            if (result.Any())
            {
                result = result.OrderByDescending(t => t.Prio);
            }
            return result;
        }

        public IEnumerable<Train.TrainViewModel> RollingTrains()
        {
            var result = _allTrains.Where(t => !t.Arrived && !t.HasStopped);
            if (result.Any())
            {
                result = result.OrderByDescending(t => t.Prio);
            }
            return result;
        }

        // Methode zum Auslösen des Events
        protected virtual void OnTrainAdded(Train.TrainViewModel train)
        {
            TrainAdded?.Invoke(this, new TrainAddedEventArgs(train));
        }
    }

    // EventArgs-Klasse für zusätzliche Informationen
    public class TrainAddedEventArgs : EventArgs
    {
        public Train.TrainViewModel Train { get; }

        public TrainAddedEventArgs(Train.TrainViewModel train)
        {
            Train = train;
        }
    }

}
