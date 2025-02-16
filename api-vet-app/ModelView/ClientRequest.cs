using api_vet_app.Models.Persona;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_vet_app.Models.ModelView
{
    public record ClientRequest
    {
        public string? Name { get; set; }
        public decimal Celular { get; set; }

    }
}
