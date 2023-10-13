using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StudentClass.Domain.Models;
using StudentClass.Domain.Models.Requests;
using StudentClass.UI.Models;
using System.Net.Http.Headers;

namespace StudentClass.Controllers
{
    public class RelateClassController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<Database> _data;

        public RelateClassController(IHttpClientFactory httpClient, IOptions<Database> data)
        {
            _data = data;
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index(string? mensagem, bool sucesso = true)
        {
            if (sucesso)
                TempData["success"] = mensagem;
            else
                TempData["error"] = mensagem;

            HttpResponseMessage response = await _httpClient.GetAsync($"{_data.Value.API_URL_BASE}RelateClass/Obter-Todos");


            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<RelateClassResponse>(await response.Content.ReadAsStringAsync());

                return View(result?.Dados);
            }
            else
                throw new Exception(response.ReasonPhrase);

        }

        public async Task<IActionResult> Details(int idStudent, int idClass)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_data.Value.API_URL_BASE}RelateClass/Obter/Aluno/{idStudent}/Turma/{idClass}");

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<RelateClassRequest>(await response.Content.ReadAsStringAsync()));
            else
                throw new Exception(response.ReasonPhrase);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Students = await ListStudents();
            ViewBag.Class = await ListClass();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] RelateClassRequest relateClass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_data.Value.API_URL_BASE}RelateClass/Incluir", relateClass);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Associação de Aluno e Turma realizado com sucesso!", sucesso = true });
                    else
                        throw new Exception(response.ReasonPhrase);
                }
                else
                {
                    TempData["error"] = "Algum campo está faltando ser selecionado.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Algum erro aconteceu - " + ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Edit(int idStudent, int idClass)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_data.Value.API_URL_BASE}RelateClass/Obter/Aluno/{idStudent}/Turma/{idClass}");

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<RelateClassRequest>(await response.Content.ReadAsStringAsync()));
            else
                throw new Exception(response.ReasonPhrase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] RelateClassRequest relateClass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_data.Value.API_URL_BASE}RelateClass/Atualizar", relateClass);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Alterações feitas com sucesso!", sucesso = true });
                    else
                        throw new Exception(response.ReasonPhrase);
                }
                else
                {
                    TempData["error"] = "Algum campo está faltando ser selecionado.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Algum erro aconteceu - " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Route("Delete/{idstudent}/{idclass}")]
        public async Task<IActionResult> Delete(int idstudent, int idclass)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_data.Value.API_URL_BASE}RelateClass/Deletar/Aluno/{idstudent}/Turma/{idclass}");

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index), new { mensagem = "Exclusão realizada com sucesso!", sucesso = true });
                else
                    throw new Exception(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Algum erro aconteceu - " + ex.Message;
                return View();
            }
        }

        private async Task<List<SelectListItem>> ListStudents()
        {
            List<SelectListItem> lista = new();

            HttpResponseMessage response = await _httpClient.GetAsync($"{_data.Value.API_URL_BASE}Student/Obter-Todos");

            if (response.IsSuccessStatusCode)
            {
                var resultStudent = JsonConvert.DeserializeObject<StudentResponse>(await response.Content.ReadAsStringAsync());

                foreach (var linha in resultStudent.Dados)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.Id.ToString(),
                        Text = linha.Nome,
                        Selected = false,
                    });
                }

                return lista;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        private async Task<List<SelectListItem>> ListClass()
        {
            List<SelectListItem> lista = new();

            HttpResponseMessage response = await _httpClient.GetAsync($"{_data.Value.API_URL_BASE}Class/Obter-Todos");

            if (response.IsSuccessStatusCode)
            {
                var resultClass = JsonConvert.DeserializeObject<ClassResponse>(await response.Content.ReadAsStringAsync());

                foreach (var linha in resultClass.Dados)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.Id.ToString(),
                        Text = linha.Turma,
                        Selected = false,
                    });
                }

                return lista;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
