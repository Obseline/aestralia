using Microsoft.Extensions.Logging;

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
        static readonly HttpListener _http_listener = new();

        public WebSocketServer(string uri)
        {
            _http_listener.Prefixes.Add(uri);
        }

        public async Task Listen()
        {
            _http_listener.Start();

            while (true)
            {
                HttpListenerContext ctx = await _http_listener.GetContextAsync();
                if (!ctx.Request.IsWebSocketRequest)
                {
                    ctx.Response.StatusCode = 400;
                    ctx.Response.Close();
                    Log.Logger.LogInformation("Non websocket client connection received from {ip}", ctx.Request.RemoteEndPoint);
                    continue;
                }

                HttpListenerWebSocketContext wsCtx = await ctx.AcceptWebSocketAsync(null);
                _ = Handle(wsCtx.WebSocket);
                Log.Logger.LogInformation("Client connected ({ip})", ctx.Request.RemoteEndPoint);
            }
        }

        static async Task Handle(WebSocket ws)
        {
            byte[] buf = new byte[1024];
            try
            {
                while (ws.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult res = await ws.ReceiveAsync(new ArraySegment<byte>(buf), CancellationToken.None);
                    if (res.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
                        Log.Logger.LogInformation("Client disconnected");
                        break;
                    }

                    string msg = Encoding.UTF8.GetString(buf, 0, res.Count);
                    if (msg.Trim() == "ping")
                    {
                        byte[] response = Encoding.UTF8.GetBytes("pong");
                        await ws.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else
                    {
                        byte[] response = Encoding.UTF8.GetBytes("nop");
                        await ws.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Logger.LogWarning("Client died unexpectedly: {e}", e);
                try { await ws.CloseAsync(WebSocketCloseStatus.InternalServerError, "Client died unexpectedly", CancellationToken.None); }
                catch (Exception nested_e)
                {
                    Log.Logger.LogError("Failed to close client: {e}", nested_e);
                }
            }
        }
    }

}
