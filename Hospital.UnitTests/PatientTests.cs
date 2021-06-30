using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Users;
using FakeItEasy;
using Moq;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services;
using Hospital.Center.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HospitalGroup.UnitTests
{
    public class PatientTests
    {
        [Fact]
        public void GetAllPatients_Success()
        {
            // Arange
            var service = CreatePatientService();

            // Act
            var response = service.GetAll();

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public void GetAllMalicious_Success()
        {
            // Arange
            var service = CreatePatientService();

            // Act
            var response = service.GetAll();

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public void BlockPatient_Success()
        {
            // Arange
            var service = CreatePatientService();

            // Act
            var response = service.BlockPatient(4);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public void BlockPatient_AlreadyBlocked_Fail()
        {
            // Arange
            var service = CreatePatientService();

            // Act
            var response = service.BlockPatient(5);

            // Assert
            Assert.False(response);
        }

        [Fact]
        public void BlockPatient_DoesntExist_Fail()
        {
            // Arange
            var service = CreatePatientService();

            // Act
            var response = service.BlockPatient(123);

            // Assert
            Assert.False(response);
        }

        [Fact]
        public void RegisterPatient_AlreadyExists_Fail()
        {
            // Arange
            var service = CreatePatientService();
            var patient = new Patient()
            {
                ChosenDoctorId = 7,
                Gender = Gender.MALE,
                FirstName = "Pacijent",
                LastName = "Pacijentovic",
                Username = "pacijent1",
                Password = "pacijent",
                Role = Role.PATIENT,
            };

            // Act
            var response = service.Register(patient);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public void RegisterPatient_Success()
        {
            // Arange
            var service = CreatePatientService();
            var patient = new Patient()
            {
                ChosenDoctorId = 7,
                Gender = Gender.MALE,
                FirstName = "Pacijent",
                LastName = "Pacijentovic",
                Username = "pacijent42",
                Password = "pacijent",
                Role = Role.PATIENT,
            };

            // Act
            var response = service.Register(patient);

            // Assert
            Assert.NotNull(response);
        }

        public static IPatientRepository CreatePatientRepositoryMock()
        {
            var patientRepositoryMock = new Mock<IPatientRepository>();

            var patients = new List<Patient>() { CreatePatient(), CreateBlockedPatient(), CreateMaliciousPatient() };

            patientRepositoryMock.Setup(x => x.FindAll()).Returns(patients);

            patientRepositoryMock.Setup(x => x.GetMalicious()).Returns(patients.Where(p => p.ShouldBeBlocked).ToList());

            patientRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => patients.Where(p => p.Id == id).FirstOrDefault());

            patientRepositoryMock.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(
                (string username) => patients.Where(p => p.Username == username).FirstOrDefault());

            patientRepositoryMock.Setup(x => x.Create(It.IsAny<Patient>())).Returns(
                (Patient p) => patients.Any(pat => pat.Username == p.Username) ? null : p);

            patientRepositoryMock.Setup(x => x.Update(It.IsAny<Patient>())).Returns(
                (Patient p) => patients.Where(pat => p.Id == pat.Id).FirstOrDefault());

            return patientRepositoryMock.Object;
        }

        public static IPatientService CreatePatientService()
            => new PatientService(CreatePatientRepositoryMock());


        public static Patient CreatePatient()
        {
            return new Patient()
            {
                Id = 2,
                ChosenDoctorId = 6,
                Gender = Gender.MALE,
                FirstName = "Pacijent",
                LastName = "Pacijentovic",
                Username = "pacijent",
                Password = "pacijent",
                Role = Role.PATIENT,
                Blocked = false,
                ShouldBeBlocked = false

            };
        }
        public static Patient CreateMaliciousPatient()
        {
            return new Patient()
            {
                Id = 4,
                ChosenDoctorId = 7,
                Gender = Gender.MALE,
                FirstName = "Pacijent",
                LastName = "Pacijentovic",
                Username = "pacijent1",
                Password = "pacijent",
                Role = Role.PATIENT,
                Blocked = false,
                ShouldBeBlocked = true
            };
        }
        public static Patient CreateBlockedPatient()
        {
            return new Patient()
            {
                Id = 5,
                ChosenDoctorId = 7,
                Gender = Gender.MALE,
                FirstName = "Pacijent",
                LastName = "Pacijentovic",
                Username = "pacijent2",
                Password = "pacijent",
                Role = Role.PATIENT,
                Blocked = true,
                ShouldBeBlocked = true

            };
        }
    }
}
