namespace AestraliaBackend.Models
{
    /// <summary>
    /// Struct <c>Coord</c> models coordinates
    /// </summary>
    public struct Coord
    {
        /// <summary>
        /// Position on the X axis (the horizontal one).
        /// </summary>
        public int x;

        /// <summary>
        /// Position on the Y axis (the vertical one).
        /// </summary>
        /// // NOTE: winux138 `warning` is not a valid tag ?
        /// <remarks>
        /// The Y axis grows DOWNWARD!
        /// </remarks>
        public int y;
    }
}
