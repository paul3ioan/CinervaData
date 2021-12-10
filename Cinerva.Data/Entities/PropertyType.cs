using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class PropertyType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
