using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_App_For_Freelancers.Models.ModelPetETutor
{
    public record Raca
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TipoRaca { get; set; } = default!;
    }
}
