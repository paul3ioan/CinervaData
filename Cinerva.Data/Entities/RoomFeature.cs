using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class RoomFeature
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string IconUrl { get; set; }
        public ICollection<RoomFacility> RoomFacilities { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
