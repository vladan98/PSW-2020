using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Center.GRPC.Abstract
{
    public interface IGRPCClient
    {
        RecipesResponse GetAllRecipes();
        bool AsignRecipe(AsignRecipeDTO recipeDTO);
    }
}
