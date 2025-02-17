using api_vet_app.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using api_vet_app.Models.Service;

namespace api_vet_app.Endpoints.ServiceEndpoints
{
    public static class MapJobEndpoints
    {
        public static WebApplication Job(this WebApplication app)
        {

            app.MapGet("/job", [Authorize] async (AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                try
                {
                    // Obtém o Id do vet
                    var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (vetId == null)
                        return Results.Unauthorized();

                    // recupera todos os trabalhos
                    var jobs = await dbContext.Job.Where(j => j.User.Id.Equals(vetId)).ToListAsync();

                    if (jobs.Count == 0)
                        return Results.NoContent();

                    return Results.Json(jobs);
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

            app.MapGet("/job/{id}", [Authorize] async (int id, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                try
                {
                    // Obtém o Id do vet
                    var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (vetId == null)
                        return Results.Unauthorized();

                    // recupera trabalho por id
                    var job = await dbContext.Job.Where(j => j.Id == id && j.User.Id.Equals(vetId)).FirstOrDefaultAsync();

                    // verifica se está vazio
                    if (job == null)
                        return Results.NoContent();

                    return Results.Json(job);
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

            app.MapPost("/job", [Authorize] async (Job request, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // Obtém o Id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // recupera vet 
                var vet = await dbContext.Users.Where(j => j.Id.Equals(vetId)).FirstOrDefaultAsync();

                var job = new Job
                {
                    Name = request.Name.ToUpper(),
                    Price = request.Price,
                    VetId = vetId,
                    User = vet
                };

                // salva os dados
                dbContext.Job.Add(job);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/job/{job.Id}", job);

            }).RequireAuthorization()
            .WithTags("Service");


            return app;
        }
    }
}
