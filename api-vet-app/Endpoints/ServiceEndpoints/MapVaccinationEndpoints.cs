using api_vet_app.Data;
using api_vet_app.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api_vet_app.Endpoints.ServiceEndpoints
{
    public static class MapVaccinationEndpoints
    {
        public static WebApplication Vaccination(this WebApplication app)
        {
            app.MapPost("/vaccination", [Authorize] async ([FromBody] Vaccination request, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // Verifica vet
                var vet = await dbContext.Users.FindAsync(user.FindFirstValue(ClaimTypes.NameIdentifier));
                if (vet == null)
                    return Results.Unauthorized();

                var vaccination = new Vaccination
                {
                    NextVaccination = request.NextVaccination,
                    IdClientPet = request.IdClientPet,
                    IdClient = request.IdClient,
                    IdAttending = request.IdAttending,
                    VetId = vet.Id,
                    User = vet
                };

                // vinculando cliente
                vaccination.Client = await dbContext.Clients.FindAsync(vaccination.IdClient);
                // vinculando o pet
                vaccination.ClientPet = await dbContext.ClientsPet.FindAsync(vaccination.IdClientPet);
                // vinculando o atendimento
                vaccination.Attending = await dbContext.Attendings.FindAsync(vaccination.IdAttending);

                // adicionando a vacina
                await dbContext.Vaccination.AddAsync(vaccination);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/vaccination/{vaccination.Id}", vaccination);
            });

            app.MapGet("/attendings", [Authorize] async ())

            return app;
        }
    }
}
