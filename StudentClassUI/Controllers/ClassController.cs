﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentClassDomain.Models;
using StudentClassUI.Models;
using System.Net.Http.Headers;

namespace StudentClassUI.Controllers
{
    public class ClassController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClassController(IHttpClientFactory httpClient)
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

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7005/api/v1/Class/Obter-Todos");

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<IEnumerable<ClassResponse>>(await response.Content.ReadAsStringAsync()));
            else
                throw new Exception(response.ReasonPhrase);

        }

        public ActionResult Details(int id)
        {
            return View();
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
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7005/api/v1/Class/Incluir", classModel);

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
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7005/api/v1/Class/Obter/{id:int}");

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<ClassModel>(await response.Content.ReadAsStringAsync()));
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
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync("https://localhost:7005/api/v1/Class/Atualizar", classModel);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7005/api/v1/Class/Delete/{id:int}");

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
