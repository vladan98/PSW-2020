using Hospital.Domain;
using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Services
{
    public class ReferralService : IReferralService
    {
        private readonly IReferralRepository referralRepository;
        private readonly IDoctorRepository doctorRepository;

        public ReferralService(IReferralRepository refRepository, IDoctorRepository docRepository)
        {
            referralRepository = refRepository;
            doctorRepository = docRepository;
        }
        public bool Create(Referral referral)
        {
            var done = referralRepository.Create(referral);
            if (done != null)
                return true;
            return false;
        }

        public List<ReferralDTO> GetByPatientId(int id)
        {
            var all = referralRepository.GetByPatientId(id);
            foreach (Referral referral in all)
                BindProperties(referral);
            return ReferralMapper.ReferralListToReferralDTOList(all);
        }
        
        
        public bool UpdateUsed(int referralId)
        {
            var referral = referralRepository.GetById(referralId);
            if (referral == null)
                return false;
            referral.Used = true;
            var done = referralRepository.Update(referral);
            if (done != null)
                return true;
            return false;
        }


        private void BindProperties(Referral referral)
        {
            referral.Doctor = doctorRepository.GetById(referral.DoctorId);
        }


    }
}
