using StudentClass.Domain.Models;

namespace StudentClass.UI.Models
{
    public class ClassResponse
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public List<ClassModel>? Dados { get; set; } = new List<ClassModel>();
    }
}
