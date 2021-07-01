using Hospital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.DTO
{
    public class UserAppointmentsDTO
    {
        public List<AppointmentDTO> previousAppointments { get; set; }
        public List<AppointmentDTO> futureAppointments { get; set; }
        public UserAppointmentsDTO()
        {
        }


    }
}
