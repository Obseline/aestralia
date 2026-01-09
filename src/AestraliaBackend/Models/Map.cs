using System.Text;

using AestraliaBackend.Models.Structs;
using AestraliaBackend.Interfaces;

namespace AestraliaBackend.Models
{
    /// <summary>
    /// Class <c>Map</c> models the world.
    /// </summary>
    public class Map : IDisplayable, IPpm
    {
        /// <summary>
        /// Name of the map/world.
        /// </summary>
        public string Name;

        public int Width { get; }
        public int Height { get; }

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
        public List<Biome> Biomes = [];

        /// <summary>
        /// All the <see>Village</see>s on the Map.
        /// </summary>
        public List<Village> Villages = [];

        /// <summary>
        /// Constructor, initialize a <c>Map</c> with blank <see>Chunk</see>s.
        /// </summary>
        public Map(string name, int width, int height)
        {
            Width = width;
            Height = height;
            Name = name;
            Chunks = [.. Enumerable.Range(0, height)
                    .Select(h => Enumerable.Range(0, width).Select(w => (w, h)))
                    .SelectMany(inner => inner)
                    .Select(t => new Coord { x = t.w, y = t.h })
                    .Select(coord => new Chunk(coord))];

            // Should it be done here ?
            const int BIOMES_COUNT = 20;
            var rnd = new Random();
            var biome_centers = Enumerable.Range(0, BIOMES_COUNT)
                .Select(i => rnd.Next() % (Width * Height))
                .Select(i => (Chunks[i].Coord, (LandKind)(i % (Enum.GetValues(typeof(LandKind)).Length - 1) + 1)))
            .ToList();
            foreach (var chunk in Chunks)
            {
                var closest = biome_centers.MinBy(biome =>
                        Math.Pow(biome.Item1.x - chunk.Coord.x, 2)
                        + Math.Pow(biome.Item1.y - chunk.Coord.y, 2)
                    );
                chunk.LandKind = closest.Item2;
            }
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

        public void SpawnVillages(int num_villages)
        {
            Console.WriteLine($"TODO: Spawn {num_villages} villages");
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

        private const UInt16 RED = 0x0F00;
        private const UInt16 GREEN = 0x00F0;
        private const UInt16 BLUE = 0x000F;
        private const UInt16 YELLOW = 0x0FF0;
        private const UInt16 WHITE = 0x0FFF;
        private const UInt16 GRAY = 0x0777;
        private const UInt16 BLACK = 0x0000;

        private static UInt16[,] vec2(UInt16 color)
        {
            var p = new UInt16[5, 5];
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    p[i, j] = color;
                }
            }
            return p;
        }

        /// <summary>
        /// Serialize the `Map` to a `.ppm` image.
        /// </summary>
        public void ToPpm(string filename)
        {
            const int RESOLUTION = 5;
            UInt16[][,] pixels = [.. Chunks.Select(
                    chunk => chunk.LandKind switch
                        {
                            LandKind.Ocean => vec2(BLUE),
                            LandKind.Forest => vec2(GREEN),
                            LandKind.Desert => vec2(YELLOW),
                            LandKind.Mountain => vec2(GRAY),
                            LandKind.None => vec2(RED),
                        }
                    )];

            StringBuilder sb_pixels = new();
            sb_pixels.AppendLine("P3");
            sb_pixels.AppendLine($"{Width * RESOLUTION} {Height * RESOLUTION}");
            sb_pixels.AppendLine("15");
            for (int outer_h = 0; outer_h < Chunks.Length / Width; ++outer_h)
            {
                for (int inner_h = 0; inner_h < RESOLUTION; ++inner_h)
                {
                    // Console.WriteLine($"{outer_h}:{outer_w} ({Chunks[outer_h * Width + outer_w].Coord.x})");
                    for (int outer_w = 0; outer_w < Width; ++outer_w)
                    {
                        for (int inner_w = 0; inner_w < RESOLUTION; ++inner_w)
                        {
                            sb_pixels.Append((pixels[outer_h * Width + outer_w][inner_w, inner_h] & 0x0F00) >> 8);
                            sb_pixels.Append(" ");
                            sb_pixels.Append((pixels[outer_h * Width + outer_w][inner_w, inner_h] & 0x00F0) >> 4);
                            sb_pixels.Append(" ");
                            sb_pixels.Append((pixels[outer_h * Width + outer_w][inner_w, inner_h] & 0x000F));
                            sb_pixels.Append("  ");
                        }
                        sb_pixels.AppendLine("");
                    }
                    sb_pixels.AppendLine("");
                }
            }
            File.WriteAllText(filename + ".ppm", sb_pixels.ToString());
        }
    }
}
