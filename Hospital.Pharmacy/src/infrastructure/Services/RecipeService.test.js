import { RecipeService } from "./RecipeService"

class RecipeRepositoryMock {
    AsignRecipe() {
        return {
            id: 1,
            medication: 6
        }
    }
    GetAll() {
        return [{
            id: 1,
            medication: 6
        }]
    }
}
describe("RecipeService", () => {

    it('should test AsignRecipe', () => {

        const recipeRepository = new RecipeRepositoryMock();
        const recipeService = new RecipeService(recipeRepository);

        const response = recipeService.AsignRecipe(3, 6)

        expect(response).toStrictEqual({ id: 1, medication: 6 });
    })

    it('should test GetAll', () => {

        const recipeRepository = new RecipeRepositoryMock();
        const recipeService = new RecipeService(recipeRepository);

        const response = recipeService.GetAll()

        expect(response).toStrictEqual([{
            id: 1,
            medication: 6
        }]);
    })


})
