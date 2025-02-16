using api_vet_app.Models.Pet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_vet_app.Models.Persona
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal Celular { get; set; }

        [ForeignKey(nameof(VetId))]
        [Required]
        public string? VetId { get; set; }

        public List<ClientPet>? Pets { get; set; }
    }
}
