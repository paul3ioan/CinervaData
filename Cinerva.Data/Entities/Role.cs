using System.Collections.Generic;

namespace Cinerva.Data.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users;
    }
}
