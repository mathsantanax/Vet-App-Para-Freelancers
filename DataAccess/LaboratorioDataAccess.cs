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
    public class LaboratorioDataAccess : BaseData<Lab>
    {
        public LaboratorioDataAccess(SQLiteConnection connection) : base(connection)
        {
        }
    }
}
