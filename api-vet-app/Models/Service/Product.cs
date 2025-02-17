using api_vet_app.Models.Persona;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_vet_app.Models.Service
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
        public int Id { get; set; }
        [StringLength(50), Required]
        public string? Name { get; set; }
        [Required]
        public decimal? Price { get; set; }

        // Foreign Key's
        [ForeignKey(nameof(IdLab)), Required]
        public int IdLab { get; set; }
        [JsonIgnore]
        public Lab? Lab { get; set; }

        [ForeignKey(nameof(VetId)), Required, JsonIgnore]
        public string? VetId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}
