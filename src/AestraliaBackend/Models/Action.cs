namespace AestraliaBackend.Models
{
    /// <summary>
    /// Class <c>Action</c> models an Action a <see>Villager</see> can perform.
    /// </summary>
    public class Action(string title)
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public int id;

        /// <summary>
        /// The name of the Action.
        /// </summary>
        public string title = title;

        /// <summary>
        /// The "cost" of this Action, see <see>NeedFactor</see>. 
        /// </summary>
        public NeedFactor needs_factors;

        /// <summary>
        /// Some actions consume/produce <see>Item</see>s.
        /// </summary>
        /// A negative value for the "amount" of an <see>Item</see> means it
        /// will comsume it.
        /// A positive value for the "amount" of an <see>Item</see> means it
        /// will produce it.
        public Item[] items = [];

        /// <summary>
        /// The proficiency a <see>Villager</see> will gain when perform this Action.
        /// NOTE: winux Should we rename that field to something like "xp_reward" ?
        /// </summary>
        public int gain_experience;

    }
}
