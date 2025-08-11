using BlockSystemLib.Model.Block;

namespace BlockSystemLib.Factories
{
    public class ExampleBlockFactory
    {
        public static BlockSegment Ostbahnhof = new BlockSegment() { Name = Constants.LOCATION_NAMES.OSTBAHNHOF };
        public static BlockSegment Gueterbahnhof = new BlockSegment() { Name = Constants.LOCATION_NAMES.GUETERBAHNHOF };
        public static BlockSegment WestBahnhof = new BlockSegment() { Name = Constants.LOCATION_NAMES.WESTBAHNHOF };
        public static BlockSegment Hafen = new BlockSegment() { Name = Constants.LOCATION_NAMES.HAFEN };

        public static BlockSegment CreateExampleStrecke1()
        {

            //Ausweichgleis start
            var gerade2 = new BlockSegment() { Name = "gerade2" };
            WestBahnhof.AddNext(gerade2);

            var weiche3 = new BlockSegment() { Name = "weiche3" };
            gerade2.AddNext(weiche3);

            var gerade41 = new BlockSegment() { Name = "gerade41" };
            var gerade42 = new BlockSegment() { Name = "gerade42" };
            weiche3.AddNext(gerade41);
            weiche3.AddNext(gerade42);

            var weiche5 = new BlockSegment() { Name = "weiche5" };
            gerade41.AddNext(weiche5);
            gerade42.AddNext(weiche5);

            var gerade6 = new BlockSegment() { Name = "gerade6" };
            weiche5.AddNext(gerade6);
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
            gerade6.AddNext(weiche11);
            
            weiche11.AddNext(Ostbahnhof);
            weiche11.AddNext(Gueterbahnhof);

            return WestBahnhof;
        }

     
    }
}
