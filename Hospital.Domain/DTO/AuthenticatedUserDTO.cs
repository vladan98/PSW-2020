using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class AuthenticatedUserDTO
    {
        public int Id { get; set; }
        public int ChoosenDoctorId { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
