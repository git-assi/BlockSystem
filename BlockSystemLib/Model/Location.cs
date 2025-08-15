

using BlockSystemLib.Model.Block;

namespace BlockSystemLib.Model
{
    public class Location
    {
        public Location(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public List<BlockSegment> BlockSegments { get; set; } = new List<BlockSegment>();
    }
}
