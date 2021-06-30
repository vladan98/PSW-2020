using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models.Users
{
    [Table("Doctors")]
    public class Doctor : RegisteredUser
    {
        public Doctor()
        {
        }

        public Specialization Specialization { get; set; }


    }
}
