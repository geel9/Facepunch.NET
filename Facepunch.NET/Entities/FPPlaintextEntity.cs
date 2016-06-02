namespace Facepunch.Entities
{
    public interface IFPPlaintextEntity
    {
        /// <summary>
        /// Take action upon the specified plaintext
        /// </summary>
        /// <param name="plaintext"></param>
        void HandlePlaintext(string plaintext);
    }
}
