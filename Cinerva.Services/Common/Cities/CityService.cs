using Cinerva.Data;
using Cinerva.Services.Common.Cities.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinerva.Services.Common.Cities
{
    public class CityService : ICityService
    {
        private readonly CinervaDBContext dbContext;
        public CityService(CinervaDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<CityDto> GetAllCities()
        {
            return dbContext.Cities.Include(x => x.Country).Select(x => new CityDto
            {
                Id = x.Id,
                Name = x.Name,
                Country = x.Country,
                CountryId = x.CountryId
            }).ToList();
        }
        public CityDto GetCity(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var cityEntity = dbContext.Cities.Find(id);

            if (cityEntity == null) return null;

            return new CityDto
            {
                Id = cityEntity.Id,
                Name = cityEntity.Name,
                Country = cityEntity.Country,
                CountryId = cityEntity.CountryId
            };
        }
    }
}
