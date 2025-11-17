namespace AestraliaBackend.Models
{
    /// <summary>
    /// Struct <c>Need</c> models the needs of a <see>Villager</see>.
    /// </summary>
    public struct Need
    {
        /// NOTE: winux Currently every Villager will have needs that ranges
        /// from 0 to 100. It might not be the behaviour we expect.
        /// For instance we could want the "cap" value to be random within a
        /// range, to represent differences in "genetic", or a "cap" could be
        /// raised through "pratice".

        /// <summary>
        /// The max value for <see>health</see>.
        /// </summary>
        public static readonly int HEALTH_MAX = 100;

        /// <summary>
        /// The max value for <see>saciety</see>.
        /// </summary>
        public static readonly int SACIETY_MAX = 100;

        /// <summary>
        /// The max value for <see>energy</see>.
        /// </summary>
        public static readonly int ENERGY_MAX = 100;

        /// <summary>
        /// The health of an entity, reaching 0 (or a negative value) means death.
        /// </summary>
        public int health;

        /// <summary>
        /// The lower the value, the higher the hunger.
        /// TODO: Check if this word even exist ?
        /// </summary>
        public int saciety;

        /// <summary>
        /// The energy available to perform <see>Action</see>s.
        /// </summary>
        public int energy;
    }

    /// <summary>
    /// Struct <c>NeedFactor</c> models the "cost" of an <see>Action</see>, how
    /// it will modify the <see>Need</see> of a <see>Villager</see>.
    /// </summary>
    /// NOTE: winux `factor` is probably a bad wording, it is misleading, it
    /// implies a multiplication rather than a substraction/addition
    public struct NeedFactor
    {
        /// <summary>
        /// The "cost" in <see>health</see>.
        /// </summary>
        public int health_factor;

        /// <summary>
        /// The "cost" in <see>saciety</see>.
        /// </summary>
        public int saciety_factor;

        /// <summary>
        /// TODO
        /// The "cost" in <see>energy</see>.
        /// </summary>
        public int energy_factor;
    }
}
