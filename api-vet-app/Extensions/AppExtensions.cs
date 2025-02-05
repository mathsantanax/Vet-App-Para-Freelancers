namespace api_vet_app.Extensions
{
    public static class AppExtensions
    {
        public static WebApplication UseArchitectures(this WebApplication app)
        {
            app.UseAuthentication(); // Habilita autenticação JWT
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            return app;
        }
    }
}
