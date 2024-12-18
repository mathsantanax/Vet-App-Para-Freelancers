using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_App_For_Freelancers.Models.ModelPetETutor
{
    public record class Especie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = default!;

        public string NomeEspecie { get; set; } = default!;
    }
}
