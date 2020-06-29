namespace Teste.Dev.Domain.Constante
{
    public class RetornoRequisicao<T>
    {
        public T Objeto { get; set; }
        public string Mensagem { get; set; }

        public RetornoRequisicao()
        {
            Mensagem = "";
        }

        public RetornoRequisicao(T obj)
        {
            Objeto = obj;
        }
    }
}
