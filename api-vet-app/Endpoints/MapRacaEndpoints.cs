using api_vet_app.Data;
using api_vet_app.Models.Pet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Security.Claims;

namespace api_vet_app.Endpoints
{
    public static class MapRacaEndpoints
    {
        public static WebApplication Raca(this  WebApplication app)
        {
            app.MapGet("/racas", [Authorize] async (AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // verifica usuario logado
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // pegando dados no banco de dados
                var racas = await dbContext.Racas.ToListAsync();

                if (racas == null)
                    return Results.Empty;

                return Results.Ok(racas);

            }).RequireAuthorization()
            .WithTags("Racas");

            app.MapGet("/raca/{id}", [Authorize] async (int id, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // verifica usuario logado
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();
                
                // pega a raca no BD
                var raca = await dbContext.Racas.Where(r => r.Id.Equals(id)).ToListAsync();

                // verifica se existe
                if (raca == null)
                    return Results.NotFound();

                return Results.Ok(raca);
            }).RequireAuthorization()
            .WithTags("Racas");

            app.MapPost("/raca", [Authorize] async (Raca request, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // verifica usuario logado
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                var raca = new Raca
                {
                    Name = request.Name.ToUpper()
                };

                // salva  no BD
                dbContext.Racas.Add(raca);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/raca/{raca.Id}", raca);

            }).RequireAuthorization()
            .WithTags("Racas");


            return app;
        }
    }
}
