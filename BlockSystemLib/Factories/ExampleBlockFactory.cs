using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockSystemLib.Factories
{
    public class ExampleBlockFactory
    {
        public static Block CreateExampleStrecke()
        {
            int col = 0;
            Block startBlock = new Block(++col);

            var aktBlock = new Block(++col);
            startBlock.AddNext(aktBlock);

            var nextBlock = new Block(++col);
            aktBlock.AddNext(nextBlock);
            var nextBlock2 = new Block(col);
            nextBlock2.IstFrei = false;
            aktBlock.AddNext(nextBlock2);


            var nextBlock3 = new Block(++col);
            nextBlock.AddNext(nextBlock3);
            nextBlock2.AddNext(nextBlock3);

            var nextBlock4 = new Block(++col);
            nextBlock3.AddNext(nextBlock4);


            return startBlock;
        }
    }
}
