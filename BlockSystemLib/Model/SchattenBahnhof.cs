
using BlockSystemLib.Model.Block;
using BlockSystemLib.Model.Train;
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
            if (!BlockSegments.Any(bs => bs.IstFrei))
            {
                gleis = new() { Name = $"sb_gleis{base.BlockSegments.Count + 1}" };
                base.BlockSegments.Add(gleis);
            }

            gleis = BlockSegments.First(bs => bs.IstFrei);
            gleis.BlocksNext.Clear();
            gleis.BlocksNext.Add(startLocation.BlockSegments.First());

            gleis.Train = new()
            {
                CurrentBlockSegment = gleis,
                Name = name,
                ZielDestination = zielLocation,
                StartLocation = startLocation,
                Richtung = new BewegungsRichtung() { RichtungTyp = BewegungsRichtungTyp.VORWÄRTS },
            };

            Debug.WriteLine($"Added {name} Startlocation: {startLocation.Name} to Schattenbahnhof {gleis.Name}");
            return gleis.Train;
        }

        public bool ExitSchattenBahnhof(Train.Train train, BlockSegment nextBlock)
        {
            if (!nextBlock.IstFrei)
            {
                Debug.WriteLine($"{nextBlock.Name} ist gesperrt");
                return false;
            }

            TrainController.Leave(train.CurrentBlockSegment);
            TrainController.Enter(train, nextBlock);

            //Richtung zurücksetzen, wir iwssen nicht wie es weitergeht
            train.Richtung.RichtungTyp = BewegungsRichtungTyp.UNBEKANNT;

            return true;
        }

        

        public IEnumerable<Train.Train> Trains() => BlockSegments.Where(bs => !bs.IstFrei).Select(t => t.Train);
    }
}
