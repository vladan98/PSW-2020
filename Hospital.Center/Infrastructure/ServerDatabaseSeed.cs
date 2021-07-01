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
            SeedAppointments(context);
            SeedWorkDays(context);
            SeedReferrals(context);
            SeedFeedback(context);
        }
        private static void SeedAppointments(HospitalDbContext context)
        {
            context.Add(new Appointment
            {
                Id = 1,
                StartTime = new DateTime(2021, 7, 10, 14, 00, 0),
                EndTime = new DateTime(2021, 7, 10, 14, 15, 0),
                TypeOfAppointment = TypeOfAppointment.EXAMINATION,
                Description = "regular examination",
                DoctorId = 1,
                PatientId = 6
            });
            context.Add(new Appointment
            {
                Id = 2,
                StartTime = new DateTime(2021, 7, 10, 15, 00, 0),
                EndTime = new DateTime(2021, 7, 10, 15, 15, 0),
                TypeOfAppointment = TypeOfAppointment.EXAMINATION,
                Description = "regular examination",
                DoctorId = 1,
                PatientId = 6,
                Canceled = true
            });
            context.Add(new Appointment
            {
                Id = 3,
                StartTime = new DateTime(2021, 5, 11, 14, 00, 0),
                EndTime = new DateTime(2021, 5, 11, 14, 15, 0),
                TypeOfAppointment = TypeOfAppointment.EXAMINATION,
                Description = "regular examination",
                DoctorId = 1,
                PatientId = 7
            });
            context.Add(new Appointment
            {
                Id = 4,
                StartTime = new DateTime(2021, 5, 11, 14, 00, 0),
                EndTime = new DateTime(2021, 5, 11, 14, 15, 0),
                TypeOfAppointment = TypeOfAppointment.SURGERY,
                Description = "Hip removal",
                DoctorId = 2,
                PatientId = 6
            });

            context.SaveChanges();
        }


        private static void SeedWorkDays(HospitalDbContext context)
        {
            context.Add(new WorkDay { Date = new DateTime(2021, 07, 10), StartTime = 16, EndTime = 23, DoctorId = 1 });
            context.Add(new WorkDay { Date = new DateTime(2021, 07, 11), StartTime = 16, EndTime = 23, DoctorId = 1 });
            context.Add(new WorkDay { Date = new DateTime(2021, 07, 13), StartTime = 16, EndTime = 23, DoctorId = 1 });
            context.Add(new WorkDay { Date = new DateTime(2021, 07, 12), StartTime = 16, EndTime = 23, DoctorId = 4 });
            context.Add(new WorkDay { Date = new DateTime(2021, 07, 13), StartTime = 16, EndTime = 23, DoctorId = 2 });
            context.Add(new WorkDay { Date = new DateTime(2021, 07, 14), StartTime = 16, EndTime = 23, DoctorId = 3 });
            context.Add(new WorkDay { Date = new DateTime(2021, 07, 15), StartTime = 16, EndTime = 23, DoctorId = 2 });
            context.SaveChanges();
        }
        private static void SeedReferrals(HospitalDbContext context)
        {
            context.Add(new Referral { PatientId = 6, DoctorId = 2, Specialization = Specialization.SURGEON, Used = false });
            context.Add(new Referral { PatientId = 6, DoctorId = 2, Specialization = Specialization.SURGEON, Used = true });
            context.SaveChanges();
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
