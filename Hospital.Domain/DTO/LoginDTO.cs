using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class LoginDTO
    {
        public LoginDTO()
        {
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
