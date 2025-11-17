namespace AestraliaBackend.Interfaces

{
    /// <summary>
    /// Represents an object that can produce a textual representation for display.
    /// </summary>
    public interface IDisplayable
    {
        /// <summary>
        /// Default display function, with sane defaults.
        /// </summary>
        void Display();

        /// <summary>
        /// Display must be tweak through `format`.
        /// </summary>
        void Display(string format);
    }
}
