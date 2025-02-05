using api_vet_app.Data;
using api_vet_app.Models.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api_vet_app.Endpoints
{
    public static class MapClientEndpoints
    {
        public static WebApplication Client(this WebApplication app)
        {
            // Criar um Novo Cliente vinculado ao vet autenticado
            app.MapPost("/client", [Authorize] async ([FromBody] Client request, 
                AppDbContext dbContext, 
                ClaimsPrincipal user ) => {
                    // Obtém o Id do vet
                    var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (vetId == null)
                        return Results.Unauthorized();

                    var client = new Client
                    {
                        Name = request.Name.ToUpper(),
                        Celular = request.Celular,
                        VetId = vetId,
                    };

                    dbContext.Clients.Add(client);
                    await dbContext.SaveChangesAsync();

                    return Results.Created($"/client/{client.Id}", client);
                }).RequireAuthorization()
                .WithTags("Client");

            // Listar todos os cliente com o vet autenticado
            app.MapGet("/clients", [Authorize] async (AppDbContext dbContext, ClaimsPrincipal user) => {

                // obtem id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // recebe a lista de cliente do bd
                var clients = await dbContext.Clients.Where(c => c.VetId.Equals(vetId)).ToListAsync();

                // retorna a lista em json
                return Results.Ok(clients);
            }).RequireAuthorization()
                .WithTags("Client");


            // Deletar cliente com id com vet autenticado
            app.MapDelete("/client/{id}", [Authorize] async ([FromRoute] int id, 
                AppDbContext dbContext,
                ClaimsPrincipal user) => 
            {
                // obtem o id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // busca no bd o cliente por id e por id do vet
                var cliente = await dbContext.Clients.Where(c => c.Id.Equals(id) && c.VetId.Equals(vetId)).FirstOrDefaultAsync();
                if (cliente == null)
                    return Results.BadRequest("Não Econtrado");

                //exclui dados
                dbContext.Clients.Remove(cliente);
                dbContext.SaveChanges();

                return Results.NoContent();
            }).RequireAuthorization()
            .WithTags("Client");
            return app;
        }
    }
}
