namespace AestraliaBackend.Models
{
    /// <summary>
    /// Struct <c>Coord</c> models coordinates
    /// </summary>
    public struct Coord : IEquatable<Coord>
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

        // IEquatable
        // https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1?view=net-9.0
        public bool Equals(Coord other) => x == other.x && y == other.y;
        public override bool Equals(object? obj) => obj is Coord c && Equals(c);
        public override int GetHashCode() => HashCode.Combine(x, y);
        public static bool operator ==(Coord left, Coord right) => left.Equals(right);
        public static bool operator !=(Coord left, Coord right) => !left.Equals(right);
    }
}
