using System;
using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal Price { get; set; }
        public bool? IsCanceled { get; set; }
        public bool? IsCancelRequest { get; set; }
        public bool Paid { get; set; }
        public User User { get; set; }
        public ICollection<RoomReservation> RoomReservations { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
