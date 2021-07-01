using Hospital.Domain.DTO;
using Hospital.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain
{
    public class DoctorMapper
    {

        public static List<DoctorDTO> DoctorsToDoctorsDTO(List<Doctor> doctors)
        {
            var doctorDTOs = new List<DoctorDTO>();
            foreach (Doctor doctor in doctors)
                doctorDTOs.Add(new DoctorDTO()
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Username = doctor.Username,
                    Specialization = doctor.Specialization,
                });
            return doctorDTOs;

        }
        public static DoctorDTO DoctorToDoctorDTO(Doctor doctor)
        {
            return new DoctorDTO()
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Username = doctor.Username,
                Specialization = doctor.Specialization,
            };

        }
    }
}
