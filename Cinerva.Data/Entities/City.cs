﻿using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
