using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Teste.Dev.Domain.Dto;
using Teste.Dev.Domain.Entidade;
using Teste.Dev.Domain.IRepository;

namespace Teste.Dev.Infra.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private IMongoCollection<Empresa> _base;

        public EmpresaRepository()
        {
            IMongoClient _client = new MongoClient("mongodb://localhost");
            IMongoDatabase _database = _client.GetDatabase("TesteBaseMongo");
            _base = _database.GetCollection<Empresa>("Empresa");
        }

        public async Task CadastrarEmpresa(Empresa empresa)
        {
            await _base.InsertOneAsync(empresa);
        }

        public async Task AtualizarCadastroEmpresa(Empresa empresa)
        {
            Expression<Func<Empresa, bool>> filter =
                x => x.Id.Equals(empresa.Id);

            Empresa emp = _base.Find(filter).FirstOrDefault();
            if (emp != null)
            {
                ReplaceOneResult result = await _base.ReplaceOneAsync(filter, empresa);
            }
        }

        public async Task DeletaCadastroEmpresa(string id)
        {
            Expression<Func<Empresa, bool>> filter =
                x => x.Id.Equals(id);
            DeleteResult delresult = await _base.DeleteOneAsync(filter);
        }

        public async Task<IList<Empresa>> BuscarTodos()
        {
            IMongoQueryable<Empresa> query = _base.AsQueryable().OrderBy(x => x.Codigo);

            IList<Empresa> empresas = query.ToList();

            return empresas;
        }

        public async Task<Empresa> Buscar(string id)
        {
            Expression<Func<Empresa, bool>> filter =
                x => x.Id.Equals(id);

            return _base.Find(filter).FirstOrDefault();
        }

        public async Task<Empresa> BuscarEmpresa(string codigo)
        {
            Expression<Func<Empresa, bool>> filter =
                x => x.Codigo.Equals(codigo);

            return _base.Find(filter).FirstOrDefault();
        }

        public async Task<int> BuscarProximoCodigo()
        {
            IMongoQueryable<Empresa> query = _base.AsQueryable().OrderBy(x => x.Codigo);

            List<Empresa> fornecedorList = query.ToList();

            if (fornecedorList.Count > 0)
            {
                var ultimoRegistro = fornecedorList[fornecedorList.Count - 1].Codigo;

                return ultimoRegistro + 1;
            }
            else
            {
                return 1;
            }
        }

        public async Task<List<Empresa>> BuscarEmpresaFiltro(EmpresaFiltroListagemDto filtro)
        {
            Expression<Func<Empresa, bool>> filter = null;

            if (filtro != null)
            {
                if (filtro.CNPJ != null)
                {
                    filter = x => x.CNPJ.Equals(filtro.CNPJ);

                    return _base.Find(filter).ToList();
                }
                else
                {
                    if (filtro.NomeFantasia != null)
                    {
                        filter = x => x.NomeFantasia.Equals(filtro.NomeFantasia);
                        return _base.Find(filter).ToList();
                    }
                    else
                    {
                        if (filtro.DataCadastro != null)
                        {
                            filter = x => x.DataCadastro.Equals(filtro.DataCadastro);
                            return _base.Find(filter).ToList();
                        }
                    }
                }
            }

            return null;
        }

    }
}

