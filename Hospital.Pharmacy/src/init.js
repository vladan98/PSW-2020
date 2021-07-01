import { RecipeController } from './controller/RecipeController';
import { RecipeService } from './infrastructure/Services/RecipeService';
import { RecipeRepository } from './infrastructure/Repository/RecipeRepository';

const recipeRepository = new RecipeRepository();
export const recipeService = new RecipeService(recipeRepository);

export const recipeController = new RecipeController();

