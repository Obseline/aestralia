using AestraliaBackend.Models;

class Program
{
    static void Main(string[] args)
    {
        // Create a new map instance
        var map = new Map("Aestralia", 10, 10);

        // Display map information
        Console.WriteLine($"Map Name: {map.Name}");
        foreach (Chunk chunk in map.Chunks)
        {
            Console.WriteLine($"Chunk at ({chunk.Coord.x}, {chunk.Coord.y})");
        }

        // Create a new male villager instance
        for (int i = 0; i < 5; i++)
        {
            var villager = new Villager(true);
            Console.WriteLine($"Male villager First Name: {villager.Identity.FirstName}");
        }

        // Create a new female villager instance
        for (int i = 0; i < 5; i++)
        {
            var villager = new Villager(false);
            Console.WriteLine($"Female villager First Name: {villager.Identity.FirstName}");
        }

    }
}
