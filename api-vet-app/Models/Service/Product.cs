using api_vet_app.Models.Persona;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_vet_app.Models.Service
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50), Required]
        public string? Name { get; set; }
        [Required]
        public decimal? Price { get; set; }

        // Foreign Key's
        [ForeignKey(nameof(IdLab)), Required]
        public string? IdLab { get; set; }
        public Lab? Lab { get; set; }

        [ForeignKey(nameof(VetId)), Required]
        public string? VetId { get; set; }
        public User? User { get; set; }

    }
}
