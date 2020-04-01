using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedFCIC.Entidades
{
    [Serializable]
    public class UsuarioOlvidoClave : UsuarioIngresar
    {
        private string email;
        private string pregunta;
        private string respuesta;
        private string passwordNueva;
        private string passwordConfirmacion;
        public UsuarioOlvidoClave()
        {
        }
        [Required(ErrorMessage = "El email es obligatorio"), DataType(DataType.EmailAddress)]
        [MaxLength(128, ErrorMessage = "La longitud máxima del email es de 128 caracteres")]
        public string Email
        {
            set
            {
                email = value;
            }
            get
            {
                return email;
            }
        }
        public string Pregunta
        {
            set
            {
                pregunta = value;
            }
            get
            {
                return pregunta;
            }
        }
        [Required(ErrorMessage = "La respuesta de la pregunta es obligatoria")]
        public string Respuesta
        {
            set
            {
                respuesta = value;
            }
            get
            {
                return respuesta;
            }
        }
        [Display(Name = "Nueva Clave")]
        [MaxLength(50, ErrorMessage = "La longitud máxima de la nueva clave es de 50 caracteres")]
        [Required(ErrorMessage = "La nueva clave es obligatoria")]
        [MinLength(6, ErrorMessage = "La longitud mínima de la nueva clave debe ser de 6 caracteres")]
        [DataType(DataType.Password)]
        public string PasswordNueva
        {
            set
            {
                passwordNueva = value;
            }
            get
            {
                return passwordNueva;
            }
        }
        [Display(Name = "Confirmación Nueva Clave")]
        [MaxLength(50, ErrorMessage = "La longitud máxima de la confirmación de la nueva clave es de 50 caracteres")]
        [Required(ErrorMessage = "La confirmación de la nueva clave es obligatoria")]
        [MinLength(6, ErrorMessage = "La longitud mínima de la identificación del usuario debe ser de 6 caracteres")]
        [DataType(DataType.Password)]
        public string PasswordConfirmacion
        {
            set
            {
                passwordConfirmacion = value;
            }
            get
            {
                return passwordConfirmacion;
            }
        }
    }
}
