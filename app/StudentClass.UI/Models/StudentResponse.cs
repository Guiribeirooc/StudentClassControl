using StudentClass.Domain.Models;

namespace StudentClass.UI.Models
{
    public class StudentResponse
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public List<StudentModel>? Dados { get; set; }
    }
}
