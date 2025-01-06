using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_App_For_Freelancers.Data;
using Vet_App_For_Freelancers.Models.ModelServicos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Vet_App_For_Freelancers.DataAccess
{
    public class ProxVacinacaoAtendimentoDataAccess : BaseData<Vacinacao>
    {
        public ProxVacinacaoAtendimentoDataAccess(SQLiteConnection connection) : base(connection)
        {
        }

        public List<Vacinacao> GetTwoDaysVacinas()
        {
            try
            {
                DateTime date = DateTime.Now;
                var startOfDay = new DateTime(date.Day, date.Year, date.Month);
                var endOfDay = startOfDay.AddDays(2);

                // Busca no banco comparando os valores de início e fim
                return _connection.Table<Vacinacao>()
                          .Where(a => a.DataProxima >= startOfDay && a.DataProxima < endOfDay).OrderBy(a => a.DataProxima.Date)
                          .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao buscar atendimentos pela data: {ex.Message}");
                return new List<Vacinacao>();
            }
        }
    }
}
