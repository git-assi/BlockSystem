using FontAwesome5;

using BlockSystemLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using BlockSystemLib.Model;
using System.Windows.Ink;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Metadata;

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
                
                /*/BlockXx x = new BlockXx();

                var wantedNode = myGrid.FindName(trn.Name);
                if (wantedNode is ImageAwesome)
                {
                    ImageAwesome wantedChild = wantedNode as ImageAwesome;
                    wantedChild.Foreground = Brushes.Blue;
                }

                int r = Grid.GetRow(trn);
                int c = Grid.GetColumn(trn);
                trn.SetValue(Grid.ColumnProperty, ++c);
                */
            }
            catch
            {

            }
        }


        private Train train;
        private Block strecke;

        private void Button_Fac_Click(object sender, RoutedEventArgs e)
        {
            strecke = BlockSystemLib.Factories.ExampleBlockFactory.CreateExampleStreckeYX();
            PaintStrecke(strecke, 0, 0);
          
        }

        //Krücken
        private List<Label> allNodes = new List<Label>();
        private List<string> allNodeNames = new List<string>();
        //Zeigt alle Strecken
        private void PaintStrecke(Block block, int col, int row)
        {

            if (allNodeNames.Contains(block.Name))
            {
                return;
            }
            allNodeNames.Add(block.Name);
            string blockName = block.Name + $" {col}{row}";

            Debug.WriteLine("In " + blockName);
            if (block == null)
                return;

            StackPanel pnl = UIHelper.CreatePanel(col, row);

            myGrid.Children.Add(pnl);
            ImageAwesome newPic = UIHelper.CreatePic(block);
            pnl.Children.Add(newPic);


            Label lbl = UIHelper.CreateDefaultLabel(block, blockName);
            allNodes.Add(lbl);
            pnl.Children.Add(lbl);

            lbl.DataContext = block;

            col++;
            foreach (var b in block.NextBlocks)
            {
                PaintStrecke(b, col, row++);
            }
            Debug.WriteLine("Out " + blockName);
        }

        private void RefreshStrecke()
        {

            foreach (var node in allNodes)
            {
                if (node is Label)
                {
                    Label wantedChild = node as Label;
                    wantedChild.Background = new SolidColorBrush((wantedChild.DataContext as Block).IstFrei ? Colors.Green : Colors.Red);
                }
            }



        }

        private void Button_Go_Click(object sender, RoutedEventArgs e)
        {
            if (train.Arrived)
            {
                MessageBox.Show("Angekommen");
            }

            foreach (var b in train.CurrentBlock.NextBlocks)
            {
                if (train.FindWay(b))
                {
                    train.MoveToBlock(b);
                    RefreshStrecke();
                    break;
                }
            }
        }

        private void Button_B2(object sender, RoutedEventArgs e)
        {
            train = new Train("V100", strecke);
            train.Destination = BlockSystemLib.Constants.LOCATION_NAMES.OSTBAHNHOF;
            RefreshStrecke();
        }

        private void Button_BG(object sender, RoutedEventArgs e)
        {
            train = new Train("V100", strecke);
            train.Destination = BlockSystemLib.Constants.LOCATION_NAMES.GUETERBAHNHOF;
            RefreshStrecke();
        }
    }
}