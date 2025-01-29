using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_App_For_Freelancers.Models.ModelServicos
{
    public record class Servico
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NomeService { get; set; } = string.Empty;
        public decimal Preco {  get; set; }
    }
}
