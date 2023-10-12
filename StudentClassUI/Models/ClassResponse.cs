using StudentClassDomain.Models;

namespace StudentClassUI.Models
{
    public class ClassResponse
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public List<ClassModel>? Dados { get; set; }
    }
}
