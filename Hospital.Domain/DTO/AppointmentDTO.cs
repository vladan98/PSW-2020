using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class AppointmentDTO
    {
        public DateTime StartTime { get; set; }
        public int TypeOfAppointment { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int ReferralId { get; set; }
        public DoctorDTO Doctor { get; set; }
        public PatientDTO Patient { get; set; }
        public DateTime EndTime { get; set; }

        public AppointmentDTO() { }

    }
}
