
namespace BlockSystemLib.Factories
{
    public class ExampleBlockFactory
    {
        public static Block CreateExampleStrecke()
        {
            Block startBlock = new Block();

            var aktBlock = new Block();
            startBlock.AddNext(aktBlock);

            var nextBlock = new Block();
            aktBlock.AddNext(nextBlock);
            var nextBlock2 = new Block();
            nextBlock2.IstFrei = false;
            aktBlock.AddNext(nextBlock2);


            var nextBlock3 = new Block();
            nextBlock.AddNext(nextBlock3);
            nextBlock2.AddNext(nextBlock3);

            var nextBlock4 = new Block();
            nextBlock3.AddNext(nextBlock4);

            return startBlock;
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
    }
}
