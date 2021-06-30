using Hospital.Domain.Models.Users;
using Hospital.Center.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository.Abstract
{
    public interface IRegisteredUserRepository : IRepositoryBase<RegisteredUser, int>
    {
        RegisteredUser RegisterUser(RegisteredUser user);
        RegisteredUser GetByUsername(string username);
    }
}
