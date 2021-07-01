
export class RecipeService {
    recipeRepository;

    constructor(recipeRepository) {
        this.recipeRepository = recipeRepository;
    }

    AsignRecipe(recipeId, patientId) {
        return this.recipeRepository.AsignRecipe(recipeId, patientId);
    }

    GetAll() {
        const all = this.recipeRepository.GetAll();
        return all;
    }
}

