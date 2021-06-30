using Hospital.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Services.Abstract
{
    public interface IRegisteredUserService
    {
        List<RegisteredUser> GetAll();
        RegisteredUser RegisterUser(RegisteredUser user);
        RegisteredUser GetByUsername(string username);
    }
}
