using Hospital.Domain.Enums;
using Hospital.Domain.Models.Users;
using Hospital.Center.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Repository.Abstract
{
    public interface IDoctorRepository : IRepositoryBase<Doctor, int>
    {
        Doctor GetById(int id);
        List<Doctor> GetAllBySpecialty(Specialization specialization);
        List<Doctor> GetGeneral();
    }
}

