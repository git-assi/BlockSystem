using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPConnector
{

    public class UdpClientHandler
    {
        private readonly UdpClient _udpClient;
        private readonly UdpClientConfig _config;       

        public UdpClientHandler(UdpClientConfig config)
        {
            _config = config;
            _udpClient = new UdpClient(_config.FetchPort);
        }

        public async Task SendAsync(string message, string ipAddress, int port)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            await _udpClient.SendAsync(data, data.Length, ipAddress, port);
            Debug.WriteLine($"Gesendet: {message} an {ipAddress}:{port}");
        }

        public async Task ReceiveAsync()
        {
            Debug.WriteLine($"Listening {_config.FetchPort}");
            while (true)
            {
                UdpReceiveResult result = await _udpClient.ReceiveAsync();
                string receivedMessage = Encoding.UTF8.GetString(result.Buffer);
                Debug.WriteLine($"Empfangen von {result.RemoteEndPoint}: {receivedMessage}");
            }
        }

        public void Close()
        {
            _udpClient.Close();
        }
    }
}