using AestraliaBackend.Models.Structs;

namespace AestraliaBackend.Models
{
    using System;
    using System.Text;

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


        /// <summary>
        /// <see>Item</see>s held by this <c>Villager</c>.
        /// </summary>
        public List<Item> Items = [];

        private struct WordBeginning(char a)
        {
            public char a = a;
        };

        private struct WordTransition(char a, Nullable<char> next)
        {
            public char a = a;
            public Nullable<char> next = next;
        };

        public void SetFirstName()
        {
            const int MIN_LENGTH = 3;
            var rng = new Random();
            var starts = new List<WordBeginning>();
            var transitions = new List<WordTransition>();

            // Sample names based on gender
            string[] names = this.gender
                ? new[] { "John", "Paul", "Mike", "Kevin", "Steve", "David", "Robert", "James", "Daniel", "Mark", "Anthony", "Brian", "Thomas", "Jason", "Matthew", "Ryan", "Adam", "Eric", "Justin", "Andrew", "Luke", "Nathan" }
                : new[] { "Mary", "Linda", "Susan", "Karen", "Lisa", "Patricia", "Jessica", "Jennifer", "Sarah", "Nancy", "Laura", "Emily", "Rebecca", "Michelle", "Melissa", "Angela", "Stephanie", "Kimberly", "Amanda", "Rachel", "Elizabeth" };

            // Build Markov chain transitions table
            foreach (var name in names)
            {
                var name_sanitized = name.ToLower();
                starts.Add(new WordBeginning(name_sanitized[0]));
                for (int i = 0; i < name_sanitized.Length - 1; ++i)
                {
                    transitions.Add(new WordTransition(a: name_sanitized[i], next: name_sanitized[i + 1]));
                }
                transitions.Add(new WordTransition(a: name_sanitized[^1], next: null));
            }

            // Generate name from transitions table
            StringBuilder sb_result = new StringBuilder();

            var last_insert = starts.OrderBy(a => rng.Next()).First().a;
            sb_result.Append(last_insert);

            while (true)
            {
                try
                {
                    var next_transition = transitions
                        .Where(x => x.a == last_insert)
                        .OrderBy(x => rng.Next())
                        .First();

                    if (next_transition.next is char next)
                    {
                        last_insert = next;
                        sb_result.Append(last_insert);
                        continue;
                    }

                    // NOTE: Infinite loop if result.len < 3 but no transitions available
                    if (transitions.Where(x => x.a == last_insert).All(x => x.next is null))
                        break;

                    if (sb_result.Length >= MIN_LENGTH)
                        break;
                }
                catch { break; }
            }

            sb_result[0] = char.ToUpper(sb_result[0]);
            this.identity.FirstName = sb_result.ToString();
        }
    }
}
