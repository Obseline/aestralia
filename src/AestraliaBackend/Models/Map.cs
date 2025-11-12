using System.Globalization;
using System.Text;

namespace AestraliaBackend.Models
{
    /// <summary>
    /// Class <c>Map</c> models the world.
    /// </summary>
    public class Map : IFormattable
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

        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string formatter)
        {
            return ToString(formatter, CultureInfo.CurrentCulture);
        }

        public string ToString(string? formatter, IFormatProvider? provider)
        {
            if (string.IsNullOrEmpty(formatter))
            {
                formatter = "G";
            }

            var sb = new StringBuilder();
            var enable_color = formatter.Contains('C');

            foreach (var chunk in Chunks)
            {
                if (chunk.Coord.x == 0) { sb.AppendLine(); }

                var initial_color = Console.ForegroundColor;
                // NOTE: winux Using 2 characters per cell make it look
                // closer to a 1:1 aspect ratio than using only one
                // character.
                var c = chunk.LandKind switch
                {
                    LandKind.Ocean => (str: "~~", color: ConsoleColor.Blue),
                    LandKind.Forest => (str: "==", color: ConsoleColor.Green),
                    LandKind.Desert => (str: "uu", color: ConsoleColor.Yellow),
                    LandKind.Mountain => (str: "^^", color: ConsoleColor.Gray),
                    LandKind.None => (str: "  ", color: initial_color),
                };

                // NOTE: winux Hitting a wall there:
                // The coloring is tied to the `Console`
                // Hence we cannot modify it here, we are supposed to only
                // return a string
                // The solution is probably to have our own `IFormattable`
                // interface.

                // if (enable_color) { Console.ForegroundColor = c.color; }
                // Console.Write(c.str);
                // Console.ForegroundColor = c.color;

                sb.Append(c.str);

            }

            return sb.ToString();
        }
    }
}
