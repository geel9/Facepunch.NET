using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Facepunch.Entities
{
    public class FPEntity
    {
        [JsonIgnore]
        public FPContext Context { get; internal set; }

        /// <summary>
        /// Recursively apply the specified FPContext to this instance and all child instances.
        /// </summary>
        /// <param name="context">The FPContext to apply</param>
        internal virtual void ApplyContext(FPContext context)
        {
            this.Context = context;
            PropertyInfo[] subEntities = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            foreach(var property in subEntities)
            {
                if (!typeof(FPEntity).IsAssignableFrom(property.PropertyType)) continue;

                var value = (FPEntity)property.GetValue(this);
                if (value == null) continue;

                value.ApplyContext(context);
            }
        }
    }
}
