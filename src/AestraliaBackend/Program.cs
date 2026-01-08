using AestraliaBackend.Models;
using AestraliaBackend.Network;
using Microsoft.Extensions.Logging;

class Program
{
    static async Task Main()
    {
        Log.Configure(LoggerFactory.Create(builder => builder.AddConsole()), "AestraliaBackend");

        var server = new WebSocketServer("http://localhost:34000/");
        var t = Task.Run(server.Listen);

        var map = new Map("Aestralia", 100, 100);
        map.ToPpm("aestralia01");
        map.SpawnVillages(15);
        map.ToPpm("aestralia02");

        // NOTE: winux Infinite loop, `t` never returns
        await t;
    }
}
