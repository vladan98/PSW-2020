using Hospital.Domain.DTO;
using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain
{
    public class ReferralMapper
    {
        public static Referral CreateReferralDTOToReferral(ReferralCreateDTO dto)
        {
            return new Referral()
            {
                Specialization = CastSpecialization(dto.Specialization),
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Used = false
            };
        }

        public static List<ReferralDTO> ReferralListToReferralDTOList(List<Referral> referrals)
        {
            var referralsDTOList = new List<ReferralDTO>();
            foreach (Referral referral in referrals)
            {
                referralsDTOList.Add(new ReferralDTO()
                {
                    Id = referral.Id,
                    Used = referral.Used,
                    Specialization = (int) referral.Specialization,
                    DoctorFullName = referral.Doctor.FirstName + " " + referral.Doctor.LastName,
                    DoctorId = referral.DoctorId
                });
            }
            return referralsDTOList;
        }

        private static Specialization CastSpecialization(int spec)
        {
            if (spec == 1) return Specialization.PEDIATRICAN;
            if (spec == 2) return Specialization.OPHTHALOMOGIST;
            if (spec == 3) return Specialization.SURGEON;
            return Specialization.GENERAL;
        }
    }
}