using Cinerva.Services.Common.Cities.Dto;
using Cinerva.Services.Common.PropertyTypes.Dto;
using Cinerva.Services.Common.Users.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Cinerva.Web.Models.Properties
{
    public class EditAndCreatePropertyViewModel : BasePropertyViewModel
    { 
        public IEnumerable<IFormFile> Images { get; set; }
        public IEnumerable<PropertyTypeDto> PropertyTypes { get; set; }
        public IEnumerable<CityDto> Cities { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }
}
