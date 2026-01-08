namespace AestraliaBackend.Interfaces

{
    /// <summary>
    /// Serialize an object to an image.
    /// </summary>
    public interface IPpm
    {
        /// <summary>
        /// Serialize to an image with given name.
        /// </summary>
        /// The extension is automatically added to the filename.
        void ToPpm(string filename);
    }
}
