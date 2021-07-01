import { config } from '../../dbconfig'
import snycsql from "sync-sql"

export class RecipeRepository {

    AsignRecipe(recipeId, patientId) {

        let sql = `insert into recipe_record(PatientId, recipeId) values (${patientId}, ${recipeId})`;
        var output = snycsql.mysql(config, sql);

        if (!output.success)
            return null

        return {
            recipeId: recipeId,
            patientId: patientId
        }
    }

    GetAll() {
        let sql = "select * from recipe";
        var output = snycsql.mysql(config, sql);
        const res = [];
        output.data.rows.forEach(r => res.push({ id: r.Id, medication: r.Medication }));
        const recipes = {
            recipes: res
        }
        return recipes;
    }
}
