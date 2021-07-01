using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Specialization Specialization { get; set; }
        public DoctorDTO()
        {
        }

    }
}
