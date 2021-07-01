using Hospital.Domain.DTO;
using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Domain
{
    public static class AppointmentMapper
    {
        public static SearchParameters SearchDTOToSearchParameters(SearchAppointmentsDTO dto)
        {
            return new SearchParameters()
            {
                UserId = dto.UserId,
                DoctorId = dto.DoctorId,
                From = dto.From,
                To = dto.To,
                Priority = CastPriority(dto.Priority),
                TypeOfAppointment = CastTypeOfAppointment(dto.TypeOfAppointment)
            };
        }
        public static Appointment AppointmentDTOToAppointment(AppointmentDTO dto)
        {
            var type = CastTypeOfAppointment(dto.TypeOfAppointment);
            var endTime = dto.StartTime.AddMinutes(type == TypeOfAppointment.EXAMINATION ? 15 : 30);
            return new Appointment()
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                StartTime = dto.StartTime,
                EndTime = endTime,
                TypeOfAppointment = type,
            };
        }
        public static List<AppointmentDTO> AppointmentsToAppointmentsDTOs(List<Appointment> appointments)
        {
            var mapped = new List<AppointmentDTO>();
            foreach (Appointment appointment in appointments)
            {
                mapped.Add(new AppointmentDTO()
                {
                    Id = appointment.Id,
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    Patient = PatientMapper.PatientToPatientDTO(appointment.Patient),
                    Doctor = DoctorMapper.DoctorToDoctorDTO(appointment.Doctor),
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                    TypeOfAppointment = (int) appointment.TypeOfAppointment,
                    Description = appointment.Description,
                });

            }

            return mapped;
        }

        public static UserAppointmentsDTO AppointmentsToUserAppointmentsDTO(List<Appointment> appointments)
        {
            return new UserAppointmentsDTO()
            {
                previousAppointments = AppointmentsToAppointmentsDTOs(appointments.Where(app => app.EndTime.CompareTo(DateTime.Now) < 0).ToList()),
                futureAppointments = AppointmentsToAppointmentsDTOs(appointments.Where(app => app.EndTime.CompareTo(DateTime.Now) > 0).ToList())
            };
        }

        private static SearchPriority CastPriority(int priority)
        {
            if (priority == 1)
                return SearchPriority.DOCTOR;
            if (priority == 2)
                return SearchPriority.DATE;
            return SearchPriority.NONE;
        }
        private static TypeOfAppointment CastTypeOfAppointment(int type)
        {
            if (type == 1)
                return TypeOfAppointment.SURGERY;
            return TypeOfAppointment.EXAMINATION;
        }
    }
}
