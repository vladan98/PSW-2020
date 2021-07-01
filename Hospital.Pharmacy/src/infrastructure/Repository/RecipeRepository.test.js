import syncsql from "sync-sql"
import { RecipeRepository } from "./RecipeRepository"
jest.mock("sync-sql")

describe("RecipeRepository", () => {

    it('should test AsignRecipe Success', () => {

        const mysql = jest.fn(() => ({ success: true }))
        syncsql.mysql = mysql

        const recipeRepository = new RecipeRepository();

        const response = recipeRepository.AsignRecipe(3, 6)

        expect(mysql).toHaveBeenCalled();
        expect(response).toStrictEqual({
            recipeId: 3,
            patientId: 6
        });
    })

    it('should test AsignRecipe Fail', () => {

        const mysql = jest.fn(() => ({ success: false }))
        syncsql.mysql = mysql

        const recipeRepository = new RecipeRepository();

        const response = recipeRepository.AsignRecipe(3, 6)

        expect(mysql).toHaveBeenCalled();
        expect(response).toBe(null);
    })

    it('should test GetAll Success', () => {

        const mysql = jest.fn(() => ({
            data: {
                rows: [{
                    Id: 3,
                    Medication: 5
                }]
            }
        }))
        syncsql.mysql = mysql

        const recipeRepository = new RecipeRepository();

        const response = recipeRepository.GetAll()

        expect(mysql).toHaveBeenCalled();
        expect(response).toStrictEqual({
            recipes: [{
                id: 3,
                medication: 5
            }]
        });
    })

    it('should test GetAll Fail', () => {

        const mysql = jest.fn(() => ({
            data: {
                rows: []
            }
        }))
        syncsql.mysql = mysql

        const recipeRepository = new RecipeRepository();

        const response = recipeRepository.GetAll()

        expect(mysql).toHaveBeenCalled();
        expect(response).toStrictEqual({
            recipes: []
        });
    })

})
