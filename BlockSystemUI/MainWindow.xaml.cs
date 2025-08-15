using BlockSystemLib.Factories;
using BlockSystemLib.Model;
using BlockSystemLib.Model.Block;
using BlockSystemLib.Model.Train;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BlockSystemUI
{

    public partial class MainWindow : Window
    {
        private TrainCollection _trainCollection;
        private TrainController _trainController;
        public MainWindow()
        {
            InitializeComponent();

            _trainCollection = new();
            _trainCollection.TrainAdded += _trainCollection_TrainAdded;

            _trainController = new();
        }

        private void _trainCollection_TrainAdded(object? sender, TrainAddedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Fac_Click(object sender, RoutedEventArgs e)
        {
            ExampleStreckenFactory.CreateExampleStrecke3in1();
            for (int i = 0; i < ExampleStreckenFactory.WestBahnhof.BlockSegments.Count; i++)
            {
                PaintStrecke(new(ExampleStreckenFactory.WestBahnhof.BlockSegments[i]), 0, i);
            }


        }

        private List<Label> allNodes = new List<Label>();
        private List<string> allNodeNames = new List<string>();


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
                BlockViewModel viewModel = ((BlockViewModel)node.DataContext);

                node.Background = new SolidColorBrush(viewModel.IstFrei ? Colors.Green : Colors.Red);
                node.Content = viewModel.Label;
            }
        }

        private void Button_Go_Click(object sender, RoutedEventArgs e)
        {
            foreach (var train in _schattenBahnhof.Trains())
            {
                if (train.StartLocation.BlockSegments.Any(s => s.IstFrei))
                {
                    _schattenBahnhof.ExitSchattenBahnhof(train, train.StartLocation.BlockSegments.First(s => s.IstFrei));
                }
                else
                {
                    Debug.WriteLine($"{train.Name} verbleibt im SB");
                }

            }

            foreach (var train in _trainCollection.NewTrains())
            {
                train.Richtung = _trainController.FindRichtung(train.NächsterHalt, train.CurrentBlockSegment);
                Debug.WriteLine($"{train.Name} neu: {train.Richtung}");
            }

            foreach (var train in _trainCollection.ArrviedTrains())
            {
                Debug.WriteLine($"Angekommen{train.Name}");
                train.DestinationReached();
            }

            foreach (var train in _trainCollection.RollingTrains())
            {
                foreach (var possibleNextBlock in _trainController.GetNextPossibleBlocks(train.Richtung, train.CurrentBlockSegment))
                {
                    if (_trainController.FindWay(train.NächsterHalt, possibleNextBlock, train.Richtung))
                    {
                        if (_trainController.MoveToBlock(train, possibleNextBlock))
                        {
                            break;
                        }
                    }
                }
            }



            RefreshStrecke();
        }

        private SchattenBahnhof _schattenBahnhof = new();

        private void Button_B2(object sender, RoutedEventArgs e)
        {
            var train = _schattenBahnhof.CreateTrain("ICE Neu", ExampleStreckenFactory.Gueterbahnhof, ExampleStreckenFactory.WestBahnhof);
            _trainCollection.Add(train);
        }

        private void Button_BG(object sender, RoutedEventArgs e)
        {
            var train = _schattenBahnhof.CreateTrain("V100", ExampleStreckenFactory.WestBahnhof, ExampleStreckenFactory.OstBahnhof);
            train.AddZwischenStop(ExampleStreckenFactory.Haltestelle);
            _trainCollection.Add(train);
        }

        private void Button_BH(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            Train train;
            switch (((Button)sender).Content.ToString())
            {
                case "1":
                    train = _schattenBahnhof.CreateTrain("ICE1", ExampleStreckenFactory.OstBahnhof, ExampleStreckenFactory.WestBahnhof);
                    train.AddZwischenStop(ExampleStreckenFactory.Haltestelle);
                    _trainCollection.Add(train);
                    break;

                case "2":
                    train = _schattenBahnhof.CreateTrain("ICE2", ExampleStreckenFactory.OstBahnhof, ExampleStreckenFactory.WestBahnhof);
                    train.AddZwischenStop(ExampleStreckenFactory.Haltestelle);
                    _trainCollection.Add(train);
                    break;

                case "3":
                    train = _schattenBahnhof.CreateTrain("ICE3", ExampleStreckenFactory.OstBahnhof, ExampleStreckenFactory.WestBahnhof);
                    _trainCollection.Add(train);
                    break;

                default:
                    return;
            }
        }

        private System.Timers.Timer _timer;
        private Dispatcher _dispatcher;

        public void StartMyTimer()
        {
            _timer = new(1000); // 1000 ms = 1 Sekunde
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        public void StopMyTimer()
        {

            _timer.Stop();
            _timer.AutoReset = false; // Wiederholt sich automatisch
            _timer = null;
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Button_Go_Click(this, new RoutedEventArgs());
            });
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        { 
            StartMyTimer();
        }

        private void Button_Click_Stop(object sender, RoutedEventArgs e)
        {
            StopMyTimer();
        }
    }
}