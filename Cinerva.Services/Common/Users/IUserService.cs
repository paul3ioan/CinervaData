using Cinerva.Services.Common.Users.Dto;
using System.Collections.Generic;

namespace Cinerva.Services.Common.Users
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        UserDto GetUser(int id);
    }
}
