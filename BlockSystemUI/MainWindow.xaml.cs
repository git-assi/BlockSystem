using FontAwesome5;
using BlockSystemLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using BlockSystemLib.Model;

namespace BlockSystemUI
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //BlockXx x = new BlockXx();

                var wantedNode = myGrid.FindName(trn.Name);
                if (wantedNode is ImageAwesome)
                {
                    ImageAwesome wantedChild = wantedNode as ImageAwesome;
                    wantedChild.Foreground = Brushes.Blue;
                }

                int r = Grid.GetRow(trn);
                int c = Grid.GetColumn(trn);
                trn.SetValue(Grid.ColumnProperty, ++c);

            }
            catch
            {

            }
        }


        private Train train;
        private void Button_Fac_Click(object sender, RoutedEventArgs e)
        {
            var strecke = BlockSystemLib.Factories.ExampleBlockFactory.CreateExampleStreckeY();
            PaintStrecke(strecke, 0, 0);
            train = new Train("V100", strecke);
            train.Destination = "Bahnhof2";




        }

        //Zeigt alle Strecken
        private void PaintStrecke(Block block, int col, int row)
        {

            string blockName = block.BlockType + $" {col}{row} {block.Name}";

            Debug.WriteLine("In " + blockName);
            if (block == null)
                return;

            StackPanel pnl = UIHelper.CreatePanel(col, row);

            myGrid.Children.Add(pnl);
            ImageAwesome newPic = UIHelper.CreatePic(block);
            pnl.Children.Add(newPic);

            Label lbl = UIHelper.CreateDefaultLabel(block, blockName);
            pnl.Children.Add(lbl);

            lbl.DataContext = block;

            col++;
            foreach (var b in block.NextBlocks)
            {
                PaintStrecke(b, col, row++);
            }
            Debug.WriteLine("Out " + blockName);

        }

        private void Button_Go_Click(object sender, RoutedEventArgs e)
        {
            while (train.Arrived == false)
            {
                foreach (var b in train.CurrentBlock.NextBlocks)
                {
                    if (train.FindWay(b))
                    {
                        train.MoveToBlock(b);
                        break;
                    }
                }
            }
        }
    }
}