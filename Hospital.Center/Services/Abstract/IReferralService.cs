using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using System;
using System.Collections.Generic;

namespace Hospital.Center.Services.Abstract
{
    public interface IReferralService
    {
        List<ReferralDTO> GetByPatientId(int id);
        bool Create(Referral referral);
        bool UpdateUsed(int referralId);
    }
}
