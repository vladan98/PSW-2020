
using Hospital.Domain.DTO;
using Hospital.Domain.Models;
using HospitalGroup.Center;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalGroup.IntegrationTests
{
    public class AppointmentTests
    {
        private readonly HttpClient client;

        public AppointmentTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            client = appFactory.CreateClient();
        }

        [Fact]
        public async Task AppointmentHistory_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/appointments/user/6");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<UserAppointmentsDTO>(body);

            // Assert
            Assert.NotEmpty(jsonBody.previousAppointments);
            Assert.NotEmpty(jsonBody.futureAppointments);
        }

        [Fact]
        public async Task AppointmentHistory_Fail()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/appointments/user/1235");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<UserAppointmentsDTO>(body);

            // Assert
            Assert.Empty(jsonBody.previousAppointments);
            Assert.Empty(jsonBody.futureAppointments);
        }

        [Fact]
        public async Task CancelAppointment_DoesntExist_Fail()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/appointments/cancel/1235");
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(400, code);
        }

        [Fact]
        public async Task CancelAppointment_AlreadyCanceled_Fail()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/appointments/cancel/2");
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(400, code);
        }

        [Fact]
        public async Task CancelAppointment_AlreadyPassed_Fail()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/appointments/cancel/3");
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(400, code);
        }

        [Fact]
        public async Task CancelAppointment_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/appointments/cancel/1");
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(200, code);
        }

        [Fact]
        public async Task SearchAppointments_Success()
        {
            // Arange
            var dto = new SearchAppointmentsDTO()
            {
                DoctorId = 2,
                From = new DateTime(2021, 06, 10),
                To = new DateTime(2021, 09, 10),
                UserId = 6,
                TypeOfAppointment = 0
            };
            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");


            // Act
            var response = await client.PostAsync("http://localhost:5001/api/appointments/search/", content);
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<List<AppointmentDTO>>(body);

            // Assert
            Assert.NotEmpty(jsonBody);

        }

        [Fact]
        public async Task SearchAppointments_Fail()
        {
            // Arange
            var dto = new SearchAppointmentsDTO()
            {
                DoctorId = 2,
                From = new DateTime(2021, 10, 10),
                To = new DateTime(2021, 11, 10),
                UserId = 6,
                TypeOfAppointment = 0
            };
            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");


            // Act
            var response = await client.PostAsync("http://localhost:5001/api/appointments/search/", content);
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<List<AppointmentDTO>>(body);

            // Assert
            Assert.Empty(jsonBody);

        }

        [Fact]
        public async Task ScheduleAppointment_Success()
        {
            // Arange
            var dto = new AppointmentDTO()
            {
                DoctorId = 2,
                StartTime = new DateTime(2021, 07, 10,18,0,0),
                EndTime = new DateTime(2021, 07, 10,18,15,0),
                PatientId = 6,
                TypeOfAppointment = 0,
                ReferralId = -1
            };
            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");


            // Act
            var response = await client.PostAsync("http://localhost:5001/api/appointments/schedule/", content);
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(200, code);

        }

        [Fact]
        public async Task ScheduleAppointment_Fail()
        {
            // Arange
            var dto = new AppointmentDTO()
            {
                StartTime = new DateTime(2021, 5, 11, 14, 00, 0),
                EndTime = new DateTime(2021, 5, 11, 14, 15, 0),
                TypeOfAppointment = 0,
                DoctorId = 1,
                PatientId = 7
            };
            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");


            // Act
            var response = await client.PostAsync("http://localhost:5001/api/appointments/schedule/", content);
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(400, code);

        }


    }

}
