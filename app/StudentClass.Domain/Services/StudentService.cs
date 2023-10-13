using StudentClass.Domain.Helpers;
using StudentClass.Domain.Interfaces;
using StudentClass.Domain.Models;

namespace StudentClass.Domain.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public RequestResult Add(StudentModel studentModel)
        {
            PasswordAdvisor passwordAdvisor = new PasswordAdvisor();
            var passwordScore = passwordAdvisor.CheckStrength(studentModel.Senha);

            if (passwordScore != Enums.PasswordScore.Strong && passwordScore != Enums.PasswordScore.VeryStrong)
                return new RequestResult(true, "Senha deve conter no mínimo 8 caracteres, sendo 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial.");

#pragma warning disable CS8604 // Possible null reference argument.
            studentModel.Senha = passwordAdvisor.Hash(studentModel.Senha);
#pragma warning restore CS8604 // Possible null reference argument.

            _studentRepository.Add(studentModel);
            return new RequestResult(true, "Aluno incluído com sucesso.");
        }

        public RequestResult Delete(int id)
        {
            var result = _studentRepository.Get(id);

            if (result == null)
                return new RequestResult(false, "Aluno não existe ou já foi deletado.");

            _studentRepository.Delete(id);
            return new RequestResult(true, "Aluno deletado com sucesso.");
        }

        public RequestResult Get(int id)
        {
            var result = _studentRepository.Get(id);

            if (result == null)
                return new RequestResult(false, "Aluno não existe ou já foi deletado.");

            return new RequestResult(true, "", result);
        }

        public RequestResult GetAll()
        {
            var result = _studentRepository.GetAll();

            if(result == null)
                return new RequestResult(false, "Nenhum aluno encontrado.");

            return new RequestResult(true, "", result);
        }

        public RequestResult Update(StudentModel studentModel)
        {
            PasswordAdvisor passwordAdvisor = new PasswordAdvisor();
            var passwordScore = passwordAdvisor.CheckStrength(studentModel.Senha);

            if (passwordScore != Enums.PasswordScore.Strong && passwordScore != Enums.PasswordScore.VeryStrong)
                return new RequestResult(true, "Senha deve conter no mínimo 8 caracteres, sendo 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial.");

#pragma warning disable CS8604 // Possible null reference argument.
            studentModel.Senha = passwordAdvisor.Hash(studentModel.Senha);
#pragma warning restore CS8604 // Possible null reference argument.

            _studentRepository.Update(studentModel);
            return new RequestResult(true, "Aluno atualizado com sucesso.");
        }
    }
}
