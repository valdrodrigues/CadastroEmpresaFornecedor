using System.ComponentModel;

namespace Teste.Dev.Domain.Enum
{
    public enum TipoPessoa : short
    {
        [Description("Física")]
        Fisica = 0,
        [Description("Jurídica")]
        Juridica = 1
    }
}
