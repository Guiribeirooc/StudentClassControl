using StudentClass.Domain.Interfaces;
using StudentClass.Domain.Models;
using StudentClass.Domain.Models.Requests;

namespace StudentClass.Domain.Services
{
    public class RelateClassService : IRelateClassService
    {
        private readonly IRelateClassRepository _relateClassRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;

        public RelateClassService(
            IRelateClassRepository relateClassRepository,
            IStudentRepository studentRepository,
            IClassRepository classRepository)
        {
            _relateClassRepository = relateClassRepository;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        public RequestResult Add(RelateClassRequest request)
        {
            var getStudent = _studentRepository.Get(request.IdStudent);
            var getClass = _classRepository.Get(request.IdClass);

            if (getStudent == null && getClass == null)
                return new RequestResult(false, "Aluno ou turma não foram encontrados.");

            var getRelateClass = _relateClassRepository.Get(request.IdStudent, request.IdClass);

            if (getRelateClass != null)
                return new RequestResult(false, "Aluno já está associado a turma selecionada.");

            _relateClassRepository.Add(request);
            return new RequestResult(true, "Associação de turma e aluno realizado com sucesso.");
        }

        public RequestResult Delete(int idStudent, int idClass)
        {
            var getRelateClass = _relateClassRepository.Get(idStudent, idClass);

            if (getRelateClass == null)
                return new RequestResult(false, "Esta associação não existe ou já foi deletada.");

            _relateClassRepository.Delete(idStudent, idClass);
            return new RequestResult(true, "Associação de turma e aluno deletado com sucesso.");
        }

        public RequestResult Get(int idStudent, int idClass)
        {
            var result = _relateClassRepository.Get(idStudent, idClass);
            return new RequestResult(true, "", result);
        }

        public RequestResult GetAll()
        {
            var relateClass = _relateClassRepository.GetAll();
            var students = _studentRepository.GetAll();
            var classes = _classRepository.GetAll();

            if(relateClass != null && relateClass.Count > 0)
            {
                relateClass.ForEach(x =>
                {
                    x.Student= students.FirstOrDefault(y => y.Id == x.IdStudent);
                    x.Class = classes.FirstOrDefault(y => y.Id == x.IdClass);
                });
            }

            return new RequestResult(true, "", relateClass);
        }
    }
}
