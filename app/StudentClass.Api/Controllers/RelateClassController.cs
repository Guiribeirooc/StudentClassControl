using Microsoft.AspNetCore.Mvc;
using StudentClass.Domain.Interfaces;
using StudentClass.Domain.Models;
using StudentClass.Domain.Models.Requests;

namespace StudentClassAPI.Controllers
{
    [Route("api/v1/[controller]")]
    public class RelateClassController : Controller
    {
        private readonly IRelateClassService _relateClassService;

        public RelateClassController(IRelateClassService relateClassService)
        {
            _relateClassService = relateClassService;
        }

        [HttpGet("Obter/Aluno/{idStudent:int}/Turma/{idClass:int}")]
        [ProducesResponseType(typeof(RelateClassModel), 200)]
        public IActionResult Obter([FromRoute] int idStudent, [FromRoute] int idClass)
        {
            return Ok(_relateClassService.Get(idStudent, idClass));
        }

        [HttpGet("Obter-Todos")]
        [ProducesResponseType(typeof(List<RelateClassModel>), 200)]
        public IActionResult ObterTodos()
        {
            return Ok(_relateClassService.GetAll());
        }

        [HttpPost("Incluir")]
        [ProducesResponseType(typeof(RequestResult), 200)]
        public IActionResult Incluir([FromBody] RelateClassRequest request)
        {
            return Ok(_relateClassService.Add(request));
        }

        [HttpDelete("Deletar/Aluno/{idStudent:int}/Turma/{idClass:int}")]
        [ProducesResponseType(typeof(RequestResult), 200)]
        public IActionResult Deletar([FromRoute] int idStudent, [FromRoute] int idClass)
        {
            return Ok(_relateClassService.Delete(idStudent, idClass));
        }
    }
}
