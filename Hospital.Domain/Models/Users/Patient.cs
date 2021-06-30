using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Domain.Models.Users
{
    [Table("Patients")]
    public class Patient : RegisteredUser
    {
        [ForeignKey("ChosenDoctor")]
        public int ChosenDoctorId { get; set; }
        public virtual Doctor ChosenDoctor { get; set; }
        public bool Blocked { get; set; }
        public bool ShouldBeBlocked { get; set; }

        public Patient() { }

        public Patient(int chosenDoctorId, Doctor chosenDoctor, bool blocked, bool shouldBeBlocked)
        {
            ChosenDoctorId = chosenDoctorId;
            ChosenDoctor = chosenDoctor;
            Blocked = blocked;
            ShouldBeBlocked = shouldBeBlocked;
        }
    }
}
