using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_App_For_Freelancers.Presentation
{
    public class ServicoPresentation
    {
        public string Nome { get; set; } = default!;
        public int Quantidade { get; set; }
        public decimal Preco {  get; set; }
        public decimal Total { get; set; }
        public string Laboratorio { get; set; } = default!; 
    }
}
