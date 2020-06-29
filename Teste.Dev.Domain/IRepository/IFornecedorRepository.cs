using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Dev.Domain.Dto;
using Teste.Dev.Domain.Entidade;

namespace Teste.Dev.Domain.IRepository
{
    public interface IFornecedorRepository
    {
        Task CadastrarFornecedor(Fornecedor fornecedor);
        Task AtualizarCadastroFornecedor(Fornecedor fornecedor);
        Task DeletaCadastroFornecedor(string id);
        Task<Fornecedor> Buscar(string id);
        Task<IList<Fornecedor>> BuscarTodos();
        Task<int> BuscarProximoCodigo();
        Task<List<Fornecedor>> BuscarEmpresaFiltro(FornecedorFiltroListagemDto filtro);
    }
}
