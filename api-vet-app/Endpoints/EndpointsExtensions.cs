namespace api_vet_app.Endpoints
{
    public static class EndpointsExtensions
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {

            app.Weather()
            .User()
            .Client()
            .Especie()
            .Raca();

            return app;
        }
    }
}
