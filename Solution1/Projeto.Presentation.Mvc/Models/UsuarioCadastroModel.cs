using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Models
{
    public class UsuarioCadastroModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage ="Por favor informe o nome completo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor informe a data de nascimento")]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "Por favor informe o nome de usuario")]
        public string NomeUsuario { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor informe o e-mail")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Senha deve Conter no pelo menos 6 caracteres.")]
        [RegularExpression("^(?=.*?[A-Za-z])(?=(.*[A-Za-z]){1,})(?=(.*[0-9]){1,})(?=(.*[#?!@$%^&*]){1,})(?!.*A-Za-z0-9#?!@$%^&*).{6,}$", ErrorMessage = "Senha deve Conter no pelo menos um caracter especial e um número.")]
        [Required(ErrorMessage = "Por favor informe a senha")]
        public string Senha { get; set; }
    }
}
