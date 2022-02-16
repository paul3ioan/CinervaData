using System.Collections.Generic;
namespace Cinerva.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsBanned { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Property> Properties { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
