using api_vet_app.Data;
using api_vet_app.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api_vet_app.Endpoints.ServiceEndpoints
{
    public static class MapServiceItemEndpoints
    {
        public static WebApplication ServiceItem(this WebApplication app)
        {
            app.MapPost("/serviceitem", [Authorize] async ([FromBody] ServiceItem request, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // pegando id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                var serviceItem = new ServiceItem
                {
                    Name = request.Name.ToUpper(),
                    Quantity = request.Quantity,
                    Price = request.Price,
                    Amount = request.Amount,
                    IdJob = request.IdJob,
                    IdAttending = request.IdAttending,
                    VetId = vetId
                };

                //vinculando o attending
                serviceItem.Attending = await dbContext.Attendings.FindAsync(request.IdAttending);

                // vinculando o vet 
                serviceItem.User = await dbContext.Users.FindAsync(vetId);

                //vinculando o job
                serviceItem.Job = await dbContext.Job.FindAsync(request.IdJob);

                // adicionando o serviceItem
                await dbContext.ServiceItems.AddAsync(serviceItem);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/productitem/{serviceItem.Id}", serviceItem);
            }).RequireAuthorization()
            .WithTags("Service");

            app.MapGet("/productitem", [Authorize] async (AppDbContext dbContext, ClaimsPrincipal user) => 
            {
                // pegando id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // pegando todos os produtos
                var serviceItems = await dbContext.ServiceItems.Where(p => p.User.Id == vetId).ToListAsync();

                if (serviceItems.Count == 0)
                    return Results.NotFound("Nenhum produto encontrado!");

                return Results.Json(serviceItems);
            });

            return app;
        }
    }
}
