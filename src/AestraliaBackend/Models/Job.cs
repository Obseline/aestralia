namespace AestraliaBackend.Models
{
    /// <summary>
    /// Class <c>Job</c> represents the subset every Job need to implement.
    /// </summary>
    public abstract class Job(string title)
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public int id;

        /// <summary>
        /// Name of the Job.
        /// </summary>
        public string title = title;

        /// <summary>
        /// All the <see>Action</see>s the Job offers.
        /// </summary>
        public Action[] actions = [];

        /// <summary>
        /// "Level" of proficiency in this Job.
        /// </summary>
        public int experience;

    }
}
