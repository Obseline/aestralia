using AestraliaBackend.Models;

class Program
{
    static void Main(string[] args)
    {
        // Create a new map instance
        var map = new Map("Aestralia", 10, 10);

        // Display map information
        Console.WriteLine($"Map Name: {map.Name}");
        foreach (var chunk in map.Chunks)
        {
            Console.WriteLine($"Chunk at ({chunk.Coord.x}, {chunk.Coord.y})");
        }
    }
}