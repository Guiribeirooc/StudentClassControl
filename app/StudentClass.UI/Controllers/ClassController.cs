using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StudentClass.Domain.Models;
using StudentClass.UI.Models;
using System.Net.Http.Headers;

namespace StudentClass.UI.Controllers
{
    public class ClassController : Controller
    {
        private readonly IOptions<Database> _data;
        private readonly HttpClient _httpClient;

        public ClassController(IHttpClientFactory httpClient, IOptions<Database> data)
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

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7005/api/v1/Class/Obter-Todos");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ClassResponse>(await response.Content.ReadAsStringAsync());
                if (result.Sucesso)
                    return View(result?.Dados);
                else
                {
                    TempData["error"] = result.Mensagem;
                    return View(result?.Dados);
                }
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
        public async Task<IActionResult> Create([FromForm] ClassModel classModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_data.Value.API_URL_BASE}Class/Incluir", classModel);

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
            HttpResponseMessage response = await _httpClient.GetAsync($"{_data.Value.API_URL_BASE}Class/Obter/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ClassModel>(await response.Content.ReadAsStringAsync());
                return View(result);
            }
            else
                throw new Exception(response.ReasonPhrase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] ClassModel classModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_data.Value.API_URL_BASE}Class/Atualizar", classModel);

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
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_data.Value.API_URL_BASE}Class/Deletar/{id}");

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
    }
}
