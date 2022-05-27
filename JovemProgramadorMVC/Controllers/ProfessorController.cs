using JovemProgramadorMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JovemProgramadorMVC.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProfessorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BuscarEnderecoP(string cep)
        {
            EnderecoModel enderecoModel = new();

            try
            {
                cep = cep.Replace("-", "");

                using var client = new HttpClient();
                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");

                if (result.IsSuccessStatusCode)
                {
                    enderecoModel = JsonSerializer.Deserialize<EnderecoModel>(
                        await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });


                    if (enderecoModel.complemento == "")
                    {
                        enderecoModel.complemento = "Nenhum";
                    }

                    if (enderecoModel.logradouro == "")
                    {
                        enderecoModel.logradouro = "Cep Geral";
                    }

                    if (enderecoModel.bairro == "")
                    {
                        enderecoModel.bairro = "Nenhum";
                    }

                    if (enderecoModel.localidade == "")
                    {
                        enderecoModel.localidade = "Nenhum";
                    }

                    if (enderecoModel.uf == "")
                    {
                        enderecoModel.uf = "Nenhum";
                    }

                    if (enderecoModel.ddd == "")
                    {
                        enderecoModel.ddd = "Nenhum";
                    }

                }
                else
                {
                    ViewData["Mensagem"] = "Erro em Buscar o Endereço!";
                    return View("Index");
                }
            }
            catch (Exception)
            {

            }

            return View("BuscarEnderecoP", enderecoModel);
        }
    }
}
