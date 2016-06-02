namespace Facepunch.Entities
{
    public interface IFPPlaintextEntity
    {
        /// <summary>
        /// Take action upon the specified plaintext
        /// </summary>
        void HandlePlaintext(string plaintext);
    }
}
