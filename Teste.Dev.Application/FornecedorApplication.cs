using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Dev.Domain.Constante;
using Teste.Dev.Domain.Dto;
using Teste.Dev.Domain.Entidade;
using Teste.Dev.Domain.IApplication;
using Teste.Dev.Domain.IRepository;
using Teste.Dev.Infra.Repository;

namespace Teste.Dev.Application
{
    public class FornecedorApplication : IFornecedorApplication
    {
        IFornecedorRepository _fornecedorRepository { get; set; }
        IEmpresaRepository _empresaRepository { get; set; }

        public FornecedorApplication()
        {
            _fornecedorRepository = new FornecedorRepository();
            _empresaRepository = new EmpresaRepository();
        }

        public Task<RetornoRequisicao<Fornecedor>> Cadastrar(Fornecedor fornecedor)
        {
            RetornoRequisicao<Fornecedor> retorno = new RetornoRequisicao<Fornecedor>();

            try
            {
                _fornecedorRepository.CadastrarFornecedor(fornecedor);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao cadastrar fornecedor: " + ex.Message;

                return Task.FromResult(retorno);
            }

            return Task.FromResult(retorno); 
        }

        public Task<RetornoRequisicao<List<Fornecedor>>> BuscarTodos()
        {
            RetornoRequisicao<List<Fornecedor>> retorno = new RetornoRequisicao<List<Fornecedor>>();
            retorno.Objeto = new List<Fornecedor>();

            try
            {
                var retornoIList = _fornecedorRepository.BuscarTodos().Result;

                foreach (var item in retornoIList)
                {
                    retorno.Objeto.Add(item);
                }

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao buscar lista de fornecedores: " + ex.Message;

                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<int>> BuscarProximoCodigo()
        {
            RetornoRequisicao<int> retorno = new RetornoRequisicao<int>();

            try
            {
                retorno.Objeto = _fornecedorRepository.BuscarProximoCodigo().Result;

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao buscar próximo código: " + ex.Message;

                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<List<Fornecedor>>> BuscarFiltroListagem(FornecedorFiltroListagemDto filtro)
        {
            RetornoRequisicao<List<Fornecedor>> retorno = new RetornoRequisicao<List<Fornecedor>>();
            retorno.Objeto = new List<Fornecedor>();

            try
            {
                if (filtro.Documento == "" && filtro.NomeFantasia == "" && filtro.DataCadastro == "")
                {
                    var retornoIList = _fornecedorRepository.BuscarTodos().Result;

                    foreach (var item in retornoIList)
                    {
                        retorno.Objeto.Add(item);
                    }

                    return Task.FromResult(retorno);
                }
                else
                {
                    retorno.Objeto = _fornecedorRepository.BuscarEmpresaFiltro(filtro).Result;
                }

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao buscar empresa: " + ex.Message;

                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<Empresa>> BuscarEmpresa(string codigo)
        {
            RetornoRequisicao<Empresa> retorno = new RetornoRequisicao<Empresa>();

            try
            {
                retorno.Objeto = _empresaRepository.BuscarEmpresa(codigo).Result;

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao buscar fornecedor: " + ex.Message;

                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<Fornecedor>> Buscar(string id)
        {
            RetornoRequisicao<Fornecedor> retorno = new RetornoRequisicao<Fornecedor>();

            try
            {
                retorno.Objeto = _fornecedorRepository.Buscar(id).Result;

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao buscar fornecedor: " + ex.Message;

                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<bool>> Deletar(string id)
        {
            RetornoRequisicao<bool> retorno = new RetornoRequisicao<bool>();

            try
            {
                _fornecedorRepository.DeletaCadastroFornecedor(id);
                retorno.Objeto = true;

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao deletar fornecedor: " + ex.Message;
                retorno.Objeto = false;
                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<bool>> Atualizar(Fornecedor fornecedor)
        {
            RetornoRequisicao<bool> retorno = new RetornoRequisicao<bool>();

            try
            {
                _fornecedorRepository.AtualizarCadastroFornecedor(fornecedor);
                retorno.Objeto = true;

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao atualizar cadastro do fornecedor: " + ex.Message;
                retorno.Objeto = false;
                return Task.FromResult(retorno);
            }
        }

    }
}
