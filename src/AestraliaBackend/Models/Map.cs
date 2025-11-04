namespace AestraliaBackend.Models
{
    // TODO: Documentation
    public class Map
    {
        // TODO: Documentation
        public string Name;
        // TODO: Documentation
        // This one is tough!
        public Chunk[] Chunks;

        // TODO: Documentation
        public Map(string name, Int32 width, Int32 height)
        {
            Name = name;
            Chunks = Enumerable.Range(0, height)
                    .Select(h => Enumerable.Range(0, width).Select(w => (w, h)))
                    .SelectMany(inner => inner)
                    .Select(t => new Coords { x = t.Item1, y = t.Item2 })
                    // Weird hack below, cant figure it out :shrug:
                    .Select(coords => new Chunk(coords) { Coords = coords })
                    .ToArray();

            // Chunks = new Chunk[width * height];
            // foreach (var h in Enumerable.Range(0, height))
            // {
            //     foreach (var w in Enumerable.Range(0, width))
            //     {
            //         Chunks[h * width + w] = new Chunk(new Coords { x = w, y = h })
            //         {
            //             Coords = new Coords { x = w, y = h }
            //         };
            //     }
            // }

        }
    }
}
