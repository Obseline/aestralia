using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace AestraliaBackend.Network
{

    /// <summary>
    /// Class <c>WebSocketServer</c> handle the clients' connections.
    /// </summary>
    /// NOTE: winux It will quickly need a way to communicate with the "core"
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
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
                        break;
                    }

                    var msg = Encoding.UTF8.GetString(buf, 0, res.Count);
                    if (msg.Trim() == "ping")
                    {
                        var response = Encoding.UTF8.GetBytes("pong");
                        await ws.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else
                    {
                        var response = Encoding.UTF8.GetBytes("nop");
                        await ws.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
            catch (Exception e)
            {
                try { await ws.CloseAsync(WebSocketCloseStatus.InternalServerError, e.ToString(), CancellationToken.None); } catch { }
            }
        }
    }

}
