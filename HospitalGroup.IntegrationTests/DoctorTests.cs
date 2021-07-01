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
    public class DoctorTests
    {
        private readonly HttpClient client;

        public DoctorTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetGeneral_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/doctors/get");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<List<DoctorDTO>>(body);

            // Assert
            Assert.NotEmpty(jsonBody);
        }

        [Fact]
        public async Task GetAll_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/doctors/getAll");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<List<DoctorDTO>>(body);

            // Assert
            Assert.NotEmpty(jsonBody);
        }


    }
}
