using api_vet_app.Models.Persona;
using api_vet_app.ModelView;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_vet_app.Endpoints.PersonaEndpoints
{
    public static class MapUserEndpoints
    {
        private static string GerarTokenJwt(User user, IConfiguration config)
        {
            //gerando token jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["Jwt:secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.FullName ??""),
                        new Claim(ClaimTypes.Email, user.Email ?? "")
                    }),
                Expires = DateTime.UtcNow.AddHours(2), // token valido por 2 horas
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static WebApplication User(this WebApplication app)
        {
            // Endpoint para registrar veterinario
            app.MapPost("/user/register", async ([FromBody] RegisterRequest request, UserManager<User> userManager) =>
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                    return Results.BadRequest("Email e senha são obrigatórios");

                var existingUser = await userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                    return Results.BadRequest("Email já cadastrado!");

                var user = new User
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FullName = request.FullName
                };

                var result = await userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                    return Results.BadRequest(result.Errors);

                return Results.Created($"/user/{user.Id}", new { user.Id, user.FullName, user.Email });
            }).WithTags("User")
            .AllowAnonymous();


            // Endpoint para login e geração do jwt
            app.MapPost("/user/login", async ([FromBody] LoginRequest request, UserManager<User> userManager, IConfiguration config) =>
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                    return Results.BadRequest("Email e senha são obrigatórios.");

                // busca pelo email
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return Results.BadRequest("Usuário ou senha inválidos");

                // verifica a senha
                var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
                if (!passwordValid)
                    return Results.BadRequest("Usuário ou senha inválidos.");


                string tokenString = GerarTokenJwt(user, config);

                user.PasswordHash = "";// limpa a senha
                request.Password = ""; //limpa a senha

                return Results.Ok(new { Token = tokenString });
            }).WithTags("User")
            .AllowAnonymous();

            return app;
        }
    }
}
