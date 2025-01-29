using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public List<Atendimento> GetAtendimentosByDate(DateTime date)
        {
            try
            {
                var startOfMonth = new DateTime(date.Year, date.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1);

                // Busca no banco comparando os valores de início e fim
                return _connection.Table<Atendimento>()
                                  .Where(a => a.Data >= startOfMonth && a.Data < endOfMonth)
                                  .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao buscar atendimentos pela data: {ex.Message}");
                return new List<Atendimento>();
            }
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
