using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Dev.Domain.Constante;
using Teste.Dev.Domain.Dto;
using Teste.Dev.Domain.Entidade;
using Teste.Dev.Domain.IApplication;
using Teste.Dev.Domain.IRepository;
using Teste.Dev.Infra.Repository;

namespace Teste.Dev.Application
{
    public class EmpresaApplication : IEmpresaApplication
    {
        IEmpresaRepository _empresaRepository {get; set;}

        public EmpresaApplication()
        {
            _empresaRepository = new EmpresaRepository();
        }

        public Task<RetornoRequisicao<Empresa>> Cadastrar(Empresa empresa)
        {
            RetornoRequisicao<Empresa> retorno = new RetornoRequisicao<Empresa>();

            try
            {
                _empresaRepository.CadastrarEmpresa(empresa);

                retorno.Mensagem = "OK";
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao cadastrar empresa: " + ex.Message;

                return Task.FromResult(retorno);
            }

            return Task.FromResult(retorno);
        }

        public Task<RetornoRequisicao<List<Empresa>>> BuscarTodos()
        {
            RetornoRequisicao<List<Empresa>> retorno = new RetornoRequisicao<List<Empresa>>();
            retorno.Objeto = new List<Empresa>();

            try
            {
                var retornoIList = _empresaRepository.BuscarTodos().Result;

                foreach (var item in retornoIList)
                {
                    retorno.Objeto.Add(item);
                }

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao buscar lista de empresas: " + ex.Message;

                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<int>> BuscarProximoCodigo()
        {
            RetornoRequisicao<int> retorno = new RetornoRequisicao<int>();

            try
            {
                retorno.Objeto = _empresaRepository.BuscarProximoCodigo().Result;

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao buscar próximo código: " + ex.Message;

                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<List<Empresa>>> BuscarFiltroListagem(EmpresaFiltroListagemDto filtro)
        {
            RetornoRequisicao<List<Empresa>> retorno = new RetornoRequisicao<List<Empresa>>();
            retorno.Objeto = new List<Empresa>();

            try
            {
                if (filtro.CNPJ == "" && filtro.NomeFantasia == "" && filtro.DataCadastro == "")
                {
                    var retornoIList = _empresaRepository.BuscarTodos().Result;

                    foreach (var item in retornoIList)
                    {
                        retorno.Objeto.Add(item);
                    }

                    return Task.FromResult(retorno);
                }
                else
                {
                    retorno.Objeto = _empresaRepository.BuscarEmpresaFiltro(filtro).Result;
                }                    

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao buscar empresa: " + ex.Message;

                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<Empresa>> Buscar(string id)
        {
            RetornoRequisicao<Empresa> retorno = new RetornoRequisicao<Empresa>();

            try
            {
                retorno.Objeto = _empresaRepository.Buscar(id).Result;

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao buscar empresa: " + ex.Message;

                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<Empresa>> BuscarPorCodigo(string codigo)
        {
            RetornoRequisicao<Empresa> retorno = new RetornoRequisicao<Empresa>();

            try
            {
                retorno.Objeto = _empresaRepository.BuscarEmpresa(codigo).Result;

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao buscar empresa: " + ex.Message;

                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<bool>> Deletar(string id)
        {
            RetornoRequisicao<bool> retorno = new RetornoRequisicao<bool>();

            try
            {
                _empresaRepository.DeletaCadastroEmpresa(id);
                retorno.Objeto = true;

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao deletar empresa: " + ex.Message;
                retorno.Objeto = false;
                return Task.FromResult(retorno);
            }
        }

        public Task<RetornoRequisicao<bool>> Atualizar(Empresa Empresa)
        {
            RetornoRequisicao<bool> retorno = new RetornoRequisicao<bool>();

            try
            {
                _empresaRepository.AtualizarCadastroEmpresa(Empresa);
                retorno.Objeto = true;

                return Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = "Erro ao atualizar cadastro da empresa: " + ex.Message;
                retorno.Objeto = false;
                return Task.FromResult(retorno);
            }
        }

    }
}
