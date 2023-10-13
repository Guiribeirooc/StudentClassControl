namespace StudentClass.Domain.Models
{
    public class RequestResult
    {
        public RequestResult(bool sucesso, string? mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public RequestResult(bool sucesso, string? mensagem, object? dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }

        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public object? Dados { get; set; }
    }
}
