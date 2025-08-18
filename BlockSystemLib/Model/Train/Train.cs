using BlockSystemLib.Model.Block;

namespace BlockSystemLib.Model.Train
{
    public class Train
    {
        public required string Name { get; set; } = string.Empty;

        public int Prio { get; set; } = 0;

        public Location? StartLocation { get; internal set; }

        public List<Location> Streckenplan = new List<Location>();

        public BewegungsRichtung Richtung { get; set; } = new BewegungsRichtung() { RichtungTyp = BewegungsRichtungTyp.UNBEKANNT };

        public required BlockSegment CurrentBlockSegment { get; set; }      

    }
}
