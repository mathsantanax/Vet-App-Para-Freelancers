using api_vet_app.Models.Persona;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_vet_app.Models.Service
{
    public class ProductItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50), Required]
        public string? Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        // Valor unitário
        [Required]
        public decimal? Price { get; set; }
        // Valor total
        [Required]
        public decimal Amount { get; set; }

        // Foreign Key's
        [ForeignKey(nameof(IdProduct)), Required]
        public int IdProduct { get; set; }
        public Product? Product { get; set; }
        [ForeignKey(nameof(IdAttending)), Required]
        public int IdAttending { get; set; }
        public Attending? Attending { get; set; }
        [ForeignKey(nameof(VetId)), Required]
        public string? VetId { get; set; }
        public User? User { get; set; }
    }
}
