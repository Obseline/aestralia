namespace AestraliaBackend.Models
{
    /// <summary>
    /// Class <c>Chunk</c> models a "cell", the smallest unit/element of a <see>Map</see>.
    /// </summary>
    public class Chunk(Coord coord)
    {
        /// <summary>
        /// Location of the <c>Chunk</c> on the <see>Map</see>.
        /// </summary>
        public required Coord Coord = coord;

        /// <summary>
        /// <see>Ressource</see>s available on this <c>Chunk</c>.
        /// </summary>
        public Ressource[] Ressources = [];
    }
}
