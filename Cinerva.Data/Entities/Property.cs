using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cinerva.Data.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        [Column ("AdministratorId")]
        public int UserId { get; set; }
        [Column ("NumberOfDayForRefounds")]
        public int DaysForRefund { get; set; }
        public int PropertyTypeId { get; set; }
        public bool? IsDeleted { get; set; }
        public string Phone { get; set; }
        public int TotalRooms { get; set; }
        public string ZipCode { get; set; }
        public City City { get; set; }
        public User User { get; set; }
        public PropertyType PropertyType { get; set; }
        public ICollection<GeneralFeature> GeneralFeatures { get; set; }
        public ICollection<PropertyFacility> PropertyFaclities { get; set; }
        public ICollection<PropertyImage> PropertyImages { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
