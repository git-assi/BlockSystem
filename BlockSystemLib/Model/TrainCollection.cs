


namespace BlockSystemLib.Model
{
    public class TrainCollection
    {
        private List<Train.Train> _allTrains = new();

        // Event deklarieren
        public event EventHandler<TrainAddedEventArgs>? TrainAdded;

        public void Add(Train.Train train)
        {
            _allTrains.Add(train);
            OnTrainAdded(train);
        }        

        public IEnumerable<Train.Train> NewTrains()
        {
            return _allTrains.Where(t => !t.Arrived && t.IsNew);
        }

        public IEnumerable<Train.Train> ArrviedTrains()
        {
            return _allTrains.Where(t => t.Arrived && !t.HasStopped);
        }

        public IEnumerable<Train.Train> RollingTrains()
        {
            return _allTrains.Where(t => !t.Arrived && !t.HasStopped);
        }

        // Methode zum Auslösen des Events
        protected virtual void OnTrainAdded(Train.Train train)
        {
            TrainAdded?.Invoke(this, new TrainAddedEventArgs(train));
        }
    }

    // EventArgs-Klasse für zusätzliche Informationen
    public class TrainAddedEventArgs : EventArgs
    {
        public Train.Train Train { get; }

        public TrainAddedEventArgs(Train.Train train)
        {
            Train = train;
        }
    }

}
