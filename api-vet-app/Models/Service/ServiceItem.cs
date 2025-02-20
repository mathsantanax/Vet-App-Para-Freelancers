using api_vet_app.Models.Persona;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_vet_app.Models.Service
{
    public class ServiceItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
        public int Id { get; set; }
        // Nome
        [StringLength(50), Required]
        public string? Name { get; set; }
        // Quantidade
        [Required]
        public int Quantity { get; set; }
        // Valor Unitário
        [Required]
        public decimal? Price { get; set; }
        // Total
        [Required]
        public decimal Amount { get; set; }

        // Foreign Key's
        [ForeignKey(nameof(IdJob)), Required]
        public int? IdJob {  get; set; }
        [JsonIgnore]
        public Job? Job { get; set; }

        [ForeignKey(nameof(IdAttending)), Required]
        public int? IdAttending { get; set; }
        [JsonIgnore]
        public Attending? Attending { get; set; }

        [ForeignKey(nameof(VetId)), Required]
        public string? VetId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}
