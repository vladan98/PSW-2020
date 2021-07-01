using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class ReferralDTO
    {
        public int Specialization { get; set; }
        public string DoctorFullName { get; set; }
        public int DoctorId { get; set; }
        public int Id { get; set; }
        public bool Used { get; set; }
        public ReferralDTO() { }
    }
}
