
namespace BlockSystemLib.Factories
{
    public class ExampleBlockFactory
    {
        public static Block CreateExampleStrecke()
        {
            Block gerade00 = new Block("Start");

            var weiche1 = new Block("Weiche 1");
            gerade00.AddNext(weiche1);

            var gerade1 = new Block("Gerade 1");
            weiche1.AddNext(gerade1);
            
            var gerade2 = new Block("Gerade 2");
            gerade2.IstFrei = false;
            weiche1.AddNext(gerade2);

            var weiche2 = new Block("Weiche 2");
            gerade1.AddNext(weiche2);
            gerade2.AddNext(weiche2);

            var nextBlock4 = new Block("Ende");
            weiche2.AddNext(nextBlock4);

            return gerade00;
        }

        public static Block CreateExampleStrecke2()
        {            
            Block startBlock = new Block();

            var aktBlock = new Block();
            startBlock.AddNext(aktBlock);
            
            var nextBlock = new Block();
            aktBlock.AddNext(nextBlock);
            var nextBlock2 = new Block();
            nextBlock2.IstFrei = false;
            aktBlock.AddNext(nextBlock2);

            var nextBlock3Abzweigung = new Block();
            aktBlock.AddNext(nextBlock3Abzweigung);

            nextBlock3Abzweigung.AddNext(new Block( ));

            var nextBlock3 = new Block();
            nextBlock.AddNext(nextBlock3);
            nextBlock2.AddNext(nextBlock3);

            var nextBlock4 = new Block();
            nextBlock3.AddNext(nextBlock4);


            return startBlock;
        }

        public static Block CreateExampleStreckeY()
        {
            Block bahnhof1Block = new Block("Bahnhof1");

            var gerade1Block = new Block("gerade1");
            bahnhof1Block.AddNext(gerade1Block);

            var weiche1Block = new Block("weiche");
            gerade1Block.AddNext(weiche1Block);

            var gerade12Block = new Block("gerade12");
            var gerade22Block = new Block("gerade22");
            weiche1Block.AddNext(gerade12Block);
            weiche1Block.AddNext(gerade22Block);

            Block bahnhof2Block = new Block("Bahnhof2");
            Block gBahnhofBlock = new Block("Güterbahnhof");

            gerade12Block.AddNext(bahnhof2Block);
            gerade22Block.AddNext(gBahnhofBlock);

            return bahnhof1Block;
        }
    }
}
