using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Dev.Domain.Constante;
using Teste.Dev.Domain.Dto;
using Teste.Dev.Domain.Entidade;

namespace Teste.Dev.Domain.IApplication
{
    public interface IEmpresaApplication
    {
        Task<RetornoRequisicao<Empresa>> Cadastrar(Empresa empresa);
        Task<RetornoRequisicao<List<Empresa>>> BuscarTodos();
        Task<RetornoRequisicao<Empresa>> Buscar(string id);
        Task<RetornoRequisicao<bool>> Deletar(string id);
        Task<RetornoRequisicao<Empresa>> BuscarPorCodigo(string codigo);
        Task<RetornoRequisicao<bool>> Atualizar(Empresa Empresa);
        Task<RetornoRequisicao<int>> BuscarProximoCodigo();
        Task<RetornoRequisicao<List<Empresa>>> BuscarFiltroListagem(EmpresaFiltroListagemDto filtro);
    }
}
