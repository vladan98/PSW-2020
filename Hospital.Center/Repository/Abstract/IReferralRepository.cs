using Hospital.Domain.Models;
using Hospital.Center.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository.Abstract
{
    public interface IReferralRepository : IRepositoryBase<Referral, int>
    {
        public List<Referral> GetByPatientId(int id);
        public List<Referral> GetUnusedByPatientId(int id);
    }
}
