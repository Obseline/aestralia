using AestraliaBackend.Interfaces;

namespace AestraliaBackend.Models
{
    /// <summary>
    /// Class <c>Map</c> models the world.
    /// </summary>
    public class Map : IDisplayable
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
        /// {x = 0, y = 0 }, {x = 1, y = 0 }, {x = 2, y = 0 }, {x = 3, y = 0 },
        /// {x = 0, y = 1 }, {x = 1, y = 1 }, {x = 2, y = 1 }, {x = 3, y = 1 },
        /// {x = 0, y = 2 }, {x = 1, y = 2 }, {x = 2, y = 2 }, {x = 3, y = 2 },
        /// {x = 0, y = 3 }, {x = 1, y = 3 }, {x = 2, y = 3 }, {x = 3, y = 3 }
        /// ]
        /// </code>
        public Chunk[] Chunks;

        /// <summary>
        /// All the <see>Biome</see>s on the Map.
        /// </summary>
        public Biome[] Biomes = [];

        /// <summary>
        /// All the <see>Village</see>s on the Map.
        /// </summary>
        public List<Village> Villages = [];

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
                    .Select(coord => new Chunk(coord))];
        }

        /// <summary>
        /// Display the the `Map` in the Console with a preset of flags.
        /// </summary>
        /// Flags enabled:
        /// 'C' - Enable colors
        public void Display()
        {
            Display("C");
        }

        /// <summary>
        /// Display the the `Map` in the Console.
        /// </summary>
        /// Flags available:
        /// 'C' - Enable colors
        /// 'V' - Enable villages
        /// 'B' - Enable biomes
        public void Display(string format)
        {
            bool enable_colors = format.Contains('C');
            bool enable_villages = format.Contains('V');
            bool enable_biomes = format.Contains('B');
            ConsoleColor initial_foreground = Console.ForegroundColor;
            ConsoleColor initial_background = Console.BackgroundColor;

            // NOTE: winux Using 2 characters per cell make it look
            // closer to a 1:1 aspect ratio than using only one
            // character.
            foreach (Chunk chunk in Chunks)
            {
                if (chunk.Coord.x == 0 && chunk.Coord.y != 0) { Console.WriteLine(""); }

                // Layer 1: Biomes
                // TODO: winux Will do later, no `Biome`s yet

                // Layer 2: Chunks
                (string str, ConsoleColor foreground) = chunk.LandKind switch
                {
                    LandKind.Ocean => ("~~", ConsoleColor.Blue),
                    LandKind.Forest => ("==", ConsoleColor.Green),
                    LandKind.Desert => ("uu", ConsoleColor.Yellow),
                    LandKind.Mountain => ("^^", ConsoleColor.Gray),
                    LandKind.None => ("  ", initial_foreground),
                };

                // Layer 3: Villages
                if (Villages.Any(v => v.chunk_coords.Any(c => c == chunk.Coord)))
                {
                    foreground = ConsoleColor.Magenta;
                    str = "##";
                }

                if (enable_colors) { Console.ForegroundColor = foreground; }
                Console.Write(str);
                Console.ForegroundColor = initial_foreground;
                Console.BackgroundColor = initial_background;
            }

        }
    }
}
