using api_vet_app.Data;
using api_vet_app.Models.Persona;
using api_vet_app.Models.Pet;
using api_vet_app.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace api_vet_app.Endpoints.PetEndpoints
{
    public static class MapPetEndpoints
    {
        public static WebApplication Pet(this WebApplication app)
        {

            app.MapGet("/pet/{id}", [Authorize] async (int id, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // Obtém o Id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // busca cliente por id e por id do vet
                var cliente = await dbContext.Clients.Where(c => c.Id.Equals(id) && c.VetId.Equals(vetId)).FirstOrDefaultAsync();
                if (cliente == null)
                    return Results.BadRequest("Não Econtrado");

                // busca pet pelo id do cliente e id do vet
                var pet = await dbContext.ClientsPet.Where(p => p.IdClient.Equals(cliente.Id) && p.VetId.Equals(vetId)).ToListAsync();

                if (pet == null)
                    return Results.BadRequest("Não Encontrado.");

                return Results.Ok($"/pet/{pet.Count()}");
            }).RequireAuthorization()
            .WithTags("Pet");

            app.MapPost("/pet", [Authorize] async ([FromBody] ClientPetRequest request, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // Obtém o Id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                var pet = new ClientPet
                {
                    Name = request.Name.ToUpper(),
                    Microchip = request.Microchip,
                    Idade = request.Idade,
                    Peso = request.Peso,
                    Sexo = request.Sexo,
                    IdRaca = request.IdRaca,
                    IdEspecie = request.IdEspecie,
                    IdClient = request.IdClient,
                    VetId = vetId,
                };

                // verifica se o cliente existe
                if (!dbContext.Clients.Any(c => c.Id.Equals(pet.IdClient)))
                    return Results.BadRequest("Cliente não Encontrado!");
                // verifica se a especie existe
                if (!dbContext.Especies.Any(e => e.Id.Equals(pet.IdEspecie)))
                    return Results.BadRequest("Especie não Encontrada!");
                // verifica se a raça existe
                if (!dbContext.Racas.Any(r => r.Id.Equals(pet.IdRaca)))
                    return Results.BadRequest("Raca não Encontrada!");

                // cadastra no banco de dados
                dbContext.ClientsPet.Add(pet);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/pet/{pet.Id}", pet);
            }).RequireAuthorization()
            .WithTags("Pet");

            app.MapDelete("/pet", [Authorize] async ([FromBody] ClientPetRequest request, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // Obtém o Id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // verificando se o pet esta vinculado ao usuario
                if (!request.VetId.Equals(vetId))
                    return Results.Unauthorized();

                var clientPet = new ClientPet
                {
                    Id = request.Id,
                    Name = request.Name.ToUpper(),
                    Microchip = request.Microchip,
                    Idade = request.Idade,
                    Peso = request.Peso,
                    Sexo = request.Sexo,
                    IdRaca = request.IdRaca,
                    IdEspecie = request.IdEspecie,
                    IdClient = request.IdClient,
                    VetId = vetId,
                };

                // excluindo pet
                dbContext.ClientsPet.Remove(clientPet);
                await dbContext.SaveChangesAsync();

                return Results.NotFound();
            }).RequireAuthorization()
            .WithTags("Pet");

            return app;
        }
    }
}
