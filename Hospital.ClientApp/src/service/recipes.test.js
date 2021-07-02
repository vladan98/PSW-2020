import axios from "axios"
import { RecipesService } from "./recipes"
jest.mock("axios")

describe("RecipesService", () => {
    const user = {
        id: 4,
        token: "adoiaufnonjsivfnzsmviksszm"
    }
    const headers = {
        Authorization: `Bearer ${user.token}`
    }

    it('should test constructor', () => {

        const create = jest.fn(() => null)
        axios.create = create

        const recipesService = new RecipesService(user);

        expect(recipesService.user).toBe(user);
        expect(recipesService.httpClient).toBe(null);
        expect(create).toHaveBeenCalled();
    })

    it('should test generateAuthHeaders func', () => {

        const recipesService = new RecipesService(user);

        expect(recipesService.generateAuthHeaders()).toStrictEqual(headers);
    })

    it('should test assignRecipe func', () => {

        const post = jest.fn()
        axios.create = jest.fn(() => ({ post }))

        const recipesService = new RecipesService(user);
        recipesService.assignRecipe(3, 6)

        expect(post).toHaveBeenCalled();
    })

    it('should test getAllRecipes func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const recipesService = new RecipesService(user);
        recipesService.getAllRecipes()

        expect(get).toHaveBeenCalled();
    })


})