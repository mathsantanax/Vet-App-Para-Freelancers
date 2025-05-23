﻿using SQLite;
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
    public class ProdutosAtendimentoDataAccess : BaseData<ItemAtendimento>
    {
        public ProdutosAtendimentoDataAccess(SQLiteConnection connection) : base(connection)
        {
        }
        public List<ItemAtendimento> getProdutosByIdAtendimento(int id)
        {
            try
            {
                return _connection.Table<ItemAtendimento>().Where(item => item.IdAtendimento.Equals(id)).ToList();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<ItemAtendimento>();
            }
        }
    }
}
