using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.Domain.Models
{
    public class Feedback : IIdentifiable<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedAt { get; set; }

        public int GetId()
        {
            return Id;
        }
    }
}
