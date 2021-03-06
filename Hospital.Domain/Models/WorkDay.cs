using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class WorkDay : IIdentifiable<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public int GetId()
        {
            return Id;
        }
    }
}
