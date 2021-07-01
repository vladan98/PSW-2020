using Hospital.Domain.DTO;
using Hospital.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Services.Abstract
{
    public interface IDoctorService
    {
        List<DoctorDTO> GetAll();
        Doctor Create(Doctor doctor);
        List<DoctorDTO> GetGeneral();
    }
}
