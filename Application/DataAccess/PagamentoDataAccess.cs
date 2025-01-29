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
    public class PagamentoDataAccess : BaseData<Pagamento>
    {
        public PagamentoDataAccess(SQLiteConnection connection) : base(connection)
        {
            // Inserir o Pagamento "DINHEIRO" apenas se ele não existir
            if (!GetAll().Any(s => s.MetodoPagamento.Equals("DINHEIRO", StringComparison.OrdinalIgnoreCase)))
            {
                Insert(new Pagamento { MetodoPagamento = "DINHEIRO"});
            }
            
            // Inserir o Pagamento "PIX" apenas se ele não existir
            if (!GetAll().Any(s => s.MetodoPagamento.Equals("PIX", StringComparison.OrdinalIgnoreCase)))
            {
                Insert(new Pagamento { MetodoPagamento = "PIX"});
            }
            
            // Inserir o Pagamento "Debito" apenas se ele não existir
            if (!GetAll().Any(s => s.MetodoPagamento.Equals("DÉBITO", StringComparison.OrdinalIgnoreCase)))
            {
                Insert(new Pagamento { MetodoPagamento = "DÉBITO" });
            }
            // Inserir o Pagamento "Crédito" apenas se ele não existir
            if (!GetAll().Any(s => s.MetodoPagamento.Equals("CRÉDITO", StringComparison.OrdinalIgnoreCase)))
            {
                Insert(new Pagamento { MetodoPagamento = "CRÉDITO" });
            }

        }
    }
}
