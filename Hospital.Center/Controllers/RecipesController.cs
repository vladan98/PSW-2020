using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Center.Services.Abstract;
using Hospital.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Center.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService recipeService;
        public RecipesController(IRecipeService recService)
        {
            recipeService = recService;
        }

        [HttpGet("get")]
        public IActionResult GetAll()
        {
            return Ok(recipeService.GetAll());
        }

        [HttpPost("assign")]
        public IActionResult AsignRecipe([FromBody] RecipePatientDTO dto)
        {
            var asignDTO = new AsignRecipeDTO()
            {
                PatientId = dto.PatientId,
                RecipeId = dto.RecipeId
            };
            var done = recipeService.AsignRecipe(asignDTO);
            if (done)
                return Ok("Recipe assigned");
            return BadRequest();
        }
    }
}
