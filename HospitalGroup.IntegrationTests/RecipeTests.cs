using Hospital.Center;
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
    public class RecipeTests
    {
        private readonly HttpClient client;

        public RecipeTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAllRecipes_Success()
        {
            // Arange
            // Act
            var response = await client.GetAsync("http://localhost:5001/api/recipes/get");
            var body = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<RecipesResponse>(body);

            // Assert
            Assert.NotEmpty(jsonBody.Recipes);
        }

        [Fact]
        public async Task AsignRecipe_Success()
        {
            // Arange
            var dto = new AsignRecipeDTO()
            {
                RecipeId = 1,
                PatientId = 6
            };
            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("http://localhost:5001/api/recipes/assign", content);
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(200, code);
        }
        [Fact]
        public async Task AsignRecipe_Fail()
        {
            // Arange
            var dto = new AsignRecipeDTO()
            {
                RecipeId = 1,
                PatientId = 12937816
            };
            string serializedDto = JsonConvert.SerializeObject(dto);

            var content = new StringContent(serializedDto, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("http://localhost:5001/api/recipes/assign", content);
            int code = (int)response.StatusCode;

            // Assert
            Assert.Equal(400, code);
        }
    }
}
