import axios from "axios";

export class RecipesService {
    user

    constructor(user) {
        this.user = user
        this.httpClient = axios.create({
            baseURL: 'https://localhost:5021',
        });
    }

    generateAuthHeaders() {
        return {
            Authorization: `Bearer ${this.user.token}`
        }
    }

    async assignRecipe(recipeId, patientId) {
        const response = await this.httpClient.post("/recipes/assign", { recipeId, patientId }, {
            headers: this.generateAuthHeaders()
        })
        return response?.data
    }

    async getAllRecipes() {
        const response = await this.httpClient.get("/recipes/get", {
            headers: this.generateAuthHeaders()
        })
        return response?.data
    }
}