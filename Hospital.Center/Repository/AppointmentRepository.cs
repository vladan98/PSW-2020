using Hospital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository
{
    public class AppointmentRepository : RepositoryBase<Appointment, int>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public Appointment ScheduleAppointment(Appointment app)
        {
            return Create(app);
        }

        public Appointment GetById(int id)
            => FindByCondition(a => a.Id == id).FirstOrDefault();

        public List<Appointment> GetAllByDoctorAndDate(int doctorId, DateTime date)
            => FindByCondition(a => a.DoctorId.Equals(doctorId) && a.StartTime.Date.CompareTo(date.Date) == 0).ToList();

        public List<Appointment> GetAllByPatientId(int userId)
            => FindByCondition(a => a.PatientId.Equals(userId)).ToList();

        public List<Appointment> GetAllByPatientAndMonth(int userId, DateTime month)
            => FindByCondition(a => a.PatientId.Equals(userId) && a.StartTime.Month.CompareTo(month.Month) == 0).ToList();

        public Appointment GetSpecificByDoctor(int doctorId, DateTime start, DateTime end)
            => FindByCondition(a => a.DoctorId.Equals(doctorId) && a.StartTime.CompareTo(start) == 0 && a.EndTime.CompareTo(end) == 0).ToList().FirstOrDefault();

    }
}
