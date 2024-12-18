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
    public class ProdutoDataAccess : BaseData<Produto>
    {
        public ProdutoDataAccess(SQLiteConnection connection) : base(connection) 
        {
            if (!GetAll().Any(s => s.NomePruduto.Equals("V10", StringComparison.OrdinalIgnoreCase)))
            {
                Insert(new Produto { NomePruduto = "V10", Preco = 60.00m, IdLab = 1 });
            }
        }
    }
}
