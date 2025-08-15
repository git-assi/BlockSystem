
using BlockSystemLib.Model.Block;
using System.Diagnostics;

namespace BlockSystemLib.Model
{
    public class SchattenBahnhof : Location
    {
        public SchattenBahnhof() : base(Constants.LOCATION_NAMES.SCHATTENBAHNHOF)
        {

        }


        public Train.Train CreateTrain(string name, Location zielLocation, Location startLocation)
        {
            BlockSegment gleis;
            if (base.BlockSegments.Any(bs => bs.IstFrei && startLocation.BlockSegments.Contains(bs)))
            {
                gleis = base.BlockSegments.First();
            }
            else
            {
                string blockName = $"sb_gleis{base.BlockSegments.Count + 1} ziel {startLocation.Name}";
                gleis = new() { Name = blockName, IstSchattenbahnhof = true };
                gleis.BlocksNext.Add(startLocation.BlockSegments.First());
                base.BlockSegments.Add(gleis);
            }



            Train.Train result = new() { CurrentBlockSegment = gleis, Name = name, ZielDestination = zielLocation, StartLocation = startLocation };
            result.Richtung.RichtungTyp = BewegungsRichtungTyp.VORWÄRTS;
            gleis.Train = result;
            Debug.WriteLine($"Added {name} {zielLocation.Name} to Schattenbahnhof {gleis.Name}");
            return result;
        }
    }
}
