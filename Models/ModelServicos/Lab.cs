using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_App_For_Freelancers.Models.ModelServicos
{
    public record class Lab
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string NomeLaboratorio { get; set; } = default!;
    }
}
