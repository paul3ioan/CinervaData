using Cinerva.Services.Common.Properties.Dto;
using System.Collections.Generic;

namespace Cinerva.Services.Common.Properties
{
    public interface IPropertyService
    {
        void CreateProperty(PropertyDto property);
        void DeleteProperty(PropertyDto property);
        PropertyDto GetNewProperty();
        (List<PropertyDto>, int totalNumberOfProperties) GetPagedProperties(int page = 0);
        List<PropertyDto> GetProperties();
        PropertyDto GetProperty(int id);
        void UpdateProperty(PropertyDto property);
    }
}
