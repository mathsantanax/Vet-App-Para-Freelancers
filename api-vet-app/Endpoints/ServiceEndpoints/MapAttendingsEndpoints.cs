using api_vet_app.Data;
using api_vet_app.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api_vet_app.Endpoints.ServiceEndpoints
{
    public static class MapAttendingsEndpoints
    {
        public static WebApplication Attendings(this WebApplication app)
        {
            app.MapPost("/attending", [Authorize] async ([FromBody] Attending request, AppDbContext dbContext, ClaimsPrincipal user ) =>
            {
                // Verifica vet
                var vet = await dbContext.Users.FindAsync(user.FindFirstValue(ClaimTypes.NameIdentifier));
                if (vet == null)
                    return Results.Unauthorized();

                var attending = new Attending
                {
                    IsVaccina = request.IsVaccina,
                    Amount = request.Amount,
                    CreatedAt = DateTime.Now,
                    Discount = request.Discount,
                    IdPayment = request.IdPayment,
                    IdClient = request.IdClient,
                    IdClientPet = request.IdClientPet,
                    VetId = vet.Id,
                    User = vet
                };

                // vinculando cliente 
                attending.Client = await dbContext.Clients.FindAsync(attending.IdClient);
                // vinculando o pet
                attending.ClientPet = await dbContext.ClientsPet.FindAsync(attending.IdClientPet);
                // vinculando o pagamento
                attending.Payment = await dbContext.Payment.FindAsync(attending.IdPayment);

                // adicionando o atendimento
                await dbContext.Attendings.AddAsync(attending);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/attending/{attending.Id}", attending);
            }).RequireAuthorization()
            .WithTags("Service");

            app.MapGet("/attending", [Authorize] async (AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // Verifica vet
                var vet = await dbContext.Users.FindAsync(user.FindFirstValue(ClaimTypes.NameIdentifier));
                if (vet == null)
                    return Results.Unauthorized();
                // pegando todos os atendimentos
                var attendings = await dbContext.Attendings.Where(a => a.VetId == vet.Id).ToListAsync();
                if (attendings.Count == 0)
                    return Results.NotFound("Nenhum atendimento encontrado!");
                return Results.Ok(attendings);

            }).RequireAuthorization()
            .WithTags("Service");

            app.MapGet("/attending/{id}", [Authorize] async (int id, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // Verifica vet
                var vet = await dbContext.Users.FindAsync(user.FindFirstValue(ClaimTypes.NameIdentifier));
                if (vet == null)
                    return Results.Unauthorized();

                //pegando cliente
                var client = await dbContext.Clients.Where(c => c.Id == id && c.VetId == vet.Id).ToListAsync();

                // pegando o atendimento
                var attending = await dbContext.Attendings.Where(a => a.Client.Equals(client)).ToListAsync();

                if (attending.Count == 0)
                    return Results.NotFound("Atendimento não encontrado!");

                return Results.Json(attending);
            }).RequireAuthorization()
            .WithTags("Service");

            return app;
        }
    }
}
