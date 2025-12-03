using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AestraliaFrontend.Networking
{
    public class NetworkClient
    {
        private readonly ClientWebSocket _ws = new ClientWebSocket();
        private readonly Uri _uri;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        /// <summary>
        /// Gets all received messages from the server
        /// Example of usage:
        /// - In Game1 class : Network.OnMessageReceived += msg => { Whatever you want };
        /// - In Other classes : Game.Network.OnMessageReceived += msg => { Whatever you want };
        /// </summary>
        public event Action<string> OnMessageReceived;

        public NetworkClient(string url)
        {
            _uri = new Uri(url);
        }

        public async Task ConnectAsync()
        {
            if (_ws.State == WebSocketState.Open)
                return;

            await _ws.ConnectAsync(_uri, CancellationToken.None);
            _ = ListenLoopAsync();
        }

        /// <summary>
        /// Sends a message to the server
        /// Example of usage:
        /// - In Game1 class: await Network.SendMessageAsync("ping");
        /// - In other classes: await Game.Network.SendMessageAsync("ping");
        /// </summary>
        public async Task SendMessageAsync(string message)
        {
            try {
                if (_ws.State != WebSocketState.Open)
                await ConnectAsync();
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                await _ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            } catch (Exception ex) {
                Console.WriteLine("Websocket sending error: " + ex.Message);
            }
        }

        private async Task ListenLoopAsync()
        {
            var buffer = new byte[2048];

            try {
                while (!_cts.Token.IsCancellationRequested && _ws.State == WebSocketState.Open)
                {
                    var result = await _ws.ReceiveAsync(buffer, _cts.Token);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                        break;
                    }

                    string msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    OnMessageReceived?.Invoke(msg);
                }
            } catch (Exception ex) {
                Console.WriteLine("WebSocket listening error: " + ex.Message);
            }
        }

        public void Disconnect()
        {
            _cts.Cancel();
            if (_ws.State == WebSocketState.Open)
                _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnected", CancellationToken.None);
        }
    }
}