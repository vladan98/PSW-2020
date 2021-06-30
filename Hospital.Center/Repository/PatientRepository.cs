using Hospital.Domain;
using Hospital.Domain.Models.Users;
using Hospital.Center.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Center.Repository
{
    public class PatientRepository : RepositoryBase<Patient, int>, IPatientRepository
    {
        public PatientRepository(HospitalDbContext repositoryContext) : base(repositoryContext) { }

        public Patient GetById(int id)
            => FindByCondition(p => p.Id == id).FirstOrDefault();

        public Patient GetByUsername(string username)
            => FindByCondition(p => p.Username == username).FirstOrDefault();

        public List<Patient> GetMalicious()
            => FindByCondition(p => p.ShouldBeBlocked).ToList();
    }

}
