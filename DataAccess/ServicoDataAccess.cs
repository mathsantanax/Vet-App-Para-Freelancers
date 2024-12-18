using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.Models.ModelServicos;

namespace Vet_App_For_Freelancers.DataAccess
{
    public class ServicoDataAccess : BaseData<Servico>
    {
        public ServicoDataAccess(SQLiteConnection connection) : base(connection) 
        {
            // Inserir o serviço "VACINAÇÃO" apenas se ele não existir
            if (!GetAll().Any(s => s.NomeService.Equals("VACINAÇÃO", StringComparison.OrdinalIgnoreCase)))
            {
                Insert(new Servico { NomeService = "VACINAÇÃO", Preco = 60.00m });
            }

            // Inserir o serviço "CONSULTA" apenas se ele não existir
            if (!GetAll().Any(s => s.NomeService.Equals("CONSULTA", StringComparison.OrdinalIgnoreCase)))
            {
                Insert(new Servico { NomeService = "CONSULTA", Preco = 90.00m });
            }
        }
    }
}
