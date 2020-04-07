using System;
using System.ComponentModel.DataAnnotations;

namespace CedFCIC.Entidades
{
    [Serializable]
    public class ListaPrecio
    {
        private string cuit;
        private string id;
        private string descr;
        private WF wF;
        private string ultActualiz;
        private int orden;
        private string idTipo;

        public ListaPrecio()
        {
            wF = new WF();
        }
        public ListaPrecio(string IdListaPrecio, string DescrListaPrecio)
        {
            id = IdListaPrecio;
            descr = DescrListaPrecio;
            wF = new WF();
        }
        public ListaPrecio(string IdListaPrecio)
        {
            id = IdListaPrecio;
            wF = new WF();
        }

        [Display(Name = "Lista perteneciente al CUIT")]
        [MaxLength(11, ErrorMessage = "La longitud debe ser de 11 caracteres.")]
        [MinLength(11, ErrorMessage = "La longitud debe ser de 11 caracteres.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor ingrese solo un valor númerico.")]
        [Required(ErrorMessage = "El ingreso del cuit es obligatorio.")]
        public string Cuit
        {
            set
            {
                cuit = value;
            }
            get
            {
                return cuit;
            }
        }
        [Display(Name = "Identificación de la Lista")]
        [MaxLength(20, ErrorMessage = "La longitud máxima debe ser de 20 caracteres.")]
        [MinLength(3, ErrorMessage = "La longitud mínima debe ser de 3 caracteres.")]
        [Required(ErrorMessage = "El ingreso de la identificación de la Lista es obligatorio.")]
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
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El ingreso de la descripción es obligatorio.")]
        public string Descr
        {
            set
            {
                descr = value;
            }
            get
            {
                return descr;
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
        [Required(ErrorMessage = "Si usted no desea ingresar un número de orden, ingrese el valor cero.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor ingrese un valor númerico entero positivo.")]
        [Range(0, 100, ErrorMessage = "Solo se permiten valores del 0 al 100.")]
        public int Orden
        {
            set
            {
                orden = value;
            }
            get
            {
                return orden;
            }
        }
        [Display(Name = "Tipo de Lista")]
        [Required(ErrorMessage = "El ingreso del tipo de lista es obligatoria.")]
        public string IdTipo
        {
            set
            {
                idTipo = value;
            }
            get
            {
                return idTipo;
            }
        }
        #region Propiedades redundantes
        public string Estado
        {
            get
            {
                return wF.Estado;
            }
        }
        #endregion
    }
}