using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_App_For_Freelancers.Models.ModelPetETutor
{
    public record Pet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NomePet { get; set; } = default!;
        public string NumeroMicrochip { get; set; } = default!;
        public int Idade { get; set; } = default!;
        public string Sexo { get; set; } = default!;
        public float Peso { get; set; } = default!;

        [ForeignKey(nameof(IdRaca))]
        public int IdRaca { get; set; } = default!;

        [ForeignKey(nameof(IdEspecie))]
        public int IdEspecie { get; set; } = default!;

        [ForeignKey(nameof(IdTutor))]
        public int IdTutor { get; set; } = default!;

        [Ignore]
        public Especie Especie { get; set; } = default!;
        [Ignore]
        public Raca Raca { get; set; } = default!;
    }
}
