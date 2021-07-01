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
    public class FeedbackTests
    {
        private readonly HttpClient client;

        public FeedbackTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetPublishedFeedback_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/feedback/get-published");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<List<Feedback>>(body);

            // Assert
            Assert.NotEmpty(jsonBody);
        }
        

        [Fact]
        public async Task GetAllFeedback_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/feedback/get-all");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<List<Feedback>>(body);

            // Assert
            Assert.NotEmpty(jsonBody);
        }
        
        [Fact]
        public async Task UdatePublished_Fail()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/feedback/update-published/78");
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(400, code);
        }

        [Fact]
        public async Task UdatePublished_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/feedback/update-published/1");
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(200, code);
        }

        [Fact]
        public async Task LeaveFeedback_Success()
        {
            // Arange
            var dto = new LeaveFeedbackDTO()
            {
                Title = "Test ",
                Content = "Test Feedback",
                PatientId = 6
            };
            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("http://localhost:5001/api/feedback/post", content);
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(200, code);
        }

    }
}
