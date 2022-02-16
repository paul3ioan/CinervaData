using System.Collections.Generic;

namespace Cinerva.Web.Models.Properties
{
    public class DetailsAndDeleteViewModel:BasePropertyViewModel
    {
        public string Admin { get; set; }
        public string City { get; set; }
        public string PropertyType { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
