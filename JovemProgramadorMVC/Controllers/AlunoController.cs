using JovemProgramadorMVC.Data.Repositorio.Interfaces;
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
    public class AlunoController: Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAlunoRepositorio _alunorepositorio;
        public AlunoController(IConfiguration configuration, IAlunoRepositorio alunoRepositorio)
        {
            _configuration = configuration;
            _alunorepositorio = alunoRepositorio;
        }
        public IActionResult Index(AlunoModel filtroAluno)
        {
            List<AlunoModel> aluno = new();
            if (filtroAluno.Idade > 0)
            {
                return View(_alunorepositorio.FiltroIdade(filtroAluno.Idade, filtroAluno.Operacao));
            }
            if(filtroAluno.Nome != null)
            {
                return View(_alunorepositorio.FiltroNome(filtroAluno.Nome));
            }
            if(filtroAluno.Contato != null)
            {
                return View(_alunorepositorio.FiltroContato(filtroAluno.Contato));
            }
            if (filtroAluno.Email != null)
            {
                return View(_alunorepositorio.FiltroEmail(filtroAluno.Email));
            }
            if (filtroAluno.Cep != null)
            {
                return View(_alunorepositorio.FiltroCep(filtroAluno.Cep));
            }

            return View(_alunorepositorio.BuscarAlunos());

        }

        public IActionResult Adicionar()
        {
            return View();
        }

        public IActionResult Mensagem()
        {
            return View();
        }

        public IActionResult MensagemEndereco()
        {
            return View();
        }

    public async Task<IActionResult> BuscarEndereco(AlunoModel aluno)
        {
            var retorno = _alunorepositorio.BuscarId(aluno.Id);
            aluno = retorno;
            EnderecoModel enderecoModel = new();

            try
            {
                var cep = aluno.Cep.Replace("-", "");

                using var client = new HttpClient();
                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");

                if (result.IsSuccessStatusCode)
                {
                    enderecoModel = JsonSerializer.Deserialize<EnderecoModel>(
                        await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });


                    if(enderecoModel.complemento == "")
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

                    enderecoModel.IdAluno = aluno.Id;
                    _alunorepositorio.InserirEndereco(enderecoModel);
                }
                else
                {
                    ViewData["Mensagem"] = "Erro em Buscar o Endereço!";
                    return View("Index");
                }
            }
            catch(Exception)
            {

            }

            return View("BuscarEndereco", enderecoModel);
        }

        [HttpPost]
        public IActionResult Inserir(AlunoModel aluno)
        {
            var retorno = _alunorepositorio.Inserir(aluno);
            if(retorno != null)
            {
                TempData["MensagemEndereco"] = "Dados Gravados com Sucesso!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult BuscarAlunos(AlunoModel aluno)
        {
            _alunorepositorio.BuscarAlunos();
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var aluno = _alunorepositorio.BuscarId(id);
            return View("Editar", aluno);
        }

        public IActionResult Atualizar(AlunoModel aluno)
        {
            var retorno = _alunorepositorio.Atualizar(aluno);

            return RedirectToAction("Index");
        }

        public IActionResult Excluir(int id)
        {
            var retorno = _alunorepositorio.Excluir(id);
            if(retorno == true)
            {
                TempData["MensagemExcluir"] = "Dados Excluido com Sucesso!";
            }
            else
            {
                TempData["MensagemNaoExcluir"] = "Erro em Excluir Dados do Aluno!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Filtros()
        {
            return View();
        }

    }
}
