using Hospital.Domain.Models;
using Hospital.Center.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository
{
    public class ReferralRepository : RepositoryBase<Referral, int>, IReferralRepository
    {
        public ReferralRepository(HospitalDbContext repositoryContext) : base(repositoryContext)
        {

        }
        public List<Referral> GetByPatientId(int id)
        {
            return FindByCondition(d => d.PatientId == id);
        }
        public List<Referral> GetUnusedByPatientId(int id)
        {
            return FindByCondition(d => d.PatientId == id && !d.Used);
        }
    }
}
