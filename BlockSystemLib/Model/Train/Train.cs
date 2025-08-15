using BlockSystemLib.Model.Block;

namespace BlockSystemLib.Model.Train
{
    public class Train
    {
        public required string Name { get; set; } = string.Empty;
        public Location NächsterHalt
        {
            get
            {
                if (Zwischenstops.Count > 0)
                {
                    return Zwischenstops.First();
                }
                return null;
            }            
        }
        private List<Location> Zwischenstops = new List<Location>();

        public BewegungsRichtung Richtung { get; set; } = new BewegungsRichtung() { RichtungTyp = BewegungsRichtungTyp.UNBEKANNT };

        public required BlockSegment CurrentBlockSegment { get; set; }



        public bool Arrived => NächsterHalt == null || NächsterHalt.BlockSegments.Contains(CurrentBlockSegment);

        public Location ZielDestination 
        { 
            get
            {
                return Zwischenstops.Last();
            }
            internal set
            {
                Zwischenstops.Clear();
                Zwischenstops.Add(value);
            }
        }

        public bool HasStopped => Richtung.RichtungTyp == BewegungsRichtungTyp.STOP;
        public bool IsNew => Richtung.RichtungTyp == BewegungsRichtungTyp.UNBEKANNT;

        public Location StartLocation { get; internal set; }

        public void AddZwischenStop(Location haltestelle)
        {
            Zwischenstops.Insert(0, haltestelle);
        }

        public void DestinationReached()
        {
            Zwischenstops.RemoveAt(0);
            if (Zwischenstops.Count == 0)
            {
                Richtung.RichtungTyp = BewegungsRichtungTyp.STOP;
            }
        }
    }
}
