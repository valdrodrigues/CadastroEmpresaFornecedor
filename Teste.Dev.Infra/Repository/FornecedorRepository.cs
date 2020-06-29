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
    public class FornecedorRepository : IFornecedorRepository
    {
        private IMongoCollection<Fornecedor> _base;

        public FornecedorRepository()
        {
            IMongoClient _client = new MongoClient("mongodb://localhost");
            IMongoDatabase _database = _client.GetDatabase("TesteBaseMongo");
            _base = _database.GetCollection<Fornecedor>("Fornecedor");
        }

        public async Task CadastrarFornecedor(Fornecedor fornecedor)
        {
            await _base.InsertOneAsync(fornecedor);
        }

        public async Task AtualizarCadastroFornecedor(Fornecedor fornecedor)
        {
            Expression<Func<Fornecedor, bool>> filter =
                x => x.Id.Equals(fornecedor.Id);

            Fornecedor forn = _base.Find(filter).FirstOrDefault();
            if (forn != null)
            {
                ReplaceOneResult result = await _base.ReplaceOneAsync(filter, fornecedor);
            }
        }

        public async Task DeletaCadastroFornecedor(string id)
        {
            Expression<Func<Fornecedor, bool>> filter =
                x => x.Id.Equals(id);
            DeleteResult delresult = await _base.DeleteOneAsync(filter);
        }

        public async Task<IList<Fornecedor>> BuscarTodos()
        {
            IMongoQueryable<Fornecedor> query = _base.AsQueryable().OrderBy(x => x.Codigo);

            IList<Fornecedor> fornecedorList = query.ToList();

            return fornecedorList;
        }

        public async Task<int> BuscarProximoCodigo()
        {
            IMongoQueryable<Fornecedor> query = _base.AsQueryable().OrderBy(x => x.Codigo);

            List<Fornecedor> fornecedorList = query.ToList();

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

        public async Task<Fornecedor> Buscar(string id)
        {
            Expression<Func<Fornecedor, bool>> filter =
                x => x.Id.Equals(id);

            return _base.Find(filter).FirstOrDefault();
        }

        public async Task<List<Fornecedor>> BuscarEmpresaFiltro(FornecedorFiltroListagemDto filtro)
        {
            Expression<Func<Fornecedor, bool>> filter = null;

            if (filtro != null)
            {
                if (filtro.Documento != null)
                {
                    filter = x => x.Documento.Equals(filtro.Documento);

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
