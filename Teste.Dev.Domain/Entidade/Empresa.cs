using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.ComponentModel.DataAnnotations;

namespace Teste.Dev.Domain.Entidade
{
    [BsonIgnoreExtraElements]
    public class Empresa
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Codigo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "UF inválido!")]
        public string UF { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome inválido!")]
        public string NomeFantasia { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CNPJ inválido!")]
        public string CNPJ { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        [BsonIgnore]
        public int Control { get; set; }         //Usado pra definir se é insert ou update
    }
}
