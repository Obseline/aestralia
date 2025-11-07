namespace AestraliaBackend.Models
{
    /// <summary>
    /// Struct <c>Village</c> models 
    /// </summary>
    public struct Village(string title, Coords[] chunk_coords)
    {
        /// <summary>
        /// TODO
        /// </summary>
        public int id;

        /// <summary>
        /// TODO
        /// </summary>
        public string title = title;

        /// <summary>
        /// TODO
        /// </summary>
        public Villager[] villagers = [];

        /// <summary>
        /// TODO
        /// </summary>
        public Coords[] chunk_coords = chunk_coords;

    }
}
