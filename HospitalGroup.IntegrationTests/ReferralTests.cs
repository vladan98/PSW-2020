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
    public class ReferralTests
    {
        private readonly HttpClient client;

        public ReferralTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetPatientReferrals_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/referral/patient/6");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<List<ReferralDTO>>(body);

            // Assert
            Assert.NotEmpty(jsonBody);
        }

        [Fact]
        public async Task GetPatientReferrals_Fail()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/referral/patient/1236");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<List<ReferralDTO>>(body);

            // Assert
            Assert.Empty(jsonBody);
        }

        [Fact]
        public async Task UpdateReferral_Fail()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/referral/update/7786");
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(400, code);
        }

        [Fact]
        public async Task UpdateReferral_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/referral/update/2");
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(200, code);
        }

        [Fact]
        public async Task AddReferral_Success()
        {
            // Arange
            var dto = new ReferralCreateDTO()
            {
                DoctorId = 2,
                Specialization = 3,
                PatientId = 6,
            };
            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");


            // Act
            var response = await client.PostAsync("http://localhost:5001/api/referral/add", content);
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(200, code);
        }
    }
}
