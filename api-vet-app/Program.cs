using api_vet_app.Endpoints;
using api_vet_app.Extensions;

var builder = WebApplication.CreateBuilder(args);
    builder.AddArchitectures();


var app = builder.Build();

app.UseArchitectures()
    .MapEndpoints();

app.Run();