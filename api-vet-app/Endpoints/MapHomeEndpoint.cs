using api_vet_app.ModelView;

namespace api_vet_app.Endpoints
{
    public static class MapHomeEndpoint
    {
        public static WebApplication Home(this WebApplication app) 
        {
            app.MapGet("/", () => Results.Json(new Home())).AllowAnonymous().WithTags("Home");

            return app;
        }
    }
}
