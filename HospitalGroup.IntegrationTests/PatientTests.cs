using Hospital.Domain.DTO;
using HospitalGroup.Center;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalGroup.IntegrationTests
{
    public class PatientTests
    {
        private readonly HttpClient client;

        public PatientTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            client = appFactory.CreateClient();
        }

        [Fact]
        public async Task Register_Success()
        {
            // Arange
            var dto = new RegisterPatientDTO()
            {
                UserName = "newuser",
                Password = "newuser",
                ChosenDoctorId = 3,
                FirstName = "New",
                LastName = "User",
                Gender = 0
            };

            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("http://localhost:5001/api/patient/register", content);
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(200, code);
        }

        [Fact]
        public async Task Register_Fail()
        {
            // Arange
            var dto = new RegisterPatientDTO()
            {
                UserName = "pacijent",
                Password = "pacijent",
                ChosenDoctorId = 3,
                FirstName = "New",
                LastName = "User",
                Gender = 0
            };

            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("http://localhost:5001/api/patient/register", content);
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(400, code);
        }

        [Fact]
        public async Task GetAllPatients_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/patient/all");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<List<PatientDTO>>(body);

            // Assert
            Assert.NotEmpty(jsonBody);
        }


        [Fact]
        public async Task BlockPatient_Fail()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/patient/block/8");
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(400, code);
        }

        [Fact]
        public async Task BlockPatient_Sucess()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/patient/block/7");
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(200, code);
        }
        
        [Fact]
        public async Task GetMalicious_Sucess()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/patient/malicious");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<List<PatientDTO>>(body);

            // Assert
            Assert.NotEmpty(jsonBody);
        }


    }
}
