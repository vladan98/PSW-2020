import { recipeController } from './init';

const PROTO_PATH = __dirname + '/recipe.proto';

const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');

const packageDefinition = protoLoader.loadSync(
  PROTO_PATH,
  {}
);
const recipe_proto = grpc.
  loadPackageDefinition(packageDefinition).recipePackage;

const main = () => {

  var server = new grpc.Server();
  var recipeService = recipe_proto.recipeService
  server.addService(recipeService.service, {
    asignRecipe: recipeController.AsignRecipe,
    getAll: recipeController.GetAll
  });

  server.bindAsync('0.0.0.0:50051', grpc.ServerCredentials.createInsecure(), () => {
    console.log('server listening on: 0.0.0.0:50051');
    server.start();
  });
}

main();