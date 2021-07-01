using Hospital.Domain.Models;
using Hospital.Center.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository
{
    public class WorkDayRepository : RepositoryBase<WorkDay, int>, IWorkDayRepository
    {
        public WorkDayRepository(HospitalDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public WorkDay GetByDoctorId(int id)
        {
            return FindByCondition(wd => wd.DoctorId == id).FirstOrDefault();

        }

        public WorkDay GetByDoctorIdAndDate(int id, DateTime date)
        {
            return FindByCondition(wd => wd.DoctorId == id && date.Date.CompareTo(wd.Date) == 0).FirstOrDefault();
        }
    }
}
