using Cinerva.Data;
using Cinerva.Services.Common.PropertyTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinerva.Services.Common.PropertyTypes
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly CinervaDBContext dbContext;
        public PropertyTypeService(CinervaDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<PropertyTypeDto> GetAllPropertiesTypes()
        {
            if (dbContext.PropertyTypes.Count() == 0) return null;

            return dbContext.PropertyTypes.Select(x => new PropertyTypeDto
            {
                Id = x.Id,
                Type = x.Type,
            }).ToList();
        }
        public PropertyTypeDto GetPropertyType(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var propertyTypeEntity = dbContext.PropertyTypes.Find(id);

            if (propertyTypeEntity == null) return null;

            return new PropertyTypeDto
            {
                Id = propertyTypeEntity.Id,
                Type = propertyTypeEntity.Type,
            };
        }
    }
}
