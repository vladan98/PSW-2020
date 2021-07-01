namespace Hospital.Center.Services.Abstract
{
    public interface IRecipeService
    {
        RecipesResponse GetAll();
        bool AsignRecipe(AsignRecipeDTO asignRecipeDTO);
    }
}
