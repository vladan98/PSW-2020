using Hospital.Domain.DTO;
using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Services
{
    public interface IAppointmentService
    {
        List<Appointment> GetAll();
        bool ScheduleAppointment(Appointment appointment, int referralId);
        List<Appointment> GetAvailableByDoctorAndDate(int docId, DateTime date, TypeOfAppointment type);
        List<Appointment> GetAvailableByDoctorAndInterval(int docId, DateTime from, DateTime to, TypeOfAppointment type);
        List<Appointment> GetAvailableByDoctorSpeciality(Specialization specialization, DateTime start, DateTime end, TypeOfAppointment type);
        List<Appointment> GetPosibleForWorkDay(int doctorId, DateTime date, TypeOfAppointment type);
        List<Appointment> FilterAllowedAppointments(int patientId, List<Appointment> appointments);
        List<Appointment> SearchAppointments(SearchParameters searchParameters);
        List<Appointment> UserAppointments(int userId);
        bool CancelAppointment(int id);
    }
}
