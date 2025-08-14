using System.Windows;
using UDPConnector;

namespace UdpClientHandlerTestUI
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


        private static UdpClientHandler utpHandler;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                utpHandler.SendAsync("test", "127.0.0.1", ((UdpClientConfigViewModel)DataContext).Config.SendPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Init_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var config = new UdpClientConfig();
                DataContext = new UdpClientConfigViewModel(config);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click_Listen(object sender, RoutedEventArgs e)
        {
            try
            {
                utpHandler = new UdpClientHandler(((UdpClientConfigViewModel)DataContext).Config);
                utpHandler.ReceiveAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
