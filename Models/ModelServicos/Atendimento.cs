using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_App_For_Freelancers.Models.ModelPetETutor;

namespace Vet_App_For_Freelancers.Models.ModelServicos
{
    public record Atendimento
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool FezVacinacao { get; set; }
        public DateTime Data { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }

        [ForeignKey(nameof(IdTutor))]
        public int IdTutor { get; set; }
        
        [ForeignKey(nameof(IdPet))]
        public int IdPet { get; set; } 
        
        [ForeignKey(nameof(IdPagamento))]
        public int IdPagamento { get; set; }

        [Ignore]
        public Tutor Tutor { get; set; } = default!;
        [Ignore]
        public Pet Pet { get; set; } = default!;
        [Ignore]
        public Pagamento Pagamento { get; set; } = default!;

        [Ignore]
        public List<ItemAtendimento> itemAtendimento { get; set; } = default!;

        [Ignore]
        public List<ItemServico> ItemServico { get; set; } = default!;
    }
}
