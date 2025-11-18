namespace AestraliaBackend.Models
{
    /// <summary>
    /// All the possible variants for a <c>Biome</c>.
    /// </summary>
    public enum LandKind
    {
        None = 0,
        Ocean,
        Forest,
        Desert,
        Mountain,
    }

    /// <summary>
    /// Class <c>Chunk</c> models a "cell", the smallest unit/element of a <see>Map</see>.
    /// </summary>
    public class Chunk(Coord coord)
    {
        /// <summary>
        /// Location of the <c>Chunk</c> on the <see>Map</see>.
        /// </summary>
        public Coord Coord = coord;

        public LandKind LandKind = LandKind.None;

        /// <summary>
        /// <see>Item</see>s available on this <c>Chunk</c>.
        /// </summary>
        public List<Item> Items = [];
    }
}
