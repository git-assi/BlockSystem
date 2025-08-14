using System.ComponentModel;
using UDPConnector;

namespace UdpClientHandlerTestUI
{
    public class UdpClientConfigViewModel : INotifyPropertyChanged
    {

        private UdpClientConfig _config;

        public UdpClientConfigViewModel(UdpClientConfig config)
        {
            _config = config;
            Empfaegner = "127.0.0.1";
            PortSend = "11000";
            PortFetch = "10000";
        }

        public UdpClientConfig Config { get => _config; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _portSend = 11000;
        private int _portFetch = 10000;

        private string _message = string.Empty;

        public string Message
        {
            get
            {
                return _message.ToString();
            }
            set
            {

                _message = value;
                NotifyPropertyChanged(nameof(Message));

            }
        }

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

        private string _empfaegner = string.Empty;
        public string Empfaegner
        {
            get
            {
                return _empfaegner.ToString();
            }
            set
            {
                _empfaegner = value;
                NotifyPropertyChanged(nameof(Empfaegner));
            }
        }
        private string _text = "Hello World";
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                NotifyPropertyChanged(nameof(Text));
            }
        }

    }
}
