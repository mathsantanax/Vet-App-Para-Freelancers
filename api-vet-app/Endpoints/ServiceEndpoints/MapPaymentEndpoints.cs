using api_vet_app.Data;
using api_vet_app.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace api_vet_app.Endpoints.ServiceEndpoints
{
    public static class MapPaymentEndpoints
    {
        public static WebApplication Payment(this WebApplication app)
        {
            app.MapGet("/payment", [Authorize] async (AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // verifica usuario logado
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // pegando todos os pagamentos
                var payments = await dbContext.Payment.ToListAsync();

                //testando para saber se veio vazio
                if (payments == null)
                    return Results.NotFound();

                return Results.Ok(payments);
            }).RequireAuthorization()
            .WithTags("Service");

            app.MapGet("/payment/{id}", [Authorize] async (int id, AppDbContext dbContext, ClaimsPrincipal user) =>
            {
                // verifica usuario logado
                var vetId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vetId == null)
                    return Results.Unauthorized();

                // pegado dado no banco por id
                var payment = await dbContext.Payment.Where(p => p.Id.Equals(id)).FirstAsync();

                // verifica se veio vazio
                if(payment == null)
                    return Results.NotFound();

                return Results.Ok(payment);
            }).RequireAuthorization()
            .WithTags("Service");

            return app;
        }
    }
}
