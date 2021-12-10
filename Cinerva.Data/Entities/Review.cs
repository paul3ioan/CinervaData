using System;

namespace Cinerva.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public int Rating { get; set; }
        public string? Description { get; set; }
        public DateTime ReviewDate { get; set; }
        public User User { get; set; }
        public Property Property { get; set; }
    }
}
