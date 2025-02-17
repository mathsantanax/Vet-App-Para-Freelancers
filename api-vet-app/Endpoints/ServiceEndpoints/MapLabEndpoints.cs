using api_vet_app.Data;
using api_vet_app.Models.Persona;
using api_vet_app.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api_vet_app.Endpoints.ServiceEndpoints
{
    public static class MapLabEndpoints
    {
        public static WebApplication Lab(this WebApplication app)
        {
            app.MapGet("/Lab",[Authorize] async (AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                try
                {
                    // Obtém o Id do vet
                    var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (vetId == null)
                        return Results.Unauthorized();

                    // Recebe todos os Laboratorios
                    var labs = await dbContext.Lab.Where(l => l.User.Id == vetId).ToListAsync();

                    // varifica se veio vazio
                    if (labs.Any() || labs.Count == 0)
                        return Results.NotFound("Não Encontrado!");

                    return Results.Json(labs);

                }
                catch (SqlException SqlException)
                {
                    return Results.BadRequest(SqlException.Message);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).RequireAuthorization()
            .WithTags("Service");

            app.MapGet("/lab/{id}", [Authorize] async (int id, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                try
                {
                    // Obtém o Id do vet
                    var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (vetId == null)
                        return Results.Unauthorized();

                    // recupera laboratorio por id
                    var lab = await dbContext.Lab.Where(l => l.Id == id && l.User.Id.Equals(vetId)).FirstAsync();

                    // verifica se esta vazio
                    if (lab == null)
                        return Results.NotFound("Não Econtrado!");

                    return Results.Json(lab);
                }
                catch (SqlException SqlException)
                {
                    return Results.BadRequest(SqlException.Message);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            }).RequireAuthorization()
            .WithTags("Service");

            app.MapPost("/lab", [Authorize] async (Lab request, AppDbContext dbContext, ClaimsPrincipal user) =>
            {

                try
                {
                    // Obtém o Id do vet
                    var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (vetId == null)
                        return Results.Unauthorized();

                    // obtem vet
                    var vet = await dbContext.Users.Where(u => u.Id.Equals(vetId)).FirstAsync();

                    var lab = new Lab
                    {
                        Name = request.Name.ToUpper(),
                        VetId = vetId,
                        User = vet
                    };

                    // salva no banco de dados
                    dbContext.Lab.Add(lab);
                    await dbContext.SaveChangesAsync();

                    return Results.Created($"/lab/{lab.Id}", lab);
                }
                catch (SqlException SqlException)
                {
                    return Results.BadRequest(SqlException.Message);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

            }).RequireAuthorization()
            .WithTags("Service");


            return app;
        }
    }
}
