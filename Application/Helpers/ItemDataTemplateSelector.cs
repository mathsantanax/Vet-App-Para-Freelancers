using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_App_For_Freelancers.Models.ModelServicos;

namespace Vet_App_For_Freelancers.Helpers
{
    public class ItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? ServicoTemplate { get; set; }
        public DataTemplate? ProdutoTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ItemServico)
            {
                return ServicoTemplate!;
            }
            else if (item is ItemAtendimento)
            {
                return ProdutoTemplate!;
            }
            return null!;
        }
    }
}
