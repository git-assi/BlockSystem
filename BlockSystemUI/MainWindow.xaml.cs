using FontAwesome5;
using BlockSystemLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BlockSystemUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

        private void Button_Fac_Click(object sender, RoutedEventArgs e)
        {
            var strecke = BlockSystemLib.Factories.ExampleBlockFactory.CreateExampleStrecke();
            PaintStrecke(strecke);
        }

        private void PaintStrecke(Block block)
        {
            if (block == null)
                return;

            

            var pnl = new StackPanel();
            pnl.Orientation = Orientation.Vertical;
            pnl.SetValue(Grid.ColumnProperty, block.Col);
            pnl.SetValue(Grid.RowProperty, block.Row);
            myGrid.Children.Add(pnl);

            var newPic = new ImageAwesome();
            newPic.Icon = EFontAwesomeIcon.Solid_GripLinesVertical;
            //newPic.Background = block.IstFrei ? Brushes.Green : Brushes.Red;
            newPic.Rotation = 90;
            newPic.Width = 20;            
            newPic.Height = 50;            
            pnl.Children.Add(newPic);
            
            var lbl = new Label();
            lbl.Content = block.Name;
            lbl.VerticalAlignment = VerticalAlignment.Top;
            lbl.Foreground = new SolidColorBrush(Colors.White);
            lbl.Background = new SolidColorBrush(block.IstFrei ? Colors.Green : Colors.Red);
            pnl.Children.Add(lbl);

            foreach (var b in block.NextBlocks)
            {
                PaintStrecke(b);
            }

        }
    }
}