using api_vet_app.Models.Persona;
using api_vet_app.Models.Pet;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_vet_app.Models.Service
{
    public class Attending
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // boolano para saber se foi vacinado ou não
        [Required]
        public bool IsVaccina { get; set; }
        // data de criação
        [Required]
        public DateTime? CreatedAt { get; set; }
        // Desconto se houver
        [Required]
        public decimal Discount { get; set; }
        // Total do Atendimento
        [Required]
        public decimal Amount { get; set; }

        // Foreign Key's
        [ForeignKey(nameof(IdClient))]
        [Required]
        public int IdClient { get; set; }
        public Client? Client { get; set; }
        [ForeignKey(nameof(IdClientPet))]
        [Required]
        public int IdClientPet { get; set; }
        public ClientPet? ClientPet { get; set; }
        [ForeignKey(nameof(IdPayment))]
        [Required]
        public int IdPayment { get; set; }
        public Payment? Payment { get; set; }
        [ForeignKey(nameof(VetId))]
        [Required]
        public string? VetId { get; set; }
        public User? User { get; set; }
    }
}
