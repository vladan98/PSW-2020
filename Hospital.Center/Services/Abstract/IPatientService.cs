using Hospital.Domain.DTO;
using Hospital.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Services.Abstract
{
    public interface IPatientService
    {
        List<PatientDTO> GetAll();
        List<PatientDTO> GetMalicious();
        Patient GetById(int id);
        PatientDTO Register(Patient pat);
        bool BlockPatient(int id);
    }
}
