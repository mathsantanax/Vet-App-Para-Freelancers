using api_vet_app.Data;
using api_vet_app.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api_vet_app.Endpoints.ServiceEndpoints
{
    public static class MapProductItemEndpoints
    {
        public static WebApplication ProductItem(this WebApplication app)
        {
            app.MapGet("/productitem", [Authorize] async (AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // pegando id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // pegando todos os produtos
                var productItems = await dbContext.ProductItems.Where(p => p.User.Id == vetId).ToListAsync();
                if (productItems.Count == 0)
                    return Results.NotFound("Nenhum item de produto encontrado!");

                return Results.Json(productItems);
            }).RequireAuthorization()
            .WithTags("Service");

            app.MapPost("/productitem", [Authorize] async ([FromBody] ProductItem request, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // pegando id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                var productItem = new ProductItem
                {
                    Name = request.Name,
                    Quantity = request.Quantity,
                    Price = request.Price,
                    Amount = request.Amount,
                    IdProduct = request.IdProduct,
                    IdAttending = request.IdAttending,
                    VetId = vetId
                };

                // vinculando o usuario
                productItem.User = await dbContext.Users.FindAsync(vetId);

                // vinculando o produto
                productItem.Product = await dbContext.Products.FindAsync(request.IdProduct);

                // vinculando o atendimento
                productItem.Attending = await dbContext.Attendings.FindAsync(request.IdAttending);

                // adicionando produto
                await dbContext.ProductItems.AddAsync(productItem);
                await dbContext.SaveChangesAsync();
                return Results.Created($"/productitem/{productItem.Id}", productItem);
            }).RequireAuthorization()
            .WithTags("Service");

            return app;
        }
    }
}
