namespace AestraliaBackend.Models
{
    /// <summary>
    /// Struct <c>Villager</c> models Villagerinates
    /// </summary>
    public struct Villager()
    {
        /// <summary>
        /// TODO
        /// </summary>
        public int id;

        /// <summary>
        /// TODO
        /// </summary>
        public Identity identity;

        /// <summary>
        /// TODO
        /// </summary>
        /// NOTE: winux Should this be part of `Identity` (I think it should)
        /// TODO: winux Create an `enum` instead of a bool
        public bool gender;

        /// <summary>
        /// TODO
        /// </summary>
        public Job[] jobs = [];

        /// <summary>
        /// TODO
        /// </summary>
        public Villager[] childs = [];

        /// <summary>
        /// TODO
        /// </summary>
        public Need need;

        /// <summary>
        /// TODO
        /// </summary>
        public Action[] actions = [
                new Action("eat"),
                new Action("sleep"),
                new Action("reproduce"),
        ];
    }
}
