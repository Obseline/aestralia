using AestraliaBackend.Network;

class Program
{
    static async Task Main()
    {
        var server = new WebSocketServer("http://localhost:34000/");
        var t = Task.Run(server.Listen);

        // NOTE: winux Infinite loop, `t` never returns
        await t;
    }
}
