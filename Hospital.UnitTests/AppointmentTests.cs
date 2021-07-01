using Hospital.Domain.DTO;
using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Users;
using FakeItEasy;
using Moq;
using Hospital.Center.Repository;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HospitalGroup.UnitTests
{
    public class AppointmentTests
    {

        [Fact]
        public void GetAllAppointments_Success()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.GetAll();

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public void GetUserAppointments_Success()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.UserAppointments(2);

            // Assert
            Assert.NotEmpty(response);
            Assert.Equal(2, response.Count);
        }

        [Fact]
        public void GetUserAppointments_Fail()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.UserAppointments(657);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public void ScheduleAppointment_Existing_Success()
        {
            // Arange
            var appointment = new Appointment
            {
                TypeOfAppointment = TypeOfAppointment.SURGERY,
                Description = "standard appointment",
                StartTime = new DateTime(2021, 7, 10, 18, 0, 0),
                EndTime = new DateTime(2021, 7, 10, 18, 30, 0),
                PatientId = 2,
                DoctorId = 6
            };
            var service = CreateAppointmentService();

            // Act
            var response = service.ScheduleAppointment(appointment,3);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public void ScheduleAppointment_Existing_Fail()
        {
            // Arange
            var appointment = new Appointment
            {
                TypeOfAppointment = TypeOfAppointment.EXAMINATION,
                Description = "standard appointment",
                StartTime = new DateTime(2021, 7, 10, 18, 0, 0),
                EndTime = new DateTime(2021, 7, 10, 18, 15, 0),
                PatientId = 2,
                DoctorId = 7
            };
            var service = CreateAppointmentService();

            // Act
            var response = service.ScheduleAppointment(appointment,9);

            // Assert
            Assert.False(response);
        }

        [Fact]
        public void ScheduleAppointment_New_Success()
        {
            // Arange
            var appointment = new Appointment
            {
                TypeOfAppointment = TypeOfAppointment.EXAMINATION,
                Description = "standard appointment",
                StartTime = new DateTime(2021, 8, 10, 18, 0, 0),
                EndTime = new DateTime(2021, 8, 10, 18, 15, 0),
                PatientId = 2,
                DoctorId = 7
            };
            var service = CreateAppointmentService();

            // Act
            var response = service.ScheduleAppointment(appointment,3);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public void ScheduleAppointment_New_Fail()
        {
            // Arange
            var appointment = new Appointment
            {
                TypeOfAppointment = TypeOfAppointment.EXAMINATION,
                Description = "standard appointment",
                StartTime = new DateTime(2021, 8, 10, 18, 0, 0),
                EndTime = new DateTime(2021, 8, 10, 18, 15, 0),
                PatientId = 2,
                DoctorId = 8
            };
            var service = CreateAppointmentService();

            // Act
            var response = service.ScheduleAppointment(appointment,3);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public void CancelAppointment_DoesntExist_Fail()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.CancelAppointment(5);

            // Assert
            Assert.False(response);
        }

        [Fact]
        public void CancelAppointment_AlreadyCanceled_Fail()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.CancelAppointment(1);

            // Assert
            Assert.False(response);
        }


        [Fact]
        public void CancelAppointment_TooSoon_Fail()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.CancelAppointment(3);

            // Assert
            Assert.False(response);
        }

        [Fact]
        public void CancelAppointment_Success()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.CancelAppointment(2);

            // Assert
            Assert.True(response);
        }


        [Fact]
        public void GetPosibleAppointmentsForWorkDay_Fail()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.GetPosibleForWorkDay(2, DateTime.Now, TypeOfAppointment.EXAMINATION);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public void GetPosibleAppointmentsForWorkDay_Succeess()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.GetPosibleForWorkDay(6, new DateTime(2021, 07, 10), TypeOfAppointment.EXAMINATION);

            // Assert
            Assert.NotEmpty(response);
        }


        [Fact]
        public void GetAvailableByDoctorAndDate_Fail()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.GetAvailableByDoctorAndDate(7, new DateTime(2021, 07, 11), TypeOfAppointment.EXAMINATION);

            // Assert
            Assert.Empty(response);
        }


        [Fact]
        public void GetAvailableByDoctorAndDate_Succeess()
        {
            // Arange
            var service = CreateAppointmentService();
            var allPosibleAppointments = service.GetPosibleForWorkDay(6, new DateTime(2021, 7, 10, 12, 0, 0), TypeOfAppointment.EXAMINATION);

            // Act
            var response = service.GetAvailableByDoctorAndDate(6, new DateTime(2021, 7, 10, 12, 0, 0), TypeOfAppointment.EXAMINATION);

            // Assert
            Assert.NotEmpty(response);
            Assert.Equal(allPosibleAppointments.Count, response.Count);
        }

        [Fact]
        public void GetAvailableByDoctorAndInterval_Succeess()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.GetAvailableByDoctorAndInterval(6, new DateTime(2021, 7, 9, 12, 0, 0), new DateTime(2021, 7, 12, 12, 0, 0), TypeOfAppointment.EXAMINATION);

            // Assert
            Assert.NotEmpty(response);
            Assert.Equal(14, response.Count);
        }


        [Fact]
        public void GetAvailableByDoctorAndInterval_Fail()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.GetAvailableByDoctorAndInterval(6, new DateTime(2021, 8, 9, 12, 0, 0), new DateTime(2021, 8, 12, 12, 0, 0), TypeOfAppointment.EXAMINATION);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public void GetAvailableByDoctorSpeciality_Success()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.GetAvailableByDoctorSpeciality(Specialization.GENERAL, new DateTime(2021, 7, 9, 19, 0, 0), new DateTime(2021, 7, 11, 19, 15, 0), TypeOfAppointment.EXAMINATION);

            // Assert
            Assert.NotEmpty(response);
            Assert.Equal(11, response.Count);
        }

        [Fact]
        public void GetAvailableByDoctorSpeciality_Fail()
        {
            // Arange
            var service = CreateAppointmentService();

            // Act
            var response = service.GetAvailableByDoctorSpeciality(Specialization.GENERAL, new DateTime(2021, 8, 10, 19, 0, 0), new DateTime(2021, 8, 10, 19, 15, 0), TypeOfAppointment.EXAMINATION);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public void FilterAllowedAppointments_Success()
        {
            // Arange
            var service = CreateAppointmentService();
            var appointments = CreateEagerAppointments();

            // Act
            var response = service.FilterAllowedAppointments(2, appointments);

            // Assert
            Assert.NotEmpty(response);
            Assert.Equal(4, response.Count);
        }

        [Fact]
        public void FilterAllowedAppointments_Fail()
        {
            // Arange
            var service = CreateAppointmentService();
            var appointments = CreateEagerAppointments();

            // Act
            var response = service.FilterAllowedAppointments(5, appointments);

            // Assert
            Assert.NotEmpty(response);
            Assert.Equal(3, response.Count);
        }

        [Fact]
        public void SearchAppointments_WithReferral_Succeess()
        {
            // Arange
            var service = CreateAppointmentService();

            var searchParams = new SearchParameters()
            {
                UserId = 2,
                DoctorId = 6,
                From = new DateTime(2021, 7, 9, 18, 0, 0),
                To = new DateTime(2021, 7, 11, 18, 0, 0),
                TypeOfAppointment = TypeOfAppointment.SURGERY
            };

            // Act
            var response = service.SearchAppointments(searchParams);

            // Assert
            Assert.NotEmpty(response);
        }


        [Fact]
        public void SearchAppointments_WithoutReferral_Fail()
        {
            // Arange
            var service = CreateAppointmentService();

            var searchParams = new SearchParameters()
            {
                UserId = 4,
                DoctorId = 6,
                From = new DateTime(2021, 7, 9, 18, 0, 0),
                To = new DateTime(2021, 7, 11, 18, 0, 0),
                TypeOfAppointment = TypeOfAppointment.SURGERY
            };


            // Act
            var response = service.SearchAppointments(searchParams);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public void SearchAppointments_WithDoctorPriority_Success()
        {
            // Arange
            var service = CreateAppointmentService();

            var searchParams = new SearchParameters()
            {
                UserId = 2,
                DoctorId = 6,
                From = new DateTime(2021, 7, 6, 18, 0, 0),
                To = new DateTime(2021, 7, 8, 18, 0, 0),
                Priority = SearchPriority.DOCTOR,
                TypeOfAppointment = TypeOfAppointment.SURGERY
            };

            // Act
            var response = service.SearchAppointments(searchParams);

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public void SearchAppointments_WithDoctorPriority_Fail()
        {
            // Arange
            var service = CreateAppointmentService();

            var searchParams = new SearchParameters()
            {
                UserId = 2,
                DoctorId = 6,
                From = new DateTime(2021, 8, 6, 18, 0, 0),
                To = new DateTime(2021, 8, 8, 18, 0, 0),
                Priority = SearchPriority.DOCTOR,
                TypeOfAppointment = TypeOfAppointment.SURGERY
            };

            // Act
            var response = service.SearchAppointments(searchParams);

            // Assert
            Assert.Empty(response);
        }

        public static IAppointmentService CreateAppointmentService()
            => new AppointmentService(
                CreateAppointmentRepositoryMock(),
                CreateDoctorRepositoryMock(),
                CreatePatientRepositoryMock(),
                CreateReferralRepositoryMock(),
                CreateWorkDayRepositoryMock());



        public static IAppointmentRepository CreateAppointmentRepositoryMock()
        {
            var appointmentRepositoryMock = new Mock<IAppointmentRepository>();

            var eagerAppointments = CreateEagerAppointments();

            appointmentRepositoryMock.Setup(x => x.FindAll()).Returns(eagerAppointments);

            appointmentRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => eagerAppointments.Where(app => app.Id == id).FirstOrDefault());

            appointmentRepositoryMock.Setup(x => x.GetAllByPatientId(It.IsAny<int>())).Returns(
                (int id) => eagerAppointments.Where(app => app.PatientId == id).ToList());

            appointmentRepositoryMock.Setup(x => x.GetAllByPatientAndMonth(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(
                (int id, DateTime month) => eagerAppointments.Where(a => a.PatientId.Equals(id) && a.StartTime.Month.CompareTo(month.Month) == 0).ToList());

            appointmentRepositoryMock.Setup(x => x.GetAllByDoctorAndDate(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(
                (int id, DateTime date) => eagerAppointments.Where(a => a.DoctorId.Equals(id) && a.StartTime.Date.CompareTo(date.Date) == 0).ToList());

            appointmentRepositoryMock.Setup(x => x.Update(It.IsAny<Appointment>())).Returns(
                (Appointment a) => eagerAppointments.Where(app => app.Id == a.Id).FirstOrDefault());

            appointmentRepositoryMock.Setup(x => x.GetSpecificByDoctor(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(
                (int id, DateTime start, DateTime end) => eagerAppointments.Where(a => a.DoctorId.Equals(id) && a.StartTime.CompareTo(start) == 0 && a.EndTime.CompareTo(end) == 0).ToList().FirstOrDefault());

            return appointmentRepositoryMock.Object;
        }

        public static IDoctorRepository CreateDoctorRepositoryMock()
        {
            var doctorRepositoryMock = new Mock<IDoctorRepository>();

            var doctors = CreateDoctors();
            doctorRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => doctors.Where(d => d.Id == id).FirstOrDefault());

            doctorRepositoryMock.Setup(x => x.GetAllBySpecialty(It.IsAny<Specialization>())).Returns(
                (Specialization s) => doctors.Where(d => d.Specialization == s).ToList());

            return doctorRepositoryMock.Object;
        }

        public static IReferralRepository CreateReferralRepositoryMock()
        {
            var referralRepositoryMock = new Mock<IReferralRepository>();

            var referrals = new List<Referral>() { CreateReferral() };

            referralRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => referrals.Where(r => r.Id == id).FirstOrDefault());

            referralRepositoryMock.Setup(x => x.GetUnusedByPatientId(It.IsAny<int>())).Returns(
                (int id) => referrals.Where(r => r.PatientId == id && !r.Used).ToList());


            referralRepositoryMock.Setup(x => x.Update(It.IsAny<Referral>())).Returns(
                (Referral r) => referrals.Where(refer => refer.Id == r.Id).FirstOrDefault());

            return referralRepositoryMock.Object;
        }

        public static IPatientRepository CreatePatientRepositoryMock()
        {
            var patientRepositoryMock = new Mock<IPatientRepository>();

            var patients = new List<Patient>() { CreatePatient(), CreateBlockedPatient() };

            patientRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => patients.Where(p => p.Id == id).FirstOrDefault());

            patientRepositoryMock.Setup(x => x.Update(It.IsAny<Patient>())).Returns(
                (Patient p) => patients.Where(pat => p.Id == pat.Id).FirstOrDefault());

            return patientRepositoryMock.Object;
        }
        public static IWorkDayRepository CreateWorkDayRepositoryMock()
        {
            var workdayRepositoryMock = new Mock<IWorkDayRepository>();

            var workDays = CreateWorkDays();

            workdayRepositoryMock.Setup(x => x.GetByDoctorIdAndDate(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(
                (int id, DateTime date) => workDays.Where(wd => wd.DoctorId == id && date.Date.CompareTo(wd.Date.Date) == 0).FirstOrDefault());

            return workdayRepositoryMock.Object;

        }

        #region data
        public static List<Appointment> CreateEagerAppointments()
        {
            return new List<Appointment>() {
                new Appointment
                {
                    Id = 1,
                    TypeOfAppointment = TypeOfAppointment.SURGERY,
                    Description = "SURGERY",
                    StartTime = new DateTime(2021, 7, 10, 18, 0, 0),
                    EndTime = new DateTime(2021, 7, 10, 18, 15, 0),
                    Canceled = true,
                    PatientId = 2,
                    DoctorId = 6,
                    Doctor = CreateDoctors()[0]
                },
                new Appointment
                {
                    Id = 2,
                    TypeOfAppointment = TypeOfAppointment.EXAMINATION,
                    Description = "standard appointment",
                    StartTime = new DateTime(2021, 7, 10, 18, 0, 0),
                    EndTime = new DateTime(2021, 7, 10, 18, 15, 0),
                    Canceled = false,
                    PatientId = 2,
                    DoctorId = 7,
                    Doctor = CreateDoctors()[1]
                },
                new Appointment
                {
                    Id = 3,
                    TypeOfAppointment = TypeOfAppointment.EXAMINATION,
                    Description = "standard appointment",
                    StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(1).AddMinutes(15),
                    Canceled = false,
                    PatientId = 3,
                    DoctorId = 7,
                    Doctor = CreateDoctors()[1]
                },
                new Appointment
                {
                    Id = 4,
                    TypeOfAppointment = TypeOfAppointment.EXAMINATION,
                    Description = "standard appointment",
                    StartTime = DateTime.Now.AddDays(9),
                    EndTime = DateTime.Now.AddDays(9).AddMinutes(15),
                    Canceled = false,
                    PatientId = 4,
                    DoctorId = 7,
                    Doctor = CreateDoctors()[1]
                }
            };

        }
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
        public static Patient CreateBlockedPatient()
        {
            return new Patient()
            {
                Id = 4,
                ChosenDoctorId = 7,
                Gender = Gender.MALE,
                FirstName = "Pacijent",
                LastName = "Pacijentovic",
                Username = "pacijent",
                Password = "pacijent",
                Role = Role.PATIENT,
                Blocked = true,
                ShouldBeBlocked = true

            };
        }
        public static List<Doctor> CreateDoctors()
        {
            return new List<Doctor>() {
                new Doctor()
                {
                    Id = 6,
                    Specialization = Specialization.SURGEON,
                    Gender = Gender.MALE,
                    FirstName = "Lekar2",
                    LastName = "Hirurgovic2",
                    Username = "hirurg2",
                    Password = "hirurg2",
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
        public static Referral CreateReferral()
        {
            return new Referral { Id = 3, PatientId = 2, DoctorId = 6, Specialization = Specialization.SURGEON, Used = false };

        }
        public static List<WorkDay> CreateWorkDays()
        {
            return new List<WorkDay>() {
                new WorkDay { Date = new DateTime(2021, 07, 10), StartTime = 16, EndTime = 23, DoctorId = 6, Doctor = CreateDoctors()[0] },
                new WorkDay { Date = new DateTime(2021, 07, 10), StartTime = 16, EndTime = 23, DoctorId = 7, Doctor = CreateDoctors()[1] }
            };
        }
        #endregion data
    }
}
