using System.ComponentModel.DataAnnotations;

namespace api_vet_app.ModelView
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Senha é obrigatória.")]
        public string Password { get; set; } = default!;
    }
}
