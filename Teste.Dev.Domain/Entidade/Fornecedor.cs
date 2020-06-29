using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using Teste.Dev.Domain.Enum;

namespace Teste.Dev.Domain.Entidade
{
    [BsonIgnoreExtraElements]
    public class Fornecedor
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Codigo { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public List<string> Telefone { get; set; } = new List<string>();
        public TipoPessoa Pessoa { get; set; }
        public DateTime DataNascimento { get; set; }
        public string RG { get; set; }
        public string Documento { get; set; }
        [BsonIgnore]
        public int Control { get; set; }        //Usado pra definir se é insert ou update
    }
}
