using Hospital.Center.GRPC.Abstract;
using Hospital.Center.Repository.Abstract;
using Hospital.Center.Services.Abstract;

namespace Hospital.Center.Services
{
    public class RecipeService: IRecipeService
    {
        private IGRPCClient gRPCClient;
        private IPatientRepository patentRepository;

        public RecipeService(IPatientRepository patRepository,IGRPCClient grpcClient)
        {
            gRPCClient = grpcClient;
            patentRepository = patRepository;
        }

        public RecipesResponse GetAll()
        {
            return gRPCClient.GetAllRecipes();
        }
        public bool AsignRecipe(AsignRecipeDTO asignRecipeDTO)
        {
            var patient = patentRepository.GetById(asignRecipeDTO.PatientId);
            if (patient == null)
                return false;
            var done = gRPCClient.AsignRecipe(asignRecipeDTO);
            if(done)
                return true;
            return false;
        }

    }
}
