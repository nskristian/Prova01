using Dapper;
using Projeto.Repository.Contracts;
using Projeto.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Projeto.Repository.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string connectionString;

        public UsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(Usuario entity)
        {
            var query = "insert into Usuario(Nome, DataNascimento, NomeUsuario, Email, Senha) values(@Nome, @DataNascimento, @NomeUsuario, @Email, @Senha)";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }

        }

        public void Update(Usuario entity)
        {
            var query = "update Usuario set Nome = @Nome, DataNascimento = @DataNascimento, NomeUsuario = @NomeUsuario, Email = @Email, Senha = @Senha where IdUsuario=@IdUsuario";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Usuario entity)
        {
            var query = "delete from Usuario where IdUsuario = @IdUsuario";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Usuario> GetAll()
        {
            var query = "select * from Usuario";
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>(query).ToList();
            }
        }

        public Usuario GetById(int id)
        {
            var query = "select * from Usuario where IdUsuario = @IdUsuario";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<Usuario>(query, new { IdUsuario = id });
            }
        }
    }
}
