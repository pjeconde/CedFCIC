using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedFCIC.Entidades
{
    [Serializable]
    public class UsuarioCrear : Usuario
    {
        private string id;
        private string password;
        private string confirmacionPassword;
        public UsuarioCrear() 
        {
        }
        [Display(Name = "Confirmación de clave")]
        [MaxLength(50, ErrorMessage = "La longitud máxima de la confirmación de la clave es de 50 caracteres")]
        [Required(ErrorMessage = "La confirmación de la clave es obligatoria")]
        [MinLength(6, ErrorMessage = "La longitud mínima de la confirmación de la clave debe ser de 6 caracteres")]
        [DataType(DataType.Password)]
        public string ConfirmacionPassword
        {
            set
            {
                confirmacionPassword = value;
            }
            get
            {
                return confirmacionPassword;
            }
        }
    }
}
