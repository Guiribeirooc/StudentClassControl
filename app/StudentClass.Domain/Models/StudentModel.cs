using System.ComponentModel.DataAnnotations;

namespace StudentClassDomain.Models
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Usuario { get; set; }
        public string? Senha { get; set; }
    }
}
