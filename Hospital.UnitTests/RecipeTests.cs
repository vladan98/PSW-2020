using Google.Protobuf.Collections;
using Hospital.Center;
using Hospital.Center.GRPC.Abstract;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services;
using Hospital.Center.Services.Abstract;
using Hospital.Domain.Enums;
using Hospital.Domain.Models.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HospitalGroup.UnitTests
{
    public class RecipeTests
    {


        [Fact]
        public void AsignRecipe_Fail()
        {
            // Arange
            var service = CreateRecipeService();
            var dto = new AsignRecipeDTO() { PatientId = 999, RecipeId = 3 };

            // Act
            var response = service.AsignRecipe(dto);

            // Assert
            Assert.False(response);
        }


        [Fact]
        public void AsignRecipe_Success()
        {
            // Arange
            var service = CreateRecipeService();
            var dto = new AsignRecipeDTO() { PatientId = 6, RecipeId = 3 };

            // Act
            var response = service.AsignRecipe(dto);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public void GetAllRecipes_Success()
        {
            // Arange
            var service = CreateRecipeService();

            // Act
            var response = service.GetAll();

            // Assert
            Assert.NotNull(response);
        }

        public static IGRPCClient CreateGRPCClientMock()
        {
            var grpcClientMock = new Mock<IGRPCClient>();

            var recipes = CreateRecipesResponse();

            grpcClientMock.Setup(x => x.GetAllRecipes()).Returns(recipes);

            grpcClientMock.Setup(x => x.AsignRecipe(It.IsAny<AsignRecipeDTO>())).Returns(true);

            return grpcClientMock.Object;
        }

        public static IPatientRepository CreatePatientRepositoryMock()
        {
            var patientRepositoryMock = new Mock<IPatientRepository>();

            var patients = CreatePatients();

            patientRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                (int id) => patients.Where(p => p.Id == id).FirstOrDefault());

            return patientRepositoryMock.Object;
        }

        public static IRecipeService CreateRecipeService()
            => new RecipeService(CreatePatientRepositoryMock(), CreateGRPCClientMock());


        #region data

        public static RecipesResponse CreateRecipesResponse()
        {
            var response = new RecipesResponse();

            response.Recipes.Add(new Recipe()
            {
                Id = 1,
                Medication = "Andol"
            });
            response.Recipes.Add(new Recipe()
            {
                Id = 2,
                Medication = "Aspirin"
            });

            return response;
        }
        public static RecipesResponse CreateEmptyRecipesResponse()
        {
            return new RecipesResponse();
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
