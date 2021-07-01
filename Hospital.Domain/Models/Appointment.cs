using Hospital.Domain.Enums;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.Domain.Models
{
    public class Appointment : IIdentifiable<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TypeOfAppointment TypeOfAppointment { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DoctorId { get; set; }
        [NotMapped]
        public virtual Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        [NotMapped]
        public virtual Patient Patient { get; set; }
        public string Description { get; set; }
        public bool Canceled { get; set; }


        public Appointment() { }

        public Appointment(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int GetId()
        {
            return Id;
        }

        public bool IsOccupied(DateTime start, DateTime end)
        {
            var isStartSame = DateTime.Compare(StartTime, start) == 0;
            var isEndSame = DateTime.Compare(EndTime, end) == 0;
            var isStartBetween = start.Ticks > StartTime.Ticks && start.Ticks < EndTime.Ticks;
            var isEndBetween = end.Ticks > StartTime.Ticks && end.Ticks < EndTime.Ticks;
            return isStartBetween || isEndBetween || isStartSame && isEndSame;
        }
    }
}
