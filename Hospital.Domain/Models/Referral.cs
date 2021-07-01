using Hospital.Domain.Enums;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class Referral : IIdentifiable<int>
    {
        public int Id { get; set; }
        public Specialization Specialization { get; set; }
        public bool Used { get; set; }
        public int DoctorId { get; set; }
        [NotMapped]
        public virtual Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        [NotMapped]
        public virtual Patient Patient { get; set; }

        public int GetId()
        {
            return Id;
        }
    }
}
