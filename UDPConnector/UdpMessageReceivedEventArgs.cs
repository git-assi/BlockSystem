using System.Net;

namespace UDPConnector
{
    public class UdpMessageReceivedEventArgs : EventArgs
    {
        public string Message { get; set; } = string.Empty;
        public IPEndPoint RemoteEndPoint { get; set; } = new IPEndPoint(IPAddress.Any, 0);
    }
}
