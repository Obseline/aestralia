namespace AestraliaBackend.Models
{
    /// <summary>
    /// Class <c>Map</c> models the world.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Name of the map/world.
        /// </summary>
        public string Name;
        /// <summary>
        /// TODO
        /// </summary>
        public Chunk[] Chunks;

        /// <summary>
        /// Constructor, initialize a <c>Map</c> with blank <see>Chunk</see>s.
        /// </summary>
        public Map(string name, int width, int height)
        {
            Name = name;
            Chunks = Enumerable.Range(0, height)
                    .Select(h => Enumerable.Range(0, width).Select(w => (w, h)))
                    .SelectMany(inner => inner)
                    .Select(t => new Coord { x = t.w, y = t.h })
                    // Weird hack below, cant figure it out :shrug:
                    .Select(coord => new Chunk(coord) { Coord = coord })
                    .ToArray();

            // // Imperative version, we need to pick one.
            // Chunks = new Chunk[width * height];
            // foreach (var h in Enumerable.Range(0, height))
            // {
            //     foreach (var w in Enumerable.Range(0, width))
            //     {
            //         Chunks[h * width + w] = new Chunk(new Coord { x = w, y = h })
            //         {
            //             Coord = new Coord { x = w, y = h }
            //         };
            //     }
            // }

        }
    }
}
