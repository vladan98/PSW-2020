using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Users;
using Hospital.Center.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.Infrastructure
{
    public class ServerDatabaseSeed
    {
        private readonly HospitalDbContext context;
        public ServerDatabaseSeed(HospitalDbContext ctx)
        {
            context = ctx;
        }

        public void Seed()
        {
            SeedUsers(context);
            SeedFeedback(context);
        }
        private static void SeedUsers(HospitalDbContext context)
        {
            context.Add(new Doctor()
            {
                Id = 1,
                Specialization = Specialization.SURGEON,
                Gender = Gender.MALE,
                FirstName = "Lekar",
                LastName = "Hirurgovic",
                Username = "hirurg",
                Password = "hirurg",
                Role = Role.DOCTOR
            });

            context.Add(new Doctor()
            {
                Id = 2,
                Specialization = Specialization.SURGEON,
                Gender = Gender.MALE,
                FirstName = "Lekar",
                LastName = "Hirurgovic2",
                Username = "hirurg2",
                Password = "hirurg2",
                Role = Role.DOCTOR
            });

            context.Add(new Doctor()
            {
                Id = 3,
                Specialization = Specialization.GENERAL,
                Gender = Gender.MALE,
                FirstName = "Lekar3",
                LastName = "Opste",
                Username = "opsti",
                Password = "opsti",
                Role = Role.DOCTOR
            });

            context.Add(new Doctor()
            {
                Id = 4,
                Specialization = Specialization.GENERAL,
                Gender = Gender.MALE,
                FirstName = "Lekar4",
                LastName = "Opste",
                Username = "opsti",
                Password = "opsti",
                Role = Role.DOCTOR
            });

            context.Add(new Administrator()
            {
                Id = 5,
                Gender = Gender.MALE,
                FirstName = "Admin",
                LastName = "Adminovic",
                Username = "admin",
                Password = "admin",
                Role = Role.ADMIN
            });

            context.Add(new Patient()
            {
                Id = 6,
                ChosenDoctorId = 4,
                Gender = Gender.MALE,
                FirstName = "Pacijent",
                LastName = "Pacijentovic",
                Username = "pacijent",
                Password = "pacijent",
                Role = Role.PATIENT,
                Blocked = false,
                ShouldBeBlocked = false
            });
            context.Add(new Patient()
            {
                Id = 7,
                ChosenDoctorId = 4,
                Gender = Gender.MALE,
                FirstName = "Pacijent2",
                LastName = "Pacijentovic2",
                Username = "pacijent2",
                Password = "pacijent2",
                Role = Role.PATIENT,
                Blocked = false,
                ShouldBeBlocked = true
            });
            context.Add(new Patient()
            {
                Id = 8,
                ChosenDoctorId = 4,
                Gender = Gender.MALE,
                FirstName = "Pacijent",
                LastName = "Pacijentovic",
                Username = "blocked",
                Password = "blocked",
                Role = Role.PATIENT,
                Blocked = true,
                ShouldBeBlocked = false
            });
            context.SaveChanges();
        }
        private static void SeedFeedback(HospitalDbContext context)
        {
            context.Add(new Feedback { Id = 1, PatientId = 7, Title = "Broken", Content = "Terible place, never coming again!", Published = false });
            context.Add(new Feedback { Id = 2, PatientId = 7, Title = "Doctors", Content = "Great job!", Published = true });
            context.Add(new Feedback { Id = 3, PatientId = 7, Title = "Hospital", Content = "It can get too cold sometimes", Published = true });
            context.Add(new Feedback { Id = 4, PatientId = 7, Title = "Nurce", Content = "Maria helped me lot!", Published = true });
            context.Add(new Feedback { Id = 5, PatientId = 7, Title = "Patients", Content = "Provide some more water!", Published = false });
            context.SaveChanges();
        }


    }
}
