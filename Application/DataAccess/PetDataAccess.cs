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
    public class PetDataAccess : BaseData<Pet>
    {
        public PetDataAccess(SQLiteConnection connection) : base(connection) { }

        public List<Pet> GetByIdTutor(int id)
        {
            return _connection.Table<Pet>().Where(p => p.IdTutor.Equals(id)).ToList();
        }
    }
}
