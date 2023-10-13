using StudentClass.Domain.Models;

namespace StudentClass.UI.Models
{
    public class StudentClassResponse
    {
        public List<StudentModel>? Student { get; set; }
        public List<ClassModel>? Class { get; set; }
        public int StudentIdSelected { get; set; }
        public int ClassIdSelected { get; set; }
    }
}
