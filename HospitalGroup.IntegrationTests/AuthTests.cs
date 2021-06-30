
using Hospital.Domain.DTO;
using HospitalGroup.Center;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalGroup.IntegrationTests
{
    public class AuthTests
    {
        private readonly HttpClient client;

        public AuthTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            client = appFactory.CreateClient();
        }

        [Fact]
        public async Task Login_Success()
        {
            // Arange
            var dto = new LoginDTO()
            {
                UserName = "pacijent",
                Password = "pacijent"
            };
            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");


            // Act
            var response = await client.PostAsync("http://localhost:5001/api/auth/login", content);
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(200, code);

        }

        [Fact]
        public async Task Login_Fail()
        {
            // Arange
            var dto = new LoginDTO()
            {
                UserName = "userthatdoesntexist",
                Password = "userthatdoesntexist"
            };
            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("http://localhost:5001/api/auth/login", content);
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(400, code);
        }



    }

}
