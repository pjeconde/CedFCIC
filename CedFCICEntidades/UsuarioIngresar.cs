using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedFCIC.Entidades
{
    [Serializable]
    public class UsuarioIngresar
    {
        private string id;
        private string password;
        public UsuarioIngresar()
        {
        }
        [Display(Name = "Id.")]
        [MaxLength(50, ErrorMessage = "La longitud máxima de la identificación del usuario es de 50 caracteres")]
        [Required(ErrorMessage = "La identificación del usuario es obligatoria")]
        [MinLength(6, ErrorMessage = "La longitud mínima de la identificación del usuario debe ser de 6 caracteres")]
        public string Id
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }
        [Display(Name = "Clave")]
        [MaxLength(50, ErrorMessage = "La longitud máxima de la clave es de 50 caracteres")]
        [Required(ErrorMessage = "El ingreso de la clave es obligatoria")]
        [MinLength(6, ErrorMessage = "La longitud mínima de la clave debe ser de 6 caracteres")]
        [DataType(DataType.Password)]
        public string Password
        {
            set
            {
                password = value;
            }
            get
            {
                return password;
            }
        }
    }
}
