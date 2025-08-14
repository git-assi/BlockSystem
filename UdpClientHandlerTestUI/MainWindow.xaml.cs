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

        private UdpClientConfigViewModel _dataContext
        {
            get
            {
                return (UdpClientConfigViewModel)this.DataContext;
            }
        }

        private static UdpClientHandler utpHandler;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                utpHandler.SendAsync(_dataContext.Text, _dataContext.Empfaegner, _dataContext.Config.SendPort);
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
                utpHandler = new UdpClientHandler(_dataContext.Config);
                utpHandler.ReceiveAsync();
                utpHandler.MessageReceived += UtpHandler_MessageReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UtpHandler_MessageReceived(object? sender, UdpMessageReceivedEventArgs e)
        {
            try
            {
                _dataContext.Message = e.Message;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
