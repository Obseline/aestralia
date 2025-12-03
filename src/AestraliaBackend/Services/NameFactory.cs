using System.Text;

namespace AestraliaBackend.Services
{
    public class NameFactory(Random rng) : INameFactory
    {
        private readonly Random _rng = rng ?? new Random();

        public NameFactory() : this(new Random()) { }

        private struct WordBeginning(char a)
        {
            public char a = a;
        };

        private struct WordTransition(char a, Nullable<char> next)
        {
            public char a = a;
            public Nullable<char> next = next;
        };

        public string Generate(string[] samples)
        {
            const int MIN_LENGTH = 3;
            var starts = new List<WordBeginning>();
            var transitions = new List<WordTransition>();

            // Build Markov chain transitions table
            foreach (string name in samples)
            {
                string name_sanitized = name.ToLower();
                starts.Add(new WordBeginning(name_sanitized[0]));
                for (int i = 0; i < name_sanitized.Length - 1; ++i)
                {
                    transitions.Add(new WordTransition(a: name_sanitized[i], next: name_sanitized[i + 1]));
                }
                transitions.Add(new WordTransition(a: name_sanitized[^1], next: null));
            }

            // Generate name from transitions table
            StringBuilder sb_result = new();

            char last_insert = starts.OrderBy(a => _rng.Next()).First().a;
            sb_result.Append(last_insert);

            while (true)
            {
                try
                {
                    WordTransition next_transition = transitions
                        .Where(x => x.a == last_insert)
                        .OrderBy(x => _rng.Next())
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
            return sb_result.ToString();

        }
    }
}
