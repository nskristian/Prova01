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

                    if (model.NomeUsuario.ToLower().Contains("mastercoin") || model.NomeUsuario.ToLower().Contains("mc"))
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

        public IActionResult Exclusao(int id, [FromServices] UsuarioRepository usuarioRepository)
        {

            try
            {
                var usuario = usuarioRepository.GetById(id);

                if (usuario != null)
                {
                    usuarioRepository.Delete(usuario);
                    TempData["MensagemSucesso"] = "Usuário excluído com sucesso.";
                }
                else
                {
                    throw new Exception("Usuário não encontrado.");
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro: " + e.Message;
            }
            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(int id, [FromServices] UsuarioRepository usuarioRepository)
        {
            var model = new UsuarioEdicaoModel();

            try
            {
                var usuario = usuarioRepository.GetById(id);

                model.IdUsuario = usuario.IdUsuario;
                model.Nome = usuario.Nome;
                model.DataNascimento = usuario.DataNascimento.ToString("yyyy-MM-dd");
                model.NomeUsuario = usuario.NomeUsuario;
                model.Email = usuario.Email;
                model.Senha = usuario.Senha;
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(UsuarioEdicaoModel model, [FromServices] UsuarioRepository usuarioRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario();
                    usuario.IdUsuario = model.IdUsuario;
                    usuario.Nome = model.Nome;
                    usuario.DataNascimento = DateTime.Parse(model.DataNascimento);
                    usuario.NomeUsuario = model.NomeUsuario;
                    usuario.Email = model.Email;
                    usuario.Senha = model.Senha;

                    usuarioRepository.Update(usuario);
                    TempData["MensagemSucesso"] = "Usuario atualizado com sucesso.";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = "Erro: " + e.Message;
                }
                return RedirectToAction("Consulta");
            }
            return View();
        }
    }
}
