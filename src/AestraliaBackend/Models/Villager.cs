using AestraliaBackend.Models.Structs;

namespace AestraliaBackend.Models
{
    /// <summary>
    /// Class <c>Villager</c> models a character in our simulation.
    /// </summary>
    public class Villager()
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public int id;

        /// <summary>
        /// <see>Identity</see> of a Villager, such as its name.
        /// </summary>
        public Identity identity;

        /// <summary>
        /// Gender of a citizen, true for a male, false otherwise.
        /// </summary>
        /// NOTE: winux Should this be part of `Identity` (I think it should)
        /// TODO: winux Create an `enum` instead of a bool
        public bool gender;

        /// <summary>
        /// All the <see>Job</see>s our Villager if qualified for.
        /// </summary>
        public Job[] jobs = [];

        /// <summary>
        /// Children of our villager, children will be shared between 2 Villagers.
        /// </summary>
        public Villager[] childs = [];

        /// <summary>
        /// <see>Need</see>s of our Villager, such as health, energy, ...
        /// </summary>
        public Need need;

        /// <summary>
        /// <see>Action</see>s not tied to a <see>Job</see> our Villager can perform.
        /// </summary>
        public Action[] actions = [
                new Action("eat"),
                new Action("sleep"),
                new Action("reproduce"),
        ];
    }
}
