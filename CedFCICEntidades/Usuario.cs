using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedFCIC.Entidades
{
    [Serializable]
    public class Usuario : UsuarioIngresar
    {
        private string nombre;
        private string telefono;
        private string email;
        private string pregunta;
        private string respuesta;
        private int cantidadEnviosMail;
        private DateTime fechaUltimoReenvioMail;
        private string emailSMS;
        private WF wF;
        private string ultActualiz;
        private List<Permiso> permisos;
        private string cuitPredef;
        private int idUNPredef;
        private string fechaOKeFactTyC;
        private int cantidadFilasXPagina = 10;
        private bool mostrarAyudaComoPaginaDefault;
        private string token;
        public Usuario()
        {
            wF = new WF();
            permisos = new List<Permiso>();
        }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "La longitud máxima del nombre es de 50 caracteres")]
        [Required(ErrorMessage = "La identificación del nombre es obligatoria")]
        [MinLength(4, ErrorMessage = "La longitud mínima del nombre debe ser de 4 caracteres")]
        public string Nombre
        {
            set
            {
                nombre = value;
            }
            get
            {
                return nombre;
            }
        }
        public string Telefono
        {
            set
            {
                telefono = value;
            }
            get
            {
                return telefono;
            }
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
        [Display(Name = "Pregunta")]
        [MaxLength(60, ErrorMessage = "La longitud máxima del nombre es de 60 caracteres")]
        [Required(ErrorMessage = "La pregunta es obligatoria")]
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
        [Display(Name = "Respuesta")]
        [MaxLength(60, ErrorMessage = "La longitud máxima del nombre es de 60 caracteres")]
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
        public int CantidadEnviosMail
        {
            set
            {
                cantidadEnviosMail = value;
            }
            get
            {
                return cantidadEnviosMail;
            }
        }
        public DateTime FechaUltimoReenvioMail
        {
            set
            {
                fechaUltimoReenvioMail = value;
            }
            get
            {
                return fechaUltimoReenvioMail;
            }
        }
        public string EmailSMS
        {
            set
            {
                emailSMS = value;
            }
            get
            {
                return emailSMS;
            }
        }
        public WF WF
        {
            set
            {
                wF = value;
            }
            get
            {
                return wF;
            }
        }
        public string UltActualiz
        {
            set
            {
                ultActualiz = value;
            }
            get
            {
                return ultActualiz;
            }
        }
        public List<Permiso> Permisos
        {
            set
            {
                permisos = value;
            }
            get
            {
                return permisos;
            }
        }
        public string CuitPredef
        {
            set
            {
                cuitPredef = value;
            }
            get
            {
                return cuitPredef;
            }
        }
        public int IdUNPredef
        {
            set
            {
                idUNPredef = value;
            }
            get
            {
                return idUNPredef;
            }
        }
        public string FechaOKeFactTyC
        {
            set
            {
                fechaOKeFactTyC = value;
            }
            get
            {
                return fechaOKeFactTyC;
            }
        }
        public int CantidadFilasXPagina
        {
            set
            {
                cantidadFilasXPagina = value;
            }
            get
            {
                return cantidadFilasXPagina;
            }
        }
        public bool MostrarAyudaComoPaginaDefault
        {
            set
            {
                mostrarAyudaComoPaginaDefault = value;
            }
            get
            {
                return mostrarAyudaComoPaginaDefault;
            }
        }
        public string Token
        {
            set
            {
                token = value;
            }
            get
            {
                return token;
            }
        }
        public string PaginaDefault (Entidades.Sesion Sesion)
        {
            Sesion.EstoyEnAyuda = false;
            if (!mostrarAyudaComoPaginaDefault)
                return "~/Factura/Index";
            else
                return "~/Factura/Ayuda/Instructivas/OperarFacturaElectronica001";
        }

        #region Propiedades redundantes
        public string Estado
        {
            get
            {
                return wF.Estado;
            }
        }
        public string EstadoId
        {
            set
            {
                wF.Estado = value;
            }
            get
            {
                return wF.Estado;
            }
        }
        #endregion
    }
}
