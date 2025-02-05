using api_vet_app.Models.Persona;

namespace api_vet_app.Interface
{
    public interface IPersona
    {
        User Login (string username, string password);
        User Register(User user);
    }
}
