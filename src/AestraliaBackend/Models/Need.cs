namespace AestraliaBackend.Models
{
    /// <summary>
    /// Struct <c>Need</c> models TODO
    /// </summary>
    public struct Need
    {
        /// <summary>
        /// TODO
        /// </summary>
        public int health;

        /// <summary>
        /// TODO: Check if this word even exist ?
        /// </summary>
        public int saciety;

        /// <summary>
        /// TODO
        /// </summary>
        public int energy;
    }

    /// <summary>
    /// Struct <c>NeedFactor</c> models TODO
    /// </summary>
    /// NOTE: winux `factor` is probably a bad wording, it is misleading, it
    /// implies a multiplication rather than a substraction/addition
    public struct NeedFactor
    {
        /// <summary>
        /// TODO
        /// </summary>
        public int health_factor;

        /// <summary>
        /// TODO: Check if this word even exist ?
        /// </summary>
        public int saciety_factor;

        /// <summary>
        /// TODO
        /// </summary>
        public int energy_factor;
    }
}
