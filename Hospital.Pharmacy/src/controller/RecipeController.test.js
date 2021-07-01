import { RecipeController } from "./RecipeController"
import { recipeService } from "../init"
jest.mock("../init")

describe("RecipeController", () => {

    it('should test AsignRecipe', () => {

        const AsignRecipe = jest.fn(() => ({ success: true }))
        recipeService.AsignRecipe = AsignRecipe

        const recipeController = new RecipeController();

        const call = {
            request: {
                recipeId: 1,
                patentId: 6
            }
        }
        const callback = jest.fn()

        recipeController.AsignRecipe(call, callback)

        expect(AsignRecipe).toHaveBeenCalled();
        expect(callback).toHaveBeenCalledWith(null, { success: true });
    })

    it('should test GetAll', () => {

        const GetAll = jest.fn(() => ([{ id: 1, medication: 6 }]))
        recipeService.GetAll = GetAll

        const recipeController = new RecipeController();

        const call = {
            request: {
                recipeId: 1,
                patentId: 6
            }
        }
        const callback = jest.fn()
        recipeController.GetAll(call, callback)

        expect(GetAll).toHaveBeenCalled();
        expect(callback).toHaveBeenCalledWith(null, [{ id: 1, medication: 6 }]);
    })


})
