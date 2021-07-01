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
using Hospital.Domain.Models;

namespace HospitalGroup.UnitTests
{
    public class ReferralTests
    {
        [Fact]
        public void UpdateUsed_Success()
        {
            // Arange
            var service = CreateReferralService();

            // Act
            var response = service.UpdateUsed(3);

            // Assert
            Assert.True(response);
        }
        [Fact]
        public void UpdateUsed_Fail()
        {
            // Arange
            var service = CreateReferralService();

            // Act
            var response = service.UpdateUsed(999);

            // Assert
            Assert.False(response);
        }
        [Fact]
        public void GetByPatientId_Fail()
        {
            // Arange
            var service = CreateReferralService();

            // Act
            var response = service.GetByPatientId(999);

            // Assert
            Assert.Empty(response);
        }
        [Fact]
        public void GetByPatientId_Success()
        {
            // Arange
            var service = CreateReferralService();

            // Act
            var response = service.GetByPatientId(2);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void CreateReferral_Success()
        {
            // Arange
            var service = CreateReferralService();
            var referral = new Referral()
            {
                PatientId = 3,
                DoctorId = 3,
                Specialization = Specialization.GENERAL
            };

            // Act
            var response = service.Create(referral);

            // Assert
            Assert.True(response);
        }

        public static IReferralService CreateReferralService()
            => new ReferralService(CreateReferralRepositoryMock(),CreateDoctorRepositoryMock());

        public static IDoctorRepository CreateDoctorRepositoryMock()
        {
            var doctorRepositoryMock = new Mock<IDoctorRepository>();

            var doctors = CreateDoctors();
            doctorRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => doctors.Where(d => d.Id == id).FirstOrDefault());

            return doctorRepositoryMock.Object;
        }

        public static IReferralRepository CreateReferralRepositoryMock()
        {
            var referralRepositoryMock = new Mock<IReferralRepository>();

            var referrals = new List<Referral>() { CreateReferral() };

            referralRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => referrals.Where(r => r.Id == id).FirstOrDefault());

            referralRepositoryMock.Setup(x => x.GetByPatientId(It.IsAny<int>())).Returns(
                (int id) => referrals.Where(r => r.PatientId == id).ToList());

            referralRepositoryMock.Setup(x => x.Update(It.IsAny<Referral>())).Returns(
                (Referral r) => referrals.Where(refer => refer.Id == r.Id).FirstOrDefault());

            referralRepositoryMock.Setup(x => x.Create(It.IsAny<Referral>())).Returns((Referral r) => r);

            return referralRepositoryMock.Object;
        }

        #region data

        public static Referral CreateReferral()
        {
            return new Referral { Id = 3, PatientId = 2, DoctorId = 6, Specialization = Specialization.SURGEON, Used = false };

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
        #endregion data
    }

}
