using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class ReferralCreateDTO
    {
        public int Specialization { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public ReferralCreateDTO() { }
    }
}
