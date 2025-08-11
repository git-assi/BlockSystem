using BlockSystemLib.Model.Block;

namespace BlockSystemLib.Model.Train
{
    public class Train
    {       
        public required string Name { get; set; } = string.Empty;
        public required string Destination { get; set; } = string.Empty;

        public BewegungsRichtung Richtung { get; set; } = BewegungsRichtung.Unbekannt;

        public required BlockSegment CurrentLocation { get; set; }

        public bool Arrived => CurrentLocation?.Name == Destination && !String.IsNullOrWhiteSpace(Destination);        
    }
}
