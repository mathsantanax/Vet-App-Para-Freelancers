using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.Models.ModelPetETutor;

namespace Vet_App_For_Freelancers.DataAccess
{
    public class EspecieDataAccess : BaseData<Especie>
    {
        public EspecieDataAccess(SQLiteConnection connection) : base(connection) { }
    }
}
