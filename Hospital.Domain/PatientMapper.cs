using Hospital.Domain.DTO;
using Hospital.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain
{
    public class PatientMapper
    {
        public static List<PatientDTO> PatientsToPatientsDTO(List<Patient> patients)
        {
            var patientDTOs = new List<PatientDTO>();
            foreach (Patient patient in patients)
                patientDTOs.Add(new PatientDTO()
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Username = patient.Username,
                    DoctorId = patient.ChosenDoctorId,
                    Blocked = patient.Blocked,
                });
            return patientDTOs;

        }
        public static PatientDTO PatientToPatientDTO(Patient patient)
        {
            if (patient == null) return null;
            return new PatientDTO()
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Username = patient.Username,
                DoctorId = patient.ChosenDoctorId,
                Blocked = patient.Blocked,
            };
            

        }
    }
}
