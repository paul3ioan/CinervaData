﻿using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities;
    }
}
