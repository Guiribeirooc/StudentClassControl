using Microsoft.AspNetCore.Mvc;
using StudentClass.Domain.Interfaces;
using StudentClass.Domain.Models;

namespace StudentClass.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("Obter/{id:int}")]
        [ProducesResponseType(typeof(StudentModel), 200)]
        public IActionResult Obter([FromRoute] int id)
        {
            return Ok(_studentService.Get(id));
        }

        [HttpGet("Obter-Todos")]
        [ProducesResponseType(typeof(List<StudentModel>), 200)]
        public IActionResult ObterTodos()
        {
            return Ok(_studentService.GetAll());
        }

        [HttpPost("Incluir")]
        [ProducesResponseType(typeof(RequestResult), 200)]
        public IActionResult Incluir([FromBody] StudentModel student)
        {
            return Ok(_studentService.Add(student));
        }

        [HttpPut("Atualizar")]
        [ProducesResponseType(typeof(RequestResult), 200)]
        public IActionResult Atualizar([FromBody] StudentModel student)
        {
            return Ok(_studentService.Update(student));
        }

        [HttpDelete("Deletar/{id:int}")]
        [ProducesResponseType(typeof(RequestResult), 200)]
        public IActionResult Deletar([FromRoute] int id)
        {
            return Ok(_studentService.Delete(id));
        }
    }
}
