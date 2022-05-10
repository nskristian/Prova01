using Microsoft.AspNetCore.Mvc;
using Projeto.Presentation.Mvc.Models;
using Projeto.Repository.Entities;
using Projeto.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(UsuarioCadastroModel model, [FromServices] UsuarioRepository usuarioRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = DateTime.Parse(model.DataNascimento);
                    var hoje = DateTime.Now;

                    int result = DateTime.Compare(data, hoje);

                    if (result >= 0)
                    {
                        throw new Exception("Insira uma data válida");
                    }

                    if(model.NomeUsuario.ToLower().Contains("mastercoin") || model.NomeUsuario.ToLower().Contains("mc"))
                    {
                        throw new Exception("Nome de usuário não deve conter mastercoin ou mc");
                    }

                    var usuario = new Usuario();
                    usuario.Nome = model.Nome;
                    usuario.DataNascimento = data;
                    usuario.NomeUsuario = model.NomeUsuario;
                    usuario.Email = model.Email;
                    usuario.Senha = model.Senha;

                    usuarioRepository.Create(usuario);

                    TempData["MensagemSucesso"] = "Você foi cadastrado com sucesso!";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = "Erro: " + e.Message;
                }
            }
            return View();
        }

        public IActionResult Consulta([FromServices] UsuarioRepository usuarioRepository)
        {

            var usuarios = new List<Usuario>();
            try
            {
                usuarios = usuarioRepository.GetAll();
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro: " + e.Message;
            }
            return View(usuarios);
        }
    }
}
