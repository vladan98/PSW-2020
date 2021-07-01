using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Users;
using FakeItEasy;
using Moq;
using Hospital.Center.Repository;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services;
using Hospital.Center.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HospitalGroup.UnitTests
{
    public class FeebackTests
    {
        [Fact]
        public void GetPublishedFeedback_Success()
        {
            // Arange
            var service = CreateFeedbackService();

            // Act
            var response = service.GetPublished();

            // Assert
            Assert.True(response.All(f => f.Published));
        }
        [Fact]
        public void UpdatePublishedFeedback_Fail()
        {
            // Arange
            var service = CreateFeedbackService();

            // Act
            var response = service.UpdatePublished(3);

            // Assert
            Assert.False(response);
        }
        [Fact]
        public void UpdatePublishedFeedback_Success()
        {
            // Arange
            var service = CreateFeedbackService();

            // Act
            var response = service.UpdatePublished(1);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public void LeaveFeedback_Success()
        {
            // Arange
            var service = CreateFeedbackService();
            var feedback = new Feedback
            {
                Title = "Doctors",
                Content = "Amazing!",
                Published = false,
                PatientId = 6,
                CreatedAt = new DateTime(2021, 6, 5, 14, 0, 0),
            };

            // Act
            var response = service.LeaveFeedback(feedback);

            // Assert
            Assert.True(response);
        }

        public static IFeedbackRepository CreateFeedbackRepositoryMock()
        {
            var feedbackRepositoryMock = new Mock<IFeedbackRepository>();

            var feedback = CreateListOfFeedback();

            feedbackRepositoryMock.Setup(x => x.FindAll()).Returns(feedback);

            feedbackRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => feedback.Where(f => f.Id == id).FirstOrDefault());

            feedbackRepositoryMock.Setup(x => x.Update(It.IsAny<Feedback>())).Returns(
                (Feedback f) => feedback.Where(fdbk => f.Id == fdbk.Id).FirstOrDefault());

            feedbackRepositoryMock.Setup(x => x.Create(It.IsAny<Feedback>())).Returns(
                (Feedback f) => feedback.Any(fdbk => fdbk.Id == f.Id) ? null : f);

            feedbackRepositoryMock.Setup(x => x.GetPublished()).Returns(
                () => feedback.Where(f => f.Published).ToList());

            return feedbackRepositoryMock.Object;
        }

        public static IPatientRepository CreatePatientRepositoryMock()
        {
            var patientRepositoryMock = new Mock<IPatientRepository>();

            var patients = CreatePatients();

            patientRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => patients.Where(p => p.Id == id).FirstOrDefault());

            return patientRepositoryMock.Object;
        }

        public static IFeedbackService CreateFeedbackService()
            => new FeedbackService(CreateFeedbackRepositoryMock(), CreatePatientRepositoryMock());


        #region data

        public static List<Feedback> CreateListOfFeedback()
        {
            return new List<Feedback>(){
                new Feedback
                {
                    Id = 1,
                    Title = "Doctors",
                    Content = "Amazing!",
                    Published = false,
                    PatientId = 6,
                    CreatedAt = new DateTime(2021, 6, 5, 14, 0, 0),
                },
                new Feedback
                {
                    Id = 2,
                    Title = "Nurse",
                    Content = "AmAazing!",
                    Published = true,
                    PatientId = 6,
                    CreatedAt = new DateTime(2021, 6, 6, 13, 0, 0),
                }
            };
        }
        public static Feedback CreateFeedback()
        {
            return new Feedback
            {
                Id = 1,
                Title = "Doctors",
                Content = "Amazing!",
                Published = false,
                PatientId = 6,
                CreatedAt = new DateTime(2021, 6, 5, 14, 0, 0),
            };
        }
        public static Feedback CreatePublishedFeedback()
        {
            return new Feedback
            {
                Id = 1,
                Title = "Doctors",
                Content = "Amazing!",
                Published = true,
                PatientId = 6,
                CreatedAt = new DateTime(2021, 6, 5, 14, 0, 0),
            };
        }
        public static List<Patient> CreatePatients()
        {
            return new List<Patient>() {
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
                    ChosenDoctorId = 3,
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
        #endregion data
    }
}
