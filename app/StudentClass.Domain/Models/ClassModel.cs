using System.ComponentModel.DataAnnotations;

namespace StudentClassDomain.Models
{
    public class ClassModel
    {
        [Key]
        public int Id { get; set; }
        public int IdCurso { get; set; }
        public string? Turma { get; set; }
        public int Ano { get; set; }
    }
}
