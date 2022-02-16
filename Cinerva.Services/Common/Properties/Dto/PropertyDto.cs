using Cinerva.Data.Entities;

namespace Cinerva.Services.Common.Properties.Dto
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Rating { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int UserId { get; set; }
        public int DaysForRefund { get; set; }
        public int PropertyTypeId { get; set; }
        public bool? IsDeleted { get; set; }
        public string Phone { get; set; }
        public int TotalRooms { get; set; }
        public string ZipCode { get; set; }
        public City City { get; set; }
        public User User { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}
