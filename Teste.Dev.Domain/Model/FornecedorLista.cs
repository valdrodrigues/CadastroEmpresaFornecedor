using System.Collections.Generic;
using Teste.Dev.Domain.Entidade;

namespace Teste.Dev.Domain.Model
{
    public class FornecedorLista
    {
        public List<Fornecedor> FornecedorList { get; set; }

        public FornecedorLista()
        {
            FornecedorList = new List<Fornecedor>();
        }
    }
}
