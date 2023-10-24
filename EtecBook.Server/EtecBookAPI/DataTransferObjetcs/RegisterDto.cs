using System.ComponentModel.DataAnnotations;

namespace EtecBookAPI.DataTransferObjetcs;

    public class RegisterDto
    {
        [Required(ErrorMessage = "Informe o nome do Usuário")]
        [StringLength(60, ErrorMessage = "O nome deve possuir no máximo 60 caracteres")]
        public string Name { get; set; }   

        [Required(ErrorMessage = "Informe o Email do Usuário")]
        [EmailAddress(ErrorMessage = "Informe um Email válido")]
        [StringLength(100, ErrorMessage = "O email deve possuir no máximo 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Infome a senha de acesso")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve possuir no minímo 6 caracteres e no máximo 20.")]
        public string Password { get; set; }
    }
