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

        public void SetFirstName()
        {
            string[] names;
            var transitions = new Dictionary<string, List<char>>();
            var rng = new Random();
            int minLength = 3;

            // Sample names based on gender
            names = this.gender
                ? new[] {"John", "Paul", "Mike", "Kevin", "Steve", "David", "Robert", "James", "Daniel", "Mark", "Anthony", "Brian", "Thomas", "Jason", "Matthew", "Ryan", "Adam", "Eric", "Justin", "Andrew", "Luke", "Nathan"}
                : new[] {"Mary", "Linda", "Susan", "Karen", "Lisa", "Patricia", "Jessica", "Jennifer", "Sarah", "Nancy", "Laura", "Emily", "Rebecca", "Michelle", "Melissa", "Angela", "Stephanie", "Kimberly", "Amanda", "Rachel", "Elizabeth"};

            // Build Markov chain transitions table
            foreach (var name in names)
            {
                string padded = "^" + name.ToLower() + "$";

                for (int i = 0; i < padded.Length - 2; i++)
                {
                    string key = padded.Substring(i, 2);
                    char next = padded[i + 2];

                    if (!transitions.ContainsKey(key))
                        transitions[key] = new List<char>();

                    transitions[key].Add(next);
                }
            }

            // Generate name using Markov chain
            StringBuilder result = new StringBuilder();
            var startKeys = transitions.Keys.Where(k => k.StartsWith("^")).ToList();
            string current = startKeys[rng.Next(startKeys.Count)];
            result.Append(current[1]);

            // Continue generating until we hit the end symbol '$' and meet the minimum length
            while (true)
            {
                string key = current.Substring(current.Length - 2, 2);

                if (!transitions.ContainsKey(key))
                    break;

                char next = transitions[key][rng.Next(transitions[key].Count)];

                if (next == '$')
                {
                    if (result.Length >= minLength)
                        break; // finish name generation
                    else
                        continue; // skip to next iteration to continue name generation
                }

                result.Append(next);
                current += next;
            }

            // Capitalize the first letter
            if (result.Length > 0)
                result[0] = char.ToUpper(result[0]);

            // Set the generated name as the villager's first name
            this.identity.FirstName = result.ToString();

        }
    }
}
