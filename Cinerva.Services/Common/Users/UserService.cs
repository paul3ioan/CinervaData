using Cinerva.Data;
using Cinerva.Services.Common.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinerva.Services.Common.Users
{
    public class UserService : IUserService
    {
        private readonly CinervaDBContext dbContext;
        public UserService(CinervaDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<UserDto> GetAllUsers()
        {
            if (dbContext.Users.Count() == 0) return null;

            return dbContext.Users.Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
            }).ToList();
        }
        public UserDto GetUser(int id)
        {
            if (id < 1) throw new ArgumentException(nameof(id));

            var userEntity = dbContext.Users.Find(id);

            if (userEntity == null) return null;

            return new UserDto
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,

            };
        }
    }
}
