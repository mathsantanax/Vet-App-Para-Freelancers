using System.ComponentModel.DataAnnotations;

namespace api_vet_app.ModelView
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string FullName { get; set; } = default!;
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de Email Inválido.")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; } = default!;
    }
}
