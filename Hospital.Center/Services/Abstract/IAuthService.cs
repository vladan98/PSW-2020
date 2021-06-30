using Hospital.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Services.Abstract
{
    public interface IAuthService
    {
        AuthenticatedUserDTO Login(LoginDTO user);
    }
}
