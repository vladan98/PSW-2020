using Hospital.Domain;
using Hospital.Domain.Enums;
using Hospital.Domain.Models.Users;
using Hospital.Center.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Center.Repository
{
    public class DoctorRepository : RepositoryBase<Doctor, int>, IDoctorRepository
    {
        public DoctorRepository(HospitalDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public Doctor GetById(int id)
        {
            return FindByCondition(d => d.Id == id).FirstOrDefault();
        }

        public List<Doctor> GetGeneral()
        {
            return FindByCondition(d => d.Specialization == Specialization.GENERAL);
        }
        public List<Doctor> GetAllBySpecialty(Specialization specialization)
        {
            return FindByCondition(d => d.Specialization == specialization);
        }
    }
}
