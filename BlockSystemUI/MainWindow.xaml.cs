using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using BlockSystemLib.Model;
using BlockSystemLib.Factories;
using BlockSystemLib.Model.Block;
using BlockSystemLib.Model.Train;

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
            
        }


        private List<BlockSystemLib.Model.Train.Train> allTrains = new List<BlockSystemLib.Model.Train.Train>();


        private void Button_Fac_Click(object sender, RoutedEventArgs e)
        {
            ExampleStreckenFactory.CreateExampleStrecke3in1();
            PaintStrecke(new (ExampleStreckenFactory.WestBahnhof), 0, 0);

        }

        //Krücken
        private List<Label> allNodes = new List<Label>();
        private List<string> allNodeNames = new List<string>();
        
        //Zeigt alle Strecken
        private void PaintStrecke(BlockViewModel block, int col, int row)
        {

            if (allNodeNames.Contains(block.Name))
            {
                Debug.WriteLine("Contains:" + block.Name);
                return;
            }

            allNodeNames.Add(block.Name);
            string labelName = block.Name + $" {col}{row}";


            StackPanel pnl = UIHelper.CreatePanel(col, row);

            myGrid.Children.Add(pnl);
            pnl.Children.Add(UIHelper.CreatePic(block.BlockType));

            Label lbl = UIHelper.CreateDefaultLabel(block.IstFrei, labelName);
            lbl.DataContext = block;
            allNodes.Add(lbl);
            pnl.Children.Add(lbl);

            col++;

            foreach (var b in block.GetNextBlocksPainting())
            {
                PaintStrecke(new BlockViewModel(b), col, row++);
                
            }
        }

        private void RefreshStrecke()
        {
            foreach (var node in allNodes)
            {
                node.Background = new SolidColorBrush(((BlockViewModel)node.DataContext).IstFrei ? Colors.Green : Colors.Red);
                node.Content = ((BlockViewModel)node.DataContext).Label;
            }
        }

        private void Button_Go_Click(object sender, RoutedEventArgs e)
        {
            var ctrl = new TrainController();

            var arrivedTrains = allTrains.Where(t => t.Arrived).ToList();
            allTrains = allTrains.Where(t => !t.Arrived).ToList();

            foreach (var train in arrivedTrains)
            {
                MessageBox.Show($"Angekommen{train.Name}");
                train.Richtung = BewegungsRichtung.Stop;
                train.CurrentLocation.Train = null;
            }

            foreach (var train in allTrains)
            {
                foreach (var b in ctrl.GetNextPossibleBlocks(train.Richtung, train.CurrentLocation))
                {
                    if (ctrl.FindWay(train.Destination, b, train.Richtung))
                    {
                        if (ctrl.MoveToBlock(train, b))
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
            allTrains.Add(new Train()
            {
                CurrentLocation = ExampleStreckenFactory.WestBahnhof,
                Name = "ICE",
                Destination = BlockSystemLib.Constants.LOCATION_NAMES.OSTBAHNHOF,
                Richtung = BewegungsRichtung.Vorwärts,
            });
            RefreshStrecke();
        }

        private void Button_BG(object sender, RoutedEventArgs e)
        {           
            allTrains.Add(new Train()
            {
                CurrentLocation = ExampleStreckenFactory.Gueterbahnhof,
                Name = "V100",
                Destination = BlockSystemLib.Constants.LOCATION_NAMES.WESTBAHNHOF,
                Richtung = BewegungsRichtung.Rückwärts,
            });            
            RefreshStrecke();
        }

        private void Button_BH(object sender, RoutedEventArgs e)
        {           
            allTrains.Add(new Train()
            {
                CurrentLocation = ExampleStreckenFactory.WestBahnhof,
                Name = "Hafenbahn",
                Destination = BlockSystemLib.Constants.LOCATION_NAMES.HAFEN,
                Richtung = BewegungsRichtung.Vorwärts,
            });
            RefreshStrecke();
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            switch(((Button)sender).Content.ToString())
            {
                case "1":
                    allTrains.Add(new Train()
                    {
                        CurrentLocation = ExampleStreckenFactory.WestBahnhof.BlocksNext[0],
                        Name = "ICE1",
                        Destination = BlockSystemLib.Constants.LOCATION_NAMES.OSTBAHNHOF,
                        Richtung = BewegungsRichtung.Vorwärts,
                    });
                    break;

                case "2":
                    allTrains.Add(new Train()
                    {
                        CurrentLocation = ExampleStreckenFactory.WestBahnhof.BlocksNext[1],
                        Name = "ICE2",
                        Destination = BlockSystemLib.Constants.LOCATION_NAMES.OSTBAHNHOF,
                        Richtung = BewegungsRichtung.Vorwärts,
                    });
                    break;

                case "3":
                    allTrains.Add(new Train()
                    {
                        CurrentLocation = ExampleStreckenFactory.WestBahnhof.BlocksNext[2],
                        Name = "ICE3",
                        Destination = BlockSystemLib.Constants.LOCATION_NAMES.OSTBAHNHOF,
                        Richtung = BewegungsRichtung.Vorwärts,
                    });
                    break;
            }
           
            RefreshStrecke();
        }
    }
}