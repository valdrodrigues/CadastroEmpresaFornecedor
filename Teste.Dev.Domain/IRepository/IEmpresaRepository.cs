using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Dev.Domain.Dto;
using Teste.Dev.Domain.Entidade;

namespace Teste.Dev.Domain.IRepository
{
    public interface IEmpresaRepository
    {
        Task CadastrarEmpresa(Empresa empresa);
        Task AtualizarCadastroEmpresa(Empresa empresa);
        Task DeletaCadastroEmpresa(string id);
        Task<Empresa> Buscar(string id);
        Task<Empresa> BuscarEmpresa(string codigo);
        Task<IList<Empresa>> BuscarTodos();
        Task<int> BuscarProximoCodigo();
        Task<List<Empresa>> BuscarEmpresaFiltro(EmpresaFiltroListagemDto filtro);
    }
}
