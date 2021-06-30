using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class PatientDTO
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DoctorId { get; set; }
        public bool Blocked { get; set; }
        public PatientDTO()
        {
        }
    }
}
