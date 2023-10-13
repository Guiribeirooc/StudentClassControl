using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StudentClass.Domain.Models;
using StudentClass.Domain.Models.Requests;
using StudentClassUI.Models;
using System.Net.Http.Headers;

namespace StudentClass.Controllers
{
    public class RelateClassController : Controller
    {
        private readonly HttpClient _httpClient;

        public RelateClassController(IHttpClientFactory httpClient)
        {
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

            HttpResponseMessage responseClass = await _httpClient.GetAsync("https://localhost:7005/api/v1/Class/Obter-Todos");
            HttpResponseMessage responseStudent = await _httpClient.GetAsync("https://localhost:7005/api/v1/Student/Obter-Todos");

            if (responseClass.IsSuccessStatusCode && responseStudent.IsSuccessStatusCode)
            {
                var resultClass = JsonConvert.DeserializeObject<ClassResponse>(await responseClass.Content.ReadAsStringAsync());
                var resultStudent = JsonConvert.DeserializeObject<StudentResponse>(await responseStudent.Content.ReadAsStringAsync());

                StudentClassResponse studentClassResponse = new StudentClassResponse();
                studentClassResponse.Student = resultStudent?.Dados;
                studentClassResponse.Class = resultClass?.Dados;

                return View(studentClassResponse);
            }

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7005/api/v1/RelateClass/Obter-Todos");

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<List<RelateClassModel>>(await response.Content.ReadAsStringAsync()));
            else
                throw new Exception(response.ReasonPhrase);

        }

        public async Task<IActionResult> Details(int idStudent, int idClass)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7005/api/v1/RelateClass/Obter/Aluno/{idStudent}/Turma/{idClass}");

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
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7005/api/v1/RelateClass/Incluir", relateClass);

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
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7005/api/v1/RelateClass/Obter/Aluno/{idStudent}/Turma/{idClass}");

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
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync("https://localhost:7005/api/v1/RelateClass/Atualizar", relateClass);

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
        public async Task<IActionResult> Delete(int idStudent, int idClass)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7005/api/v1/RelateClass/Deletar/Aluno/{idStudent:int}/Turma/{idClass:int}");

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

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7005/api/v1/Student/Obter-Todos");

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

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7005/api/v1/Class/Obter-Todos");

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
