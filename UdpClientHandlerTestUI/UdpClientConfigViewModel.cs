using System.ComponentModel;
using UDPConnector;

namespace UdpClientHandlerTestUI
{
    public class UdpClientConfigViewModel(UdpClientConfig config) : INotifyPropertyChanged
    {
        private UdpClientConfig _config = config;

        public UdpClientConfig Config { get => _config; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _portSend;
        private int _portFetch;     

        public string PortSend
        {
            get
            {
                return _portSend.ToString();
            }
            set
            {
                if (int.TryParse(value, out _portSend))
                {
                    _config.SendPort = _portSend;
                    NotifyPropertyChanged(nameof(PortSend));
                }
            }
        }
        public string PortFetch
        {
            get
            {
                return _portFetch.ToString();
            }
            set
            {
                if (int.TryParse(value, out _portFetch))
                {
                    _config.FetchPort = _portFetch;
                    NotifyPropertyChanged(nameof(PortFetch));
                }
            }
        }

    }
}
