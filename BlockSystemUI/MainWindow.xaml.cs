using FontAwesome5;
using BlockSystemLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using BlockSystemLib.Model;
using BlockSystemLib.Factories;
using Microsoft.VisualBasic;

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


        private List<Train> allTrains = new List<Train>();


        private void Button_Fac_Click(object sender, RoutedEventArgs e)
        {
            ExampleBlockFactory.CreateExampleStreckeYX();
            PaintStrecke(ExampleBlockFactory.WestBahnhof, 0, 0);

        }

        //Krücken
        private List<Label> allNodes = new List<Label>();
        private List<string> allNodeNames = new List<string>();
        //Zeigt alle Strecken
        private void PaintStrecke(Block block, int col, int row)
        {

            if (allNodeNames.Contains(block.Name))
            {
                Debug.WriteLine("Contains:" + block.Name);
                return;
            }

            allNodeNames.Add(block.Name);
            string labelName = block.LabelBez + $" {col}{row}";
           
            StackPanel pnl = UIHelper.CreatePanel(col, row);

            myGrid.Children.Add(pnl);            
            pnl.Children.Add(UIHelper.CreatePic(block.BlockType));            

            Label lbl = UIHelper.CreateDefaultLabel(block.IstFrei, labelName);
            lbl.DataContext = block;
            allNodes.Add(lbl);
            pnl.Children.Add(lbl);

            col++;

            foreach (var b in block.GetNextBlocks(true))
            {                
                PaintStrecke(b, col, row++);                
            }            
        }

        private void RefreshStrecke()
        {

            foreach (var node in allNodes)
            {
                node.Background = new SolidColorBrush(((Block)node.DataContext).IstFrei ? Colors.Green : Colors.Red);
                node.Content = ((Block)node.DataContext).LabelBez;

            }



        }

        private void Button_Go_Click(object sender, RoutedEventArgs e)
        {
            var arrivedTrains = allTrains.Where(t => t.Arrived).ToList();
            allTrains = allTrains.Where(t => !t.Arrived).ToList();

            foreach (var train in arrivedTrains)
            {
                MessageBox.Show($"Angekommen{train.Name}");
                train.Leave();
            }

            foreach (var train in allTrains)
            {
                foreach (var b in train.GetNextBlocks())
                {
                    if (train.FindWay(b))
                    {
                        if (train.MoveToBlock(b))
                        {
                            break;
                        }
                    }
                }
            }

            RefreshStrecke();
        }

        private void Button_B2(object sender, RoutedEventArgs e)
        {
            var train = new Train("ICE", ExampleBlockFactory.WestBahnhof, BlockSystemLib.Constants.LOCATION_NAMES.OSTBAHNHOF);
            allTrains.Add(train);
            RefreshStrecke();
        }

        private void Button_BG(object sender, RoutedEventArgs e)
        {
            var train = new Train("V100", ExampleBlockFactory.Gueterbahnhof, BlockSystemLib.Constants.LOCATION_NAMES.WESTBAHNHOF);
            train.Vorwaerts = false;
            allTrains.Add(train);
            RefreshStrecke();
        }

        private void Button_BH(object sender, RoutedEventArgs e)
        {
            var train = new Train("Hafenbahn", ExampleBlockFactory.WestBahnhof, BlockSystemLib.Constants.LOCATION_NAMES.HAFEN);
            train.Vorwaerts = true;
            allTrains.Add(train);
            RefreshStrecke();
        }
    }
}