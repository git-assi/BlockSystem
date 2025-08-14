using BlockSystemLib.Model.Block;

namespace BlockSystemLib.Factories
{
    public class ExampleStreckenFactory
    {
        public static BlockSegment Ostbahnhof = new BlockSegment() { Name = Constants.LOCATION_NAMES.OSTBAHNHOF };
        public static BlockSegment Gueterbahnhof = new BlockSegment() { Name = Constants.LOCATION_NAMES.GUETERBAHNHOF };
        public static BlockSegment WestBahnhof = new BlockSegment() { Name = Constants.LOCATION_NAMES.WESTBAHNHOF };
        public static BlockSegment Hafen = new BlockSegment() { Name = Constants.LOCATION_NAMES.HAFEN };

        private static void Connect(BlockSegment von, BlockSegment zu)
        {
            von.BlocksNext.Add(zu);
            zu.BlocksPrevious.Add(von);
        }

        public static BlockSegment CreateExampleStrecke1()
        {

            //Ausweichgleis start
            var gerade2 = new BlockSegment() { Name = "gerade2" };

            Connect(WestBahnhof, gerade2);

            var weiche3 = new BlockSegment() { Name = "weiche3" };
            Connect(gerade2, weiche3);

            var gerade41 = new BlockSegment() { Name = "gerade41" };
            var gerade42 = new BlockSegment() { Name = "gerade42" };
            Connect(weiche3, gerade41);
            Connect(weiche3, gerade42);

            var weiche5 = new BlockSegment() { Name = "weiche5" };
            Connect(gerade41, weiche5);
            Connect(gerade42, weiche5);

            var gerade6 = new BlockSegment() { Name = "gerade6" };
            Connect(weiche5, gerade6);
            //Ausweichgleis ende

            /*/Abzweigung Hafen start
            var gerade7 = new Block("gerade7");
            gerade6.AddNext(gerade7);

            var weiche8 = new Block("weiche8");
            gerade7.AddNext(weiche8);

            var gerade91 = new Block("gerade91");
            var gerade92 = new Block("gerade92");
            weiche8.AddNext(gerade91);
            weiche8.AddNext(gerade92);

            var hafen = new Block(Constants.LOCATION_NAMES.HAFEN);
            gerade92.AddNext(hafen);

            var gerade10 = new Block("gerade10");
            gerade91.AddNext(gerade10);

            //Abzweigung Hafen ende*/

            //Gleisfeld
            var weiche11 = new BlockSegment() { Name = "weiche11" };
            //gerade91.AddNext(weiche11);
            Connect(gerade6, weiche11);

            Connect(weiche11, Ostbahnhof);
            Connect(weiche11, Gueterbahnhof);

            return WestBahnhof;
        }

        private static BlockSegment GetG(string name)
        {
            return new BlockSegment() { Name = name };
        }

        public static BlockSegment CreateExampleStrecke3in1()
        {
            var start1 = GetG("Start1");
            var start2 = GetG("Start2");
            var start3 = GetG("Start3");

            Connect(WestBahnhof, start1);
            Connect(WestBahnhof, start2);
            Connect(WestBahnhof, start3);

            var strecke1 = GetG("Strecke1");
            Connect(start1, strecke1 );
            Connect(start2, strecke1);
            Connect(start3, strecke1);

            var weiche3 = GetG("weiche3");
            Connect(strecke1, weiche3);

            var gerade41 = new BlockSegment() { Name = "gerade41" };
            var gerade42 = new BlockSegment() { Name = "gerade42" };
            Connect(weiche3, gerade41);
            Connect(weiche3, gerade42);

            var weiche5 = new BlockSegment() { Name = "weiche5" };
            Connect(gerade41, weiche5);
            Connect(gerade42, weiche5);

            var gerade6 = new BlockSegment() { Name = "gerade6" };
            Connect(weiche5, gerade6);
            //Ausweichgleis ende

            
            //Gleisfeld
            var weiche11 = new BlockSegment() { Name = "weiche11" };
            //gerade91.AddNext(weiche11);
            Connect(gerade6, weiche11);

            Connect(weiche11, Ostbahnhof);
            Connect(weiche11, Gueterbahnhof);

            return WestBahnhof;
        }


    }
}
