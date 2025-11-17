namespace AestraliaBackend.Models
{
    /// <summary>
    /// Class <c>Village</c> models village on our <see>Map</see>.
    /// </summary>
    public class Village(string title, List<Coord> chunk_coords)
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public int id;

        /// <summary>
        /// Title of the Village.
        /// </summary>
        public string title = title;

        /// <summary>
        /// Citizen of this village, see <see>Villager</see>.
        /// </summary>
        public List<Villager> villagers = [];

        /// <summary>
        /// The "position" of our village on the <see>Map</see>.
        /// </summary>
        public List<Coord> chunk_coords = chunk_coords;

    }
}
