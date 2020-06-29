using System.Collections.Generic;
using Teste.Dev.Domain.Entidade;

namespace Teste.Dev.Domain.Model
{
    public class EmpresaLista
    {
        public List<Empresa> EmpresaList { get; set; }

        public EmpresaLista()
        {
            EmpresaList = new List<Empresa>();
        }
    }
}
