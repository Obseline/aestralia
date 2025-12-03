using AestraliaBackend.Models.Structs;
using AestraliaBackend.Services;

namespace AestraliaBackend.Models
{
    /// <summary>
    /// Class <c>Villager</c> models a character in our simulation.
    /// </summary>
    public class Villager
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public int Id;

        /// <summary>
        /// <see>Identity</see> of a Villager, such as its name.
        /// </summary>
        public Identity Identity;

        /// <summary>
        /// Gender of a citizen, true for a male, false otherwise.
        /// </summary>
        /// NOTE: winux Should this be part of `Identity` (I think it should)
        /// TODO: winux Create an `enum` instead of a bool
        public bool Gender;

        /// <summary>
        /// All the <see>Job</see>s our Villager if qualified for.
        /// </summary>
        public Job[] Jobs = [];

        /// <summary>
        /// Children of our villager, children will be shared between 2 Villagers.
        /// </summary>
        public Villager[] Childs = [];

        /// <summary>
        /// <see>Need</see>s of our Villager, such as health, energy, ...
        /// </summary>
        public Need Need;

        /// <summary>
        /// <see>Action</see>s not tied to a <see>Job</see> our Villager can perform.
        /// </summary>
        public Action[] Actions = [
                new Action("eat"),
                new Action("sleep"),
                new Action("reproduce"),
        ];

        /// <summary>
        /// <see>Item</see>s held by this <c>Villager</c>.
        /// </summary>
        public List<Item> Items = [];

        // NOTE: winux Probably move to a builder pattern ?
        public Villager() : this(new Random().NextDouble() >= 0.5) { }

        public Villager(bool gender)
        {
            // FIXME: winux Set an atomic Id
            Gender = gender;
            Identity.FirstName = new NameFactory().Generate(
         Gender
            ? ["John", "Paul", "Mike", "Kevin", "Steve", "David", "Robert", "James", "Daniel", "Mark", "Anthony", "Brian", "Thomas", "Jason", "Matthew", "Ryan", "Adam", "Eric", "Justin", "Andrew", "Luke", "Nathan"]
            : ["Mary", "Linda", "Susan", "Karen", "Lisa", "Patricia", "Jessica", "Jennifer", "Sarah", "Nancy", "Laura", "Emily", "Rebecca", "Michelle", "Melissa", "Angela", "Stephanie", "Kimberly", "Amanda", "Rachel", "Elizabeth"]
                    );
        }
    }
}
