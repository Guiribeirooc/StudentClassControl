using StudentClass.Domain.Interfaces;
using StudentClass.Domain.Models;

namespace StudentClass.Domain.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public RequestResult Add(ClassModel classModel)
        {
            if(classModel.Ano < DateTime.Now.Year)
                return new RequestResult(false, "O ano de cadastro não pode ser menor que o atual.");
            
            var turmas = _classRepository.GetAll();

            if(turmas.Select(x => x.Turma?.ToLower()).Contains(classModel.Turma?.ToLower()))
                return new RequestResult(false, "Nome de turma já cadastrada.");

            _classRepository.Add(classModel);
            return new RequestResult(true, "Turma cadastrada com sucesso.");
        }

        public RequestResult Delete(int id)
        {
            var result = _classRepository.Get(id);

            if (result == null)
                return new RequestResult(false, "Turma não existe ou já foi deletada.");

            _classRepository.Delete(id);
            return new RequestResult(true, "Turma deletada com sucesso.");
        }

        public RequestResult Get(int id)
        { 
            var result = _classRepository.Get(id);

            if(result == null)
                return new RequestResult(false, "Turma não encontrada.");
            
            return new RequestResult(true, "", result);
        }

        public RequestResult GetAll()
        {
            var result = _classRepository.GetAll();

            if (result == null)
                return new RequestResult(false, "Nenhuma turma encontrada.");

            return new RequestResult(true, "", result);
        }

        public RequestResult Update(ClassModel classModel)
        {
            if (classModel.Ano < DateTime.Now.Year)
                return new RequestResult(false, "O ano de atualização não pode ser menor que o atual.");

            var turmas = _classRepository.GetAll();

            if (turmas.Select(x => x.Turma?.ToLower()).Contains(classModel.Turma?.ToLower()))
                return new RequestResult(false, "Nome de turma já cadastrada.");

            _classRepository.Update(classModel);
            return new RequestResult(true, "Turma atualizada com sucesso.");
        }
    }
}
