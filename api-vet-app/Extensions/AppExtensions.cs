namespace api_vet_app.Extensions
{
    public static class AppExtensions
    {
        public static WebApplication UseArchitectures(this WebApplication app)
        {
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
