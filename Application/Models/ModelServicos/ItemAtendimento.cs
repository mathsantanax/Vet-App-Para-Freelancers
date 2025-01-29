using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Vet_App_For_Freelancers.Models.ModelServicos
{
    public record ItemAtendimento
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal Total {  get; set; }

        [ForeignKey(nameof(IdProduto))]
        public int IdProduto { get; set; }

        [ForeignKey(nameof(IdAtendimento))]
        public int IdAtendimento { get; set; }

        [Ignore]
        public Produto Produto { get; set; } = default!;
        [Ignore]
        public Atendimento Atendimento { get; set; } = default!;
    }
}
