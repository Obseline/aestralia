namespace AestraliaBackend.Models
{
    /// <summary>
    /// All the possible variants for an <c>Item</c>.
    /// </summary>
    public enum ItemKind
    {
        None = 0,
        WoodLog,
        WoodPlank,
        WoodStick,
        WoodSappling,
        Apple,
        Stone,
        Axe,
    }

    /// <summary>
    /// Class <c>Item</c> models an item.
    /// </summary>
    /// NOTE: winux This one is tough and will need extra and careful thinking.
    /// NOTE: winux Should this be an abstract class ?
    public class Item(ItemKind item_kind, int quantity)
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public int id;

        /// <summary>
        /// Which <c>Item</c> it is ?
        /// </summary>
        public ItemKind ItenKind = item_kind;

        /// <summary>
        /// Quantity/amount of the <c>Item</c> (must be non-zero ?).
        /// </summary>
        public int Quantity = quantity;
    }
}
