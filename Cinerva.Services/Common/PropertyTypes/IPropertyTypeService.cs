using Cinerva.Services.Common.PropertyTypes.Dto;
using System.Collections.Generic;

namespace Cinerva.Services.Common.PropertyTypes
{
    public interface IPropertyTypeService
    {
        List<PropertyTypeDto> GetAllPropertiesTypes();
        PropertyTypeDto GetPropertyType(int id);
    }
}
