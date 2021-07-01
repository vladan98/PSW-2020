using Hospital.Domain.Models.Users;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Domain;
using Hospital.Domain.DTO;

namespace Hospital.Center.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorService(IDoctorRepository docRepository)
        {
            doctorRepository = docRepository;
        }

        public List<DoctorDTO> GetAll()
            => DoctorMapper.DoctorsToDoctorsDTO(doctorRepository.FindAll());

        public List<DoctorDTO> GetGeneral()
            => DoctorMapper.DoctorsToDoctorsDTO(doctorRepository.GetGeneral());

        public Doctor Create(Doctor doctor)
            => doctorRepository.Create(doctor);
    }
}
