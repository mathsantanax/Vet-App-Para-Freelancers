using api_vet_app.Endpoints.PersonaEndpoints;
using api_vet_app.Endpoints.PetEndpoints;
using api_vet_app.Endpoints.ServiceEndpoints;

namespace api_vet_app.Endpoints
{
    public static class EndpointsExtensions
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {

            app.Home()
            .User()
            .Client()
            .Pet()
            .Especie()
            .Raca()
            .Payment()
            .Lab()
            .Job();
            

            return app;
        }
    }
}
