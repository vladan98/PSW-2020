using Hospital.Domain.Models.Users;
using Hospital.Center.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository.Abstract
{
    public interface IPatientRepository : IRepositoryBase<Patient, int>
    {
        Patient GetById(int id);
        Patient GetByUsername(string username);
        List<Patient> GetMalicious();
    }
}
