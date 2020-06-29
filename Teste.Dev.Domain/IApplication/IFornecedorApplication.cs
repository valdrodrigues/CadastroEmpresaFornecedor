using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Dev.Domain.Constante;
using Teste.Dev.Domain.Dto;
using Teste.Dev.Domain.Entidade;

namespace Teste.Dev.Domain.IApplication
{
    public interface IFornecedorApplication
    {
        Task<RetornoRequisicao<Fornecedor>> Cadastrar(Fornecedor fornecedor);
        Task<RetornoRequisicao<List<Fornecedor>>> BuscarTodos();
        Task<RetornoRequisicao<Fornecedor>> Buscar(string id);
        Task<RetornoRequisicao<Empresa>> BuscarEmpresa(string codigo);
        Task<RetornoRequisicao<bool>> Deletar(string id);
        Task<RetornoRequisicao<int>> BuscarProximoCodigo();
        Task<RetornoRequisicao<bool>> Atualizar(Fornecedor fornecedor);
        Task<RetornoRequisicao<List<Fornecedor>>> BuscarFiltroListagem(FornecedorFiltroListagemDto filtro);
    }
}
