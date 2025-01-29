using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_App_For_Freelancers.Models.ModelServicos
{
    public record class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(nameof(IdLab))]
        public int IdLab { get; set; }
        public string NomePruduto { get; set; } = default!;
        public decimal Preco { get; set; }

        [Ignore]
        public Lab Lab { get; set; } = default!;

    }
}
