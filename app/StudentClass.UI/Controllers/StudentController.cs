using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentClass.Domain.Models;
using StudentClassUI.Models;
using System.Net.Http.Headers;

namespace StudentClass.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient _httpClient;

        public StudentController(IHttpClientFactory httpClient)
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

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7005/api/v1/Student/Obter-Todos");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<StudentResponse>(await response.Content.ReadAsStringAsync());
                return View(result?.Dados);
            }
            else
                throw new Exception(response.ReasonPhrase);

        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7005/api/v1/Student/Obter/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<StudentResponse>(await response.Content.ReadAsStringAsync());
                return View(result?.Dados);
            }
            else
                throw new Exception(response.ReasonPhrase);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] StudentModel student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7005/api/v1/Student/Incluir", student);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Cadastro de turma realizado!", sucesso = true });
                    else
                        throw new Exception(response.ReasonPhrase);
                }
                else
                {
                    TempData["error"] = "Algum campo está faltando ser preenchido";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Algum erro aconteceu - " + ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7005/api/v1/Student/Obter/{id}");

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<StudentModel>(await response.Content.ReadAsStringAsync()));
            else
                throw new Exception(response.ReasonPhrase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] StudentModel student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync("https://localhost:7005/api/v1/Student/Atualizar", student);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Alterações feitas com sucesso!", sucesso = true });
                    else
                        throw new Exception(response.ReasonPhrase);
                }
                else
                {
                    TempData["error"] = "Algum campo está faltando ser preenchido";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Algum erro aconteceu - " + ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7005/api/v1/Student/Deletar/{id}");

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
    }
}
