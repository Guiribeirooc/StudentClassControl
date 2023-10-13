using Microsoft.AspNetCore.Mvc;
using StudentClass.Domain.Interfaces;
using StudentClass.Domain.Models;

namespace StudentClassAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet("Obter/{id:int}")]
        [ProducesResponseType(typeof(ClassModel), 200)]
        public IActionResult Obter([FromRoute] int id)
        {
            return Ok(_classService.Get(id));
        }

        [HttpGet("Obter-Todos")]
        [ProducesResponseType(typeof(List<ClassModel>), 200)]
        public IActionResult ObterTodos()
        {
            return Ok(_classService.GetAll());
        }

        [HttpPost("Incluir")]
        [ProducesResponseType(typeof(RequestResult), 200)]
        public IActionResult Incluir([FromBody] ClassModel classModel)
        {
            return Ok(_classService.Add(classModel));
        }

        [HttpPut("Atualizar")]
        [ProducesResponseType(typeof(RequestResult), 200)]
        public IActionResult Atualizar([FromBody] ClassModel classModel)
        {
            return Ok(_classService.Update(classModel));
        }

        [HttpDelete("Deletar/{id:int}")]
        [ProducesResponseType(typeof(RequestResult), 200)]
        public IActionResult Deletar([FromRoute] int id)
        {
            return Ok(_classService.Delete(id));
        }
    }
}
