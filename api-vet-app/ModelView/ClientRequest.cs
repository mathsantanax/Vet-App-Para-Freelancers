using api_vet_app.Models.Persona;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_vet_app.Models.ModelView
{
    public class ClientRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Celular { get; set; }

        // relacionamento com o veterinario (user)
        public int VetId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}
