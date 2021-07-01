using Hospital.Domain.Enums;
using System;

namespace Hospital.Domain.DTO
{
    public class SearchAppointmentsDTO
    {
        public int UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int TypeOfAppointment { get; set; }
        public int Priority { get; set; }
        public int DoctorId { get; set; }

        public SearchAppointmentsDTO() { }
    }
    public class SearchParameters
    {
        public int UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public TypeOfAppointment TypeOfAppointment { get; set; }
        public SearchPriority Priority { get; set; }
        public int DoctorId { get; set; }

        public SearchParameters(int userId, DateTime from, DateTime to, TypeOfAppointment typeOfAppointment, SearchPriority priority, int doctorId)
        {
            UserId = userId;
            From = from;
            To = to;
            TypeOfAppointment = typeOfAppointment;
            Priority = priority;
            DoctorId = doctorId;
        }

        public SearchParameters()
        {
        }
    }
}
