using Hospital.Domain.Enums;
using Hospital.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Hospital.Domain.Models.Users
{
    public abstract class RegisteredUser : IIdentifiable<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        public RegisteredUser() { }

        public RegisteredUser(string username, string password, string role, string firstName, string lastName, Gender gender)
        {
            Username = username;
            Password = password;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
        }

        public int GetId()
        {
            return Id;
        }

    }
}
