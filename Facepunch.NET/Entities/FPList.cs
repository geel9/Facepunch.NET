using System;
using Newtonsoft.Json;

namespace Facepunch.Entities
{
    internal class FPList<T> : FPEntity, IFPPlaintextEntity where T : FPEntity
    {
        public T[] Result;

        public void HandlePlaintext(string plaintext)
        {
            try
            {
                Result = JsonConvert.DeserializeObject<T[]>(plaintext);
            }
            catch (JsonException)
            {
                throw new FPException("Error parsing list");
            }
        }

        internal override void ApplyContext(FPContext context)
        {
            this.Context = context;
            
            foreach(var item in Result)
            {
                item.ApplyContext(context);
            }
        }
    }
}
