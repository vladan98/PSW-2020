using Hospital.Domain.Models;
using Hospital.Center.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository
{
    public interface IAppointmentRepository : IRepositoryBase<Appointment, int>
    {
        Appointment ScheduleAppointment(Appointment appointment);
        List<Appointment> GetAllByDoctorAndDate(int doctorId, DateTime date);
        List<Appointment> GetAllByPatientAndMonth(int userId, DateTime month);
        List<Appointment> GetAllByPatientId(int userId);
        Appointment GetSpecificByDoctor(int doctorId, DateTime start, DateTime end);
        Appointment GetById(int id);
    }
}
