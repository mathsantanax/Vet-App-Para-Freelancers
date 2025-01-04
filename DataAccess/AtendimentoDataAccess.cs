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
    public class AtendimentoDataAccess : BaseData<Atendimento>
    {
        public AtendimentoDataAccess(SQLiteConnection connection) : base(connection)
        {
        }

        public Atendimento GetLast()
        {
            try
            {
                return _connection.Table<Atendimento>().Last();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching tutor by name: {ex.Message}");
                return new Atendimento();
            }
        }
        public List<Atendimento> SearchByIdPet(int id)
        {
            try
            {
                return _connection.Table<Atendimento>().Where(a => a.IdPet.Equals(id)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching tutor by name: {ex.Message}");
                return new List<Atendimento>();
            }
        }
    }
}
