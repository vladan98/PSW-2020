using Hospital.Domain;
using Hospital.Domain.DTO;
using Hospital.Domain.Models.Users;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services.Abstract;
using System.Collections.Generic;

namespace Hospital.Center.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository patientRepository;

        public PatientService(IPatientRepository patRepository)
        {
            patientRepository = patRepository;
        }

        public Patient GetById(int id)
            => patientRepository.GetById(id);

        public List<PatientDTO> GetAll()
            => PatientMapper.PatientsToPatientsDTO(patientRepository.FindAll());

        public List<PatientDTO> GetMalicious()
            => PatientMapper.PatientsToPatientsDTO(patientRepository.GetMalicious());

        public bool BlockPatient(int id)
        {
            var patient = patientRepository.GetById(id);
            if (patient == null || patient.Blocked)
                return false;

            patient.Blocked = true;
            patientRepository.Update(patient);
            return true;
        }

        public PatientDTO Register(Patient pat)
        {
            var exists = patientRepository.GetByUsername(pat.Username);
            if (exists != null)
                return null;

            var patient = patientRepository.Create(pat);
            if (patient == null)
                return null;
            return PatientMapper.PatientToPatientDTO(patient);
        }

    }
}
