using Hospital.Domain.Enums;
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
    public class DoctorTests
    {
        [Fact]
        public void GetAllPatients_Success()
        {
            // Arange
            var service = CreateDoctorService();

            // Act
            var response = service.GetAll();

            // Assert
            Assert.NotEmpty(response);
        }
        [Fact]
        public void Create_Success()
        {
            // Arange
            var service = CreateDoctorService();
            var doctor = new Doctor()
            {
                Specialization = Specialization.SURGEON,
                Gender = Gender.MALE,
                FirstName = "Lekar2",
                LastName = "Hirurgovic1231",
                Username = "hirurg12312",
                Password = "hirurg12312"
            };

            // Act
            var response = service.Create(doctor);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void Create_AlreadyExist_Fail()
        {
            // Arange
            var service = CreateDoctorService();
            var doctor = new Doctor()
            {
                Specialization = Specialization.SURGEON,
                Gender = Gender.MALE,
                FirstName = "Lekar3",
                LastName = "Hirurgovic",
                Username = "hirurg1",
                Password = "hirurg1"
            };

            // Act
            var response = service.Create(doctor);

            // Assert
            Assert.Null(response);
        }

        public static IDoctorService CreateDoctorService()
            => new DoctorService(CreateDoctorRepositoryMock());

        public static IDoctorRepository CreateDoctorRepositoryMock()
        {
            var doctorRepositoryMock = new Mock<IDoctorRepository>();

            var doctors = CreateDoctors();
            doctorRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => doctors.Where(d => d.Id == id).FirstOrDefault());

            doctorRepositoryMock.Setup(x => x.Create(It.IsAny<Doctor>())).Returns(
                (Doctor doc) => doctors.Any(d => doc.Username == d.Username) ? null : doc);

            doctorRepositoryMock.Setup(x => x.FindAll()).Returns(doctors);

            return doctorRepositoryMock.Object;
        }

        public static List<Doctor> CreateDoctors()
        {
            return new List<Doctor>() {
                new Doctor()
                {
                    Id = 6,
                    Specialization = Specialization.SURGEON,
                    Gender = Gender.MALE,
                    FirstName = "Lekar1",
                    LastName = "Hirurgovic2",
                    Username = "hirurg1",
                    Password = "hirurg1",
                    Role = Role.DOCTOR
                },
                new Doctor()
                {
                    Id = 7,
                    Specialization = Specialization.GENERAL,
                    Gender = Gender.MALE,
                    FirstName = "Lekar2",
                    LastName = "Hirurgovic2",
                    Username = "hirurg2",
                    Password = "hirurg2",
                    Role = Role.DOCTOR
                }
            };

        }
    }

}
