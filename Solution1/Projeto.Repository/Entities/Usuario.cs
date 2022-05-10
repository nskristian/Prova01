using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Repository.Entities
{
    public class Usuario
    {
        #region Propriedades

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        #endregion

        #region Construtores
        public Usuario()
        {

        }

        public Usuario(string nome, DateTime dataNascimento, string nomeUsuario, string email, string senha)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            NomeUsuario = nomeUsuario;
            Email = email;
            Senha = senha;
        }
        #endregion
    }
}
