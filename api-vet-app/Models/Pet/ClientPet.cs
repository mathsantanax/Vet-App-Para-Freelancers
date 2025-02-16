using api_vet_app.Models.Persona;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_vet_app.Models.Pet
{
    public class ClientPet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [StringLength(15)]
        public string? Microchip { get; set; }
        [Required]
        public int Idade { get; set; }
        [Required]
        public float Peso { get; set; }
        [Required]
        public string?  Sexo { get; set; }

        // Foreign Key's
        [ForeignKey(nameof(IdRaca))]
        [Required]
        public string? IdRaca { get; set; }
        public Raca? Raca { get; set; }

        [ForeignKey(nameof(IdEspecie))]
        [Required]
        public string? IdEspecie { get; set; }
        public Especie? Especie { get; set; }

        [ForeignKey(nameof(IdClient))]
        [Required]
        public int IdClient { get; set; }
        public Client? Client { get; set; }

        [ForeignKey(nameof(VetId))]
        [Required]
        public string? VetId { get; set; }
        public User? User { get; set; }
    }
}
