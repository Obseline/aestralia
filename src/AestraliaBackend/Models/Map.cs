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
        /// Models the "cells" of the map.
        /// </summary>
        /// This 1 dimensional list will look like this:
        /// <code>
        /// [
        /// {x = 0, y = 0 }, {x = 1, y = 0 }, {x = 2, y = 0 }, {x = 3, y = 0 }
        /// {x = 0, y = 1 }, {x = 1, y = 1 }, {x = 2, y = 1 }, {x = 3, y = 1 }
        /// {x = 0, y = 2 }, {x = 1, y = 2 }, {x = 2, y = 2 }, {x = 3, y = 2 }
        /// {x = 0, y = 3 }, {x = 1, y = 3 }, {x = 2, y = 3 }, {x = 3, y = 3 }
        /// ]
        /// </code>
        public Chunk[] Chunks;

        /// <summary>
        /// Constructor, initialize a <c>Map</c> with blank <see>Chunk</see>s.
        /// </summary>
        public Map(string name, int width, int height)
        {
            Name = name;
            Chunks = [.. Enumerable.Range(0, height)
                    .Select(h => Enumerable.Range(0, width).Select(w => (w, h)))
                    .SelectMany(inner => inner)
                    .Select(t => new Coord { x = t.w, y = t.h })
                    // Weird hack below, cant figure it out :shrug:
                    .Select(coord => new Chunk(coord) { Coord = coord })];

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
