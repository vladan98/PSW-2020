using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Users;
using Moq;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services;
using Hospital.Center.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HospitalGroup.UnitTests
{
    public class AuthTests
    {
        [Fact]
        public void GetAllUsers_Fail()
        {
            // Arange
            var service = CreateUserRepositoryMock();

            // Act
            var response = service.FindAll();

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void RegisterUser_AlreadyExists_Fail()
        {
            // Arange
            var service = CreateUserRepositoryMock();
            var patient = new Patient()
            {
                ChosenDoctorId = 7,
                Gender = Gender.MALE,
                FirstName = "Pacijent",
                LastName = "Pacijentovic",
                Username = "pacijent",
                Password = "pacijent",
                Role = Role.PATIENT,
            };

            // Act
            var response = service.RegisterUser(patient);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public void RegisterPatient_Success()
        {
            // Arange
            var service = CreatePatientService();
            var user = new Patient()
            {
                Gender = Gender.MALE,
                FirstName = "Pacijent",
                LastName = "Pacijentovic",
                Username = "pacijent31",
                Password = "pacijent13",
                Role = Role.PATIENT
            };

            // Act
            var response = service.RegisterUser(user);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void GetByUsername_Success()
        {
            // Arange
            var service = CreatePatientService();

            // Act
            var response = service.GetByUsername("pacijent");

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void GetByUsername_Fail()
        {
            // Arange
            var service = CreatePatientService();

            // Act
            var response = service.GetByUsername("patient312");

            // Assert
            Assert.Null(response);
        }

        public static IRegisteredUserRepository CreateUserRepositoryMock()
        {
            var patientRepositoryMock = new Mock<IRegisteredUserRepository>();

            var patients = CreateUsers();

            patientRepositoryMock.Setup(x => x.FindAll()).Returns(patients);

            patientRepositoryMock.Setup(x => x.RegisterUser(It.IsAny<Patient>())).Returns(
                (Patient p) => patients.Any(pat => pat.Username == p.Username) ? null : p);

            patientRepositoryMock.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(
                (string username) => patients.Where(pat => pat.Username == username).FirstOrDefault());

            return patientRepositoryMock.Object;
        }

        public static IRegisteredUserService CreatePatientService()
            => new RegisteredUserService(CreateUserRepositoryMock());

        public static List<RegisteredUser> CreateUsers()
        {
            return new List<RegisteredUser>()
            {
                new Doctor()
                {
                    Id = 4,
                    Specialization = Specialization.GENERAL,
                    Gender = Gender.MALE,
                    FirstName = "Lekar4",
                    LastName = "Opste",
                    Username = "opsti",
                    Password = "opsti",
                    Role = Role.DOCTOR
                },
                new Administrator()
                {
                    Id = 5,
                    Gender = Gender.MALE,
                    FirstName = "Admin",
                    LastName = "Adminovic",
                    Username = "admin",
                    Password = "admin",
                    Role = Role.ADMIN
                },
                new Patient()
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
                },
                new Patient()
                {
                    Id = 7,
                    ChosenDoctorId = 4,
                    Gender = Gender.MALE,
                    FirstName = "Pacijent2",
                    LastName = "Pacijentovic2",
                    Username = "pacijent",
                    Password = "pacijent",
                    Role = Role.PATIENT,
                    Blocked = false,
                    ShouldBeBlocked = false

                }
            };
        }
    }
}