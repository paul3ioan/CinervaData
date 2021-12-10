using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomCategoryId { get; set; }
        public int PropertyId { get; set; }
        public bool? IsDeleted { get; set; }
        public Property Property { get; set; }
        public ICollection<RoomFacility> RoomFacilities { get; set; }
        public ICollection<RoomFeature> RoomFeatures { get; set; }
        public RoomCategory RoomCategory { get; set; }
        public ICollection<RoomReservation> RoomReservations { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
