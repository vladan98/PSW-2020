using Grpc.Net.Client;
using Hospital.Center.GRPC.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Hospital.Center.recipeService;

namespace Hospital.Center
{
    public class GRPCClient:IGRPCClient
    {
        private recipeServiceClient recipeService;
        public GRPCClient()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress("http://localhost:50051");
            recipeService = new recipeService.recipeServiceClient(channel);
        }

        public RecipesResponse GetAllRecipes()
        {
            var response = recipeService.GetAll(new Empty());
            return response;
        }
        public bool AsignRecipe(AsignRecipeDTO recipeDTO)
        {
            var response = recipeService.AsignRecipe(recipeDTO);

            if (response == null)
                return false;
            return true;

        }
    }
}
