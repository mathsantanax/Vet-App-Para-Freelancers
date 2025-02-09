using api_vet_app.Data;
using api_vet_app.Models.Pet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api_vet_app.Endpoints.PetEndpoints
{
    public static class MapEspecieEndpoints
    {
        public static WebApplication Especie(this WebApplication app)
        {
            app.MapGet("/especies", [Authorize] async (AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // verifica usuario logado
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // pega todas as especies no banco
                var especies = await dbContext.Especies.ToListAsync();

                // verifica se tem alguma cadastrada
                if (especies == null)
                    return Results.Empty;

                return Results.Ok(especies);
            }).RequireAuthorization()
            .WithTags("Especies");

            app.MapGet("/especie/{id}", [Authorize] async (int id, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // verifica usuario logado
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // pega especie ref id
                var especie = await dbContext.Especies.Where(e => e.Id.Equals(id)).ToListAsync();

                if (especie == null)
                    return Results.NotFound();

                return Results.Ok(especie);
            }).RequireAuthorization()
            .WithTags("Especies");

            app.MapPost("/especie", [Authorize] async ([FromBody] Especie request, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // verifica usuario logado
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                var especie = new Especie
                {
                    Name = request.Name.ToUpper()
                };

                // salva no banco de dados
                dbContext.Especies.Add(especie);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/especie/{especie.Id}", especie);
            }).RequireAuthorization()
            .WithTags("Especies");


            return app;
        }
    }
}
