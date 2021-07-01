import { recipeService } from "../init"

export class RecipeController {

    AsignRecipe(call, callback) {
        const asigned = recipeService.AsignRecipe(call.request.recipeId, call.request.patientId);
        callback(null, asigned);
    }

    GetAll(call, callback) {
        const all = recipeService.GetAll();
        callback(null, all);
    }
}