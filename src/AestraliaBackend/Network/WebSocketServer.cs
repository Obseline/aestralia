using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace AestraliaBackend.Network
{

    /// <summary>
    /// Class <c>Village</c> models village on our <see>Map</see>.
    /// </summary>
    public class WebSocketServer
    {
        static HttpListener _http_listener = new HttpListener();

        public WebSocketServer(string uri)
        {
            _http_listener.Prefixes.Add(uri);
        }

        public async Task Listen()
        {
            _http_listener.Start();

            while (true)
            {
                var ctx = await _http_listener.GetContextAsync();
                if (!ctx.Request.IsWebSocketRequest)
                {
                    ctx.Response.StatusCode = 400;
                    ctx.Response.Close();
                    continue;
                }

                var wsCtx = await ctx.AcceptWebSocketAsync(null);
                _ = Handle(wsCtx.WebSocket);
            }
        }

        static async Task Handle(WebSocket ws)
        {
            var buf = new byte[1024];
            try
            {
                while (ws.State == WebSocketState.Open)
                {
                    var res = await ws.ReceiveAsync(new ArraySegment<byte>(buf), CancellationToken.None);
                    if (res.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None);
                        break;
                    }

                    var msg = Encoding.UTF8.GetString(buf, 0, res.Count);
                    Console.WriteLine("Received: " + msg);
                    var outBytes = Encoding.UTF8.GetBytes(msg);
                    await ws.SendAsync(new ArraySegment<byte>(outBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                try { await ws.CloseAsync(WebSocketCloseStatus.InternalServerError, "err", CancellationToken.None); } catch { }
            }
        }
    }

}
