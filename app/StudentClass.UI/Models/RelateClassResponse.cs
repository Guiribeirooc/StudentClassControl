using StudentClass.Domain.Models;

namespace StudentClass.UI.Models
{
    public class RelateClassResponse
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public List<RelateClassModel>? Dados { get; set; }
    }
}
