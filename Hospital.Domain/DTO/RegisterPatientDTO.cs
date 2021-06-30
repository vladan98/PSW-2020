using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class RegisterPatientDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public int ChosenDoctorId { get; set; }
        public RegisterPatientDTO() { }
    }
}
