using Hospital.Domain.Models;
using Hospital.Center.Interfaces;
using System;

namespace Hospital.Center.Repository.Abstract
{
    public interface IWorkDayRepository : IRepositoryBase<WorkDay, int>
    {
        WorkDay GetByDoctorId(int id);
        WorkDay GetByDoctorIdAndDate(int id, DateTime date);
    }
}
