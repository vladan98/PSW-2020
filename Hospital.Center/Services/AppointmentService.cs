using Hospital.Domain.DTO;
using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Users;
using Hospital.Center.Repository;
using Hospital.Center.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Center.Services
{
    public class AppointmentService : IAppointmentService
    {
        private const int appointmentDuration = 15;

        private readonly IAppointmentRepository appointmentRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IReferralRepository referralRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IWorkDayRepository workDayRepository;

        public AppointmentService(IAppointmentRepository appRepo, IDoctorRepository docRepo, IPatientRepository patRepo, IReferralRepository refRepo, IWorkDayRepository wdRepo)
        {
            appointmentRepository = appRepo;
            referralRepository = refRepo;
            doctorRepository = docRepo;
            patientRepository = patRepo;
            workDayRepository = wdRepo;
        }

        public List<Appointment> GetAll()
        {
            var appointments = appointmentRepository.FindAll();
            if (appointments.Count > 0)
                foreach (Appointment appointment in appointments)
                    BindAppointmentProperties(appointment);
            return appointments;
        }

        public List<Appointment> UserAppointments(int userId)
        {
            var userAppointments = appointmentRepository.GetAllByPatientId(userId);
            if (userAppointments.Count > 0)
                foreach (Appointment appointment in userAppointments)
                    BindAppointmentProperties(appointment);
            return userAppointments;
        }

        public bool ScheduleAppointment(Appointment appointment, int referralId)
        {
            // check if desired appointment already exists
            var desired = appointmentRepository.GetSpecificByDoctor(appointment.DoctorId, appointment.StartTime, appointment.EndTime);
            // check if referral is passed
            if (referralId != -1)
            {
                // check if referral is updated
                var referral = referralRepository.GetById(referralId);
                if (referral == null)
                    return false;
                referral.Used = true;
                var updatedReferral = referralRepository.Update(referral);


                if (updatedReferral == null)
                    return false;
            }
            if (desired != null)
            {
                // if appointment exists but is canceled, just update it
                if (desired.Canceled)
                {
                    desired.Canceled = false;
                    desired.PatientId = appointment.PatientId;
                    desired.Description = desired.Description;
                    var ret = appointmentRepository.Update(desired);
                    if (ret == null) return false;
                    return true;
                }
                return false;
            }

            var done = appointmentRepository.ScheduleAppointment(appointment);
            if (done != null)
                return true;

            return true;
        }

        public bool CancelAppointment(int appointmentId)
        {
            Appointment appointment = appointmentRepository.GetById(appointmentId);

            // check if exists or is already canceled
            if (appointment == null || appointment.Canceled)
                return false;

            // check if appintment can be canceled
            DateTime appointmentDate = appointment.StartTime;
            if (appointmentDate <= DateTime.Now.AddDays(2))
                return false;

            var patient = patientRepository.GetById(appointment.PatientId);

            // Cancel appoitnment
            appointment.Canceled = true;
            appointmentRepository.Update(appointment);

            // Mark patient as malicious
            var patientAppointmentsForMonth = appointmentRepository.GetAllByPatientAndMonth(patient.Id, appointment.StartTime);
            var patientCanceledApps = patientAppointmentsForMonth.Where(app => app.Canceled).ToList();
            if (patientCanceledApps.Count >= 3)
                patient.ShouldBeBlocked = true;


            var done = patientRepository.Update(patient);
            if (done == null) return false;
            return true;
        }

        public List<Appointment> SearchAppointments(SearchParameters searchParameters)
        {
            var patient = patientRepository.GetById(searchParameters.UserId);
            if (patient == null) return new List<Appointment>() { };

            if (searchParameters.DoctorId == -1)
                searchParameters.DoctorId = patient.ChosenDoctorId;

            var found = GetAvailableByDoctorAndInterval(searchParameters.DoctorId, searchParameters.From, searchParameters.To, searchParameters.TypeOfAppointment);

            if (found.Count > 0)
                return FilterAllowedAppointments(searchParameters.UserId, found);

            if (searchParameters.Priority == SearchPriority.DOCTOR)
            {
                var widerStart = searchParameters.From.AddDays(-7);
                var widerEnd = searchParameters.To.AddDays(7);

                var foundWider = GetAvailableByDoctorAndInterval(searchParameters.DoctorId, widerStart, widerEnd, searchParameters.TypeOfAppointment);

                return FilterAllowedAppointments(searchParameters.UserId, foundWider);
            }
            else if (searchParameters.Priority == SearchPriority.DATE)
            {
                var desiredSpecialty = doctorRepository.GetById(searchParameters.DoctorId).Specialization;
                var foundInRange = GetAvailableByDoctorSpeciality(desiredSpecialty, searchParameters.From, searchParameters.To, searchParameters.TypeOfAppointment);

                return FilterAllowedAppointments(searchParameters.UserId, foundInRange);
            }

            return new List<Appointment>() { };
        }

        public List<Appointment> FilterAllowedAppointments(int patientId, List<Appointment> appointments)
        {
            List<Referral> patientReferrals = referralRepository.GetUnusedByPatientId(patientId);

            List<Specialization> allowedSpecializations = new List<Specialization> { Specialization.GENERAL };

            var filteredApointments = new List<Appointment>();

            if (patientReferrals.Count > 0)
                foreach (Referral referral in patientReferrals)
                    allowedSpecializations.Add(referral.Specialization);

            allowedSpecializations.Distinct().ToList();

            if (allowedSpecializations.Count > 0)
                filteredApointments.AddRange(
                    appointments.Where(app => allowedSpecializations.Any(
                        (spec) => spec == app.Doctor.Specialization)).ToList());

            return filteredApointments;
        }

        public List<Appointment> GetAvailableByDoctorSpeciality(Specialization specialization, DateTime start, DateTime end, TypeOfAppointment type)
        {
            List<Appointment> available = new List<Appointment>();

            var desiredSpecialists = doctorRepository.GetAllBySpecialty(specialization);

            foreach (Doctor doc in desiredSpecialists)
            {
                var availableForDoctor = GetAvailableByDoctorAndInterval(doc.Id, start, end, type);
                if (availableForDoctor.Count > 0)
                {
                    available.AddRange(availableForDoctor);
                    break;
                }
            }

            return available;
        }

        public List<Appointment> GetAvailableByDoctorAndInterval(int doctorId, DateTime start, DateTime end, TypeOfAppointment type)
        {
            List<Appointment> available = new List<Appointment>();

            var daysBetween = (end - start).TotalDays + 1;
            var iDay = new DateTime(start.Ticks);

            for (int i = 0; i < daysBetween; i++)
            {
                available.AddRange(GetAvailableByDoctorAndDate(doctorId, iDay, type));
                iDay = iDay.AddDays(1);
            }

            return available;
        }

        public List<Appointment> GetAvailableByDoctorAndDate(int doctorId, DateTime date, TypeOfAppointment type)
        {
            var taken = appointmentRepository.GetAllByDoctorAndDate(doctorId, date).ToList();
            foreach (Appointment appointment in taken)
                BindAppointmentProperties(appointment);

            List<Appointment> allPosibleAppointments = GetPosibleForWorkDay(doctorId, date, type);

            List<Appointment> available = new List<Appointment>();
            foreach (Appointment app in allPosibleAppointments)
            {
                if (taken.FirstOrDefault(a => a.IsOccupied(app.StartTime, app.EndTime) && !a.Canceled) != null)
                    continue;
                available.Add(app);
            }
            return available;
        }

        public List<Appointment> GetPosibleForWorkDay(int doctorId, DateTime date, TypeOfAppointment type)
        {
            List<Appointment> appointments = new List<Appointment>();
            WorkDay workDay = workDayRepository.GetByDoctorIdAndDate(doctorId, date.Date);

            if (workDay == null)
                return appointments;

            int startTime = workDay.StartTime;
            int endTime = workDay.EndTime;
            DateTime appointmentStart = new DateTime(date.Year, date.Month, date.Day, startTime, 0, 0);

            int duration = type == TypeOfAppointment.SURGERY ? appointmentDuration * 2 : appointmentDuration;
            int appointmentsForDay = type == TypeOfAppointment.SURGERY ? (endTime - startTime) * 2 : (endTime - startTime) * 2;


            for (int i = 0; i < appointmentsForDay; i++)
            {
                appointments.Add(
                    new Appointment
                    {
                        DoctorId = doctorId,
                        Doctor = workDay.Doctor,
                        StartTime = appointmentStart.AddMinutes(duration * i),
                        EndTime = appointmentStart.AddMinutes(duration * (i + 1)),
                        TypeOfAppointment = type
                    });
            }
            foreach (Appointment appointment in appointments)
                BindAppointmentProperties(appointment);

            return appointments;
        }

        private void BindAppointmentProperties(Appointment appointment)
        {
            appointment.Doctor = doctorRepository.GetById(appointment.DoctorId);
            appointment.Patient = patientRepository.GetById(appointment.PatientId);
        }

    }
}
