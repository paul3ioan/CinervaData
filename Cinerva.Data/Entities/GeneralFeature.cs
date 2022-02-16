using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class GeneralFeature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public ICollection<Property> Properties { get; set; }
        public ICollection<PropertyFacility> PropertyFacilities { get; set; }
    }
}
