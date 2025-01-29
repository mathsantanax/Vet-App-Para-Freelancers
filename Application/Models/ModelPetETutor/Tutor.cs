using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_App_For_Freelancers.Models.ModelPetETutor
{
    public record Tutor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NomeTutor { get; set; } = default!;
        public decimal Celular { get; set; } = default!;
        [Ignore]
        Pet Pet { get; set; } = default!;
    }
}
