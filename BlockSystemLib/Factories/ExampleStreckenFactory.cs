using BlockSystemLib.Model.Block;
using BlockSystemLib.Model;

namespace BlockSystemLib.Factories
{
    public class ExampleStreckenFactory
    {
        public static Location OstBahnhof;
        public static Location Gueterbahnhof;
        public static Location WestBahnhof;
        public static Location Hafen;
        public static Location Haltestelle;



        private static Location CreateLocation(string name)
        {
            return new Location(name)
            {
                BlockSegments = new List<BlockSegment>() { new BlockSegment() { Name = name } }
            };
        }

        private static void Connect(Location von, BlockSegment zu)
        {
            zu.BlocksNext.AddRange(von.BlockSegments);
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

        private static void Init()
        {
            OstBahnhof = CreateLocation(Constants.LOCATION_NAMES.OSTBAHNHOF);
            Gueterbahnhof = CreateLocation(Constants.LOCATION_NAMES.GUETERBAHNHOF);
            Hafen = CreateLocation(Constants.LOCATION_NAMES.HAFEN);
        }


        private static BlockSegment GetG(string name)
        {
            return new BlockSegment() { Name = name };
        }

        public static BlockSegment CreateExampleStrecke3in1()
        {
            Init();

            var wb_Gleis1 = GetG("wb_Gleis1");
            var wb_Gleis2 = GetG("wb_Gleis2");
            var wb_Gleis3 = GetG("wb_Gleis3");

            WestBahnhof = new Location(Constants.LOCATION_NAMES.WESTBAHNHOF)
            {
                BlockSegments = new List<BlockSegment>() { wb_Gleis1, wb_Gleis2, wb_Gleis3 }
            };

            var strecke1 = GetG("Strecke1");
            Connect(wb_Gleis1, strecke1);
            Connect(wb_Gleis2, strecke1);
            Connect(wb_Gleis3, strecke1);

            var weiche3 = GetG("weiche3");
            Connect(strecke1, weiche3);


            var ht_Gleis1 = GetG("ht_Gleis1");
            var ht_Gleis2 = GetG("ht_Gleis2");

            Haltestelle = new Location(Constants.LOCATION_NAMES.HALTESTELLE)
            {
                BlockSegments = new List<BlockSegment>() { ht_Gleis1, ht_Gleis2 }
            };

            Connect(weiche3, ht_Gleis1);
            Connect(weiche3, ht_Gleis2);

            var weiche5 = new BlockSegment() { Name = "weiche5" };
            Connect(ht_Gleis1, weiche5);
            Connect(ht_Gleis2, weiche5);

            var gerade6 = new BlockSegment() { Name = "gerade6" };
            Connect(weiche5, gerade6);
            //Ausweichgleis ende


            //Gleisfeld
            var weiche11 = new BlockSegment() { Name = "weiche11" };
            //gerade91.AddNext(weiche11);
            Connect(gerade6, weiche11);

            Connect(weiche11, OstBahnhof);
            Connect(weiche11, Gueterbahnhof);

            return WestBahnhof.BlockSegments[0];
        }


    }
}
