
using api_vet_app.Models.Persona;
using api_vet_app.Models.Pet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_vet_app.Models.Service
{
    public class Vaccination
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // Data da próxima vacina para avisa no app
        [Required]
        public DateTime? NextVaccination { get; set; }

        // Foreign Key's
        [ForeignKey(nameof(IdClientPet)), Required]
        public int IdClientPet { get; set; }
        public ClientPet? ClientPet { get; set; }
        [ForeignKey(nameof(IdClient)), Required]
        public int IdClient {  get; set; }
        public Client? Client { get; set; }
        [ForeignKey(nameof(IdAttending)), Required]
        public int IdAttending { get; set; }
        public Attending? Attending { get; set; }
        [ForeignKey(nameof(VetId)), Required]
        public string? VetId { get; set; }
        public User? User { get; set; }
    }
}
