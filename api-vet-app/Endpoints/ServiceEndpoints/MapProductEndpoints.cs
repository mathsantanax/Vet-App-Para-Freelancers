using api_vet_app.Data;
using api_vet_app.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api_vet_app.Endpoints.ServiceEndpoints
{
    public static class MapProductEndpoints
    {
        public static WebApplication Product(this WebApplication app)
        {
            app.MapGet("/product", [Authorize] async (AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // pegando id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // pegando todos os produtos
                var products = await dbContext.Products.Where(p => p.User.Id == vetId).ToListAsync();

                if(products.Count == 0)
                    return Results.NotFound("Nenhum produto encontrado!");

                return Results.Json(products);

            }).RequireAuthorization()
            .WithTags("Service");

            app.MapGet("/product/{id}", [Authorize] async (int id, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // pegando id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // pegando produto por id
                var product = await dbContext.Products.Where(p => p.Id == id && p.User.Id == vetId).FirstAsync();
                if (product == null)
                    return Results.NotFound("Produto não encontrado!");

                return Results.Json(product);
            }).RequireAuthorization()
            .WithTags("Service");

            app.MapPost("/product", [Authorize] async ([FromBody] Product request, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // pegando id do vet
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                //recupando vet
                var vet = await dbContext.Users.Where(v => v.Id.Equals(vetId)).FirstOrDefaultAsync();

                var product = new Product
                {
                    Name = request.Name.ToUpper(),
                    Price = request.Price,
                    IdLab = request.IdLab,
                    VetId = vetId,
                    User = vet
                };

                //verificando se existe o laboratorio cadastrado
                var lab = await dbContext.Lab.Where(l => l.Id == request.IdLab && l.User.Id == vetId).FirstOrDefaultAsync();
                if (lab == null)
                    return Results.BadRequest("Laboratório não encontrado!");

                dbContext.Products.Add(product);
                await dbContext.SaveChangesAsync();

                return Results.Created($"/product/{product.Id}", product);

            });
            return app;
        }
        
    }
}