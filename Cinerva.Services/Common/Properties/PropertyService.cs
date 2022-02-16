using System;
using System.Collections.Generic;
using Cinerva.Data.Entities;
using Cinerva.Data;
using System.Linq;
using Cinerva.Services.Common.Properties.Dto;
using Microsoft.EntityFrameworkCore;
using Cinerva.Services.Common.Infrastracture.Exceptions;

namespace Cinerva.Services.Common.Properties
{
    public class PropertyService : IPropertyService
    {
        public readonly CinervaDBContext dbContext;
        public PropertyService(CinervaDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public (List<PropertyDto>, int) GetPagedProperties(int page = 0)
        {
            if (page < 0) throw new ArgumentException(nameof(page));

            var properties = dbContext.Properties.Include(x => x.City).Include(x => x.PropertyType).Include(x => x.User)
               .Where(x => x.IsDeleted != true)
               .Select(x => new PropertyDto
               {
                   Id = x.Id,
                   Name = x.Name,
                   Address = x.Address,
                   UserId = x.UserId,
                   CityId = x.CityId,
                   Description = x.Description,
                   DaysForRefund = x.DaysForRefund,
                   Phone = x.Phone,
                   PropertyTypeId = x.PropertyTypeId,
                   Rating = x.Rating,
                   TotalRooms = x.TotalRooms,
                   User = x.User,
                   PropertyType = x.PropertyType,
                   City = x.City
               }).ToList();

            int maxItemsOnPage = 5;
            var numberOfProperties = properties.Count;
            var propertiesToShow = properties.Skip(page * maxItemsOnPage).Take(5).ToList();

            return (propertiesToShow, numberOfProperties);
        }
        public PropertyDto GetProperty(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var propertyEntity = dbContext.Properties.Find(id);

            if (propertyEntity == null) return null;

            return new PropertyDto
            {
                Id = propertyEntity.Id,
                Name = propertyEntity.Name,
                Address = propertyEntity.Address,
                UserId = propertyEntity.UserId,
                CityId = propertyEntity.CityId,
                Description = propertyEntity.Description,
                DaysForRefund = propertyEntity.DaysForRefund,
                Phone = propertyEntity.Phone,
                PropertyTypeId = propertyEntity.PropertyTypeId,
                Rating = propertyEntity.Rating,
                TotalRooms = propertyEntity.TotalRooms,
            };
        }
        public List<PropertyDto> GetProperties()
        {
            return dbContext.Properties.Include(x => x.City).Include(x => x.PropertyType).Include(x => x.User)
                    .Where(x => x.IsDeleted != true)
                    .Select(x => new PropertyDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Address = x.Address,
                        UserId = x.UserId,
                        CityId = x.CityId,
                        Description = x.Description,
                        DaysForRefund = x.DaysForRefund,
                        Phone = x.Phone,
                        PropertyTypeId = x.PropertyTypeId,
                        Rating = x.Rating,
                        TotalRooms = x.TotalRooms,
                        User = x.User,
                        PropertyType = x.PropertyType,
                        City = x.City
                    }).ToList();
        }
        public PropertyDto GetNewProperty()
        {
            //if there is not property in database
            if (dbContext.Properties.Count() == 0)
                return null;

            var lastId = dbContext.Properties.Max(x => x.Id);

            var newProperty = dbContext.Properties.Find(lastId);
            return new PropertyDto
            {
                Id = newProperty.Id,
                Name = newProperty.Name,
                Address = newProperty.Address,
                UserId = newProperty.UserId,
                CityId = newProperty.CityId,
                Description = newProperty.Description,
                DaysForRefund = newProperty.DaysForRefund,
                Phone = newProperty.Phone,
                PropertyTypeId = newProperty.PropertyTypeId,
                Rating = newProperty.Rating,
                TotalRooms = newProperty.TotalRooms,
            };
        }
        public void CreateProperty(PropertyDto property)
        {
            if (property == null) throw new ArgumentException(nameof(property)); 
            
            if (property.CityId < 1 || property.UserId < 1 || property.PropertyTypeId < 1)
                return;

            var propertyEntity = new Property
            {
                Name = property.Name,
                CityId = property.CityId,
                Address = property.Address,
                UserId = property.UserId,
                Description = property.Description,
                DaysForRefund = property.DaysForRefund,
                Phone = property.Phone,
                PropertyTypeId = property.PropertyTypeId,
                Rating = property.Rating,
                TotalRooms = property.TotalRooms,
            };

            dbContext.Properties.Add(propertyEntity);
            dbContext.SaveChanges();
        }
        public void DeleteProperty(PropertyDto property)
        {
            if (property == null) return;

            var propertyEntity = dbContext.Properties.FirstOrDefault(x => x.Id == property.Id);

            if (propertyEntity == null) return;

            propertyEntity.IsDeleted = true;
            dbContext.SaveChanges();
        }
        public void UpdateProperty(PropertyDto property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            var propertyEntity = dbContext.Properties.Find(property.Id);
            if (propertyEntity == null) throw new EntityNotFoundException();
            propertyEntity.Name = property.Name;
            propertyEntity.CityId = property.CityId;
            propertyEntity.Address = property.Address;
            propertyEntity.UserId = property.UserId;
            propertyEntity.Description = property.Description;
            propertyEntity.DaysForRefund = property.DaysForRefund;
            propertyEntity.Phone = property.Phone;
            propertyEntity.PropertyTypeId = property.PropertyTypeId;
            propertyEntity.Rating = property.Rating;
            propertyEntity.TotalRooms = property.TotalRooms;

            dbContext.SaveChanges();
        }
    }
}

