using Cinerva.Services.Common.Cities.Dto;
using System.Collections.Generic;

namespace Cinerva.Services.Common.Cities
{
    public interface ICityService
    {
        IEnumerable<CityDto> GetAllCities();
        CityDto GetCity(int id);
    }
}
