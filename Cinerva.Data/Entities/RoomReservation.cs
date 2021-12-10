using System.Collections.Generic;
namespace Cinerva.Data.Entities
{
    public class RoomReservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int ReservationId { get; set; }
        public Room Room { get; set; }
        public Reservation Reservation { get; set; }
    }
}
