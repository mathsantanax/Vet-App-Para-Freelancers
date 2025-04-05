using Microsoft.VisualBasic.FileIO;
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

        /// Obtém as vacinações que estão programadas para os próximos Dois dias.
        public List<Vacinacao> GetTwoDaysVacinas()
        {
            try
            {
                DateTime hoje = DateTime.Today;
                DateTime doisDiasDepois = hoje.AddDays(3).AddTicks(-1);

                return _connection.Table<Vacinacao>()
                          .Where(a => a.DataProxima >= hoje && a.DataProxima <= doisDiasDepois)
                          .OrderBy(a => a.DataProxima)
                          .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao buscar vacinações dos próximos dois dias: {ex.Message}");
                return new List<Vacinacao>();
            }
        }

        /// Obtém todas as vacinações de um pet específico.
        public List<Vacinacao> GetAllByIdPet(int id)
        {
            try
            {
                return _connection.Table<Vacinacao>()
                                  .Where(v => v.IdPet == id)
                                  .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao buscar vacinações por ID do pet: {ex.Message}");
                return new List<Vacinacao>();
            }
        }

        /// Deleta vacinações antigas (anteriores à data atual) para um pet específico.
        public async Task<string> DeleteVacinasAntigas(int idPet)
        {
            try
            {
                await Task.Delay(100);
                DateTime hoje = DateTime.Today;

                var vacinasAntigas = _connection.Table<Vacinacao>()
                                         .Where(v => v.IdPet == idPet && v.DataProxima < hoje)
                                         .ToList();

                if (!vacinasAntigas.Any())
                    return "Nenhuma vacinação antiga encontrada.";

                foreach (var vacina in vacinasAntigas)
                {
                    _connection.Delete(vacina);
                }

                return $"{vacinasAntigas.Count} vacinação(ões) antiga(s) removida(s).";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao deletar vacinações antigas: {ex.Message}");
                return $"Erro: {ex.Message}";
            }
        }
    }
}
