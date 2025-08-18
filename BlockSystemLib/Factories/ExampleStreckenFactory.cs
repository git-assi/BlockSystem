using BlockSystemLib.Model.Block;
using BlockSystemLib.Model;

namespace BlockSystemLib.Factories
{
    public class ExampleStreckenFactory
    {
        public static Location OstBahnhof;
        public static Location Gueterbahnhof;
        public static Location WestBahnhof;
        public static Location Haltestelle;



        private static void Connect(Location von, BlockSegment zu)
        {
            zu.BlocksPrevious.AddRange(von.BlockSegments);

            foreach (BlockSegment v in von.BlockSegments)
            {
                v.BlocksNext.Add(zu);
            }
        }

        private static void Connect(BlockSegment von, Location zu)
        {
            von.BlocksNext.AddRange(zu.BlockSegments);
            foreach (BlockSegment v in zu.BlockSegments)
            {
                v.BlocksPrevious.Add(von);
            }
        }

        private static void Connect(BlockSegment von, BlockSegment zu)
        {
            von.BlocksNext.Add(zu);
            zu.BlocksPrevious.Add(von);
        }


        private static BlockSegment GetG(string name)
        {
            return new BlockSegment() { Name = name };
        }

        public static BlockSegment weiche5;

        public static BlockSegment CreateExampleStrecke3in1()
        {


            var wbG3 = GetG("wb_Gleis1");
            WestBahnhof = new Location(Constants.LOCATION_NAMES.WESTBAHNHOF)
            {
                BlockSegments = new List<BlockSegment>() { GetG("wb_Gleis3"), GetG("wb_Gleis2"), wbG3 }
            };

            var strecke1 = GetG("Strecke1");
            Connect(WestBahnhof, strecke1);
            
            var weiche3 = GetG("weiche3");
            Connect(strecke1, weiche3);
            
            Haltestelle = new Location(Constants.LOCATION_NAMES.HALTESTELLE)
            {
                BlockSegments = new List<BlockSegment>() { GetG("ht_Gleis1"), GetG("ht_Gleis2") }
            };
            Connect(weiche3, Haltestelle);            

            weiche5 = new BlockSegment() { Name = "weiche5" };
            Connect(Haltestelle, weiche5);

            weiche5.Train = new Model.Train.Train() { CurrentBlockSegment = weiche5, Name = "Blocker", Richtung = new BewegungsRichtung() { RichtungTyp = BewegungsRichtungTyp.STOP } };
            
            var gerade6 = new BlockSegment() { Name = "gerade6" };
            Connect(weiche5, gerade6);
            
            var Haltestelle2 = new Location(Constants.LOCATION_NAMES.HALTESTELLE_2)
            {
                BlockSegments = new List<BlockSegment>() { GetG("ht2_Gleis1"), GetG("ht2_Gleis2"), }
            };
            Connect(gerade6, Haltestelle2);

            var gleisVorfeld = new BlockSegment() { Name = "gleisVorfeld" };
            Connect(Haltestelle2, gleisVorfeld);            

            var obGl3 = GetG("ob_Gleis3");
            OstBahnhof = new Location(Constants.LOCATION_NAMES.OSTBAHNHOF)
            {
                BlockSegments = new List<BlockSegment>() { GetG("ob_Gleis1"), GetG("ob_Gleis2"), obGl3 }
            };
            Connect(gleisVorfeld, OstBahnhof);

            var ausweich1 = GetG("ausweich1");
            var ausweich2 = GetG("ausweich2");
            var ausweich3 = GetG("ausweich3");

            Connect(WestBahnhof, ausweich1);
            Connect(ausweich1, ausweich2);
            Connect(ausweich2, ausweich3);
            Connect(ausweich3, OstBahnhof);


            Gueterbahnhof = new Location(Constants.LOCATION_NAMES.OSTBAHNHOF)
            {
                BlockSegments = new List<BlockSegment>() { GetG("gb_Gleis1"), GetG("gb_Gleis2"), GetG("gb_Gleis3") }
            };

            Connect(gleisVorfeld, Gueterbahnhof);

            return WestBahnhof.BlockSegments[0];
        }


    }
}
