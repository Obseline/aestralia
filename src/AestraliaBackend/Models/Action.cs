namespace AestraliaBackend.Models
{
    /// <summary>
    /// Struct <c>Action</c> models TODO
    /// </summary>
    public struct Action(string title)
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
        public int needs_factors;

        /// <summary>
        /// TODO
        /// </summary>
        public Item[] items = [];

        /// <summary>
        /// TODO
        /// </summary>
        /// NOTE: winux Should we rename that field to something like "xp_reward" ?
        public int gain_experience;

    }
}
