using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class RoomCategory
    {
        public int Id { get; set; }      
        public string Name { get; set; }
        public int BedsCount { get; set; }
        public decimal PricePerNight { get; set; }
        public string? Description { get; set; } 
        public ICollection<Room> Rooms { get; set; }
    }
}
