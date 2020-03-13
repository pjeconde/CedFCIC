using System;
using System.ComponentModel.DataAnnotations;

namespace CedFCIC.Entidades
{
    [Serializable]
    public class Articulo
    {
        private string cuit;
        private string id;
        private string descr;
        private string gTIN;
        private Unidad unidad;
        private string indicacionExentoGravado;
        private double alicuotaIVA;
        private WF wF;
        private string ultActualiz;
        private double stock;

        public Articulo()
        {
            unidad = new Unidad();
            wF = new WF();
        }

        [Display(Name = "Artículo perteneciente al CUIT")]
        [MaxLength(11, ErrorMessage = "La longitud debe ser de 11 caracteres.")]
        [MinLength(11, ErrorMessage = "La longitud debe ser de 11 caracteres.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor ingrese solo un valor númerico.")]
        [Required(ErrorMessage = "El ingreso del cuit es obligatorio.")]
        [Key]
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
        [Display(Name = "Id.Artículo")]
        [Required(ErrorMessage = "El ingreso de la identificación del artículo es obligatoria.")]
        [Key]
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
        [MaxLength(170, ErrorMessage = "La longitud máxima del cuit es de 11 caracteres.")]
        [RegularExpression("^([\\S\\s]{0,100})$", ErrorMessage = "Alguno de los caracteres ingresados no está permitido.")]
        [Required(ErrorMessage = "El ingreso de la descripción del artículo es obligatoria.")]
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
        [Display(Name = "GTIN")]
        [MaxLength(20, ErrorMessage = "La longitud máxima del GTIN es de 20 caracteres.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor ingrese solo un valor númerico.")]
        public string GTIN
        {
            set
            {
                gTIN = value;
            }
            get
            {
                return gTIN;
            }
        }
        public Unidad Unidad
        {
            set
            {
                unidad = value;
            }
            get
            {
                return unidad;
            }
        }
        public string IndicacionExentoGravado
        {
            set
            {
                indicacionExentoGravado = value;
            }
            get
            {
                return indicacionExentoGravado;
            }
        }
        [Display(Name = "Alícuota I.V.A.")]
        public double AlicuotaIVA
        {
            set
            {
                alicuotaIVA = value;
            }
            get
            {
                return alicuotaIVA;
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
        [Display(Name = "Stock")]
        public double Stock
        {
            set
            {
                stock = value;
            }
            get
            {
                return stock;
            }
        }
        public string DescrConStockeIdArticulo
        {
            get
            {
                if (id == "(Elegir artículo)" || id == "(Buscar)")
                    return Descr;
                else if (stock != 0)
                    return Descr + " (id: " + id + ", stock: " + stock.ToString() + ")";
                else
                    return Descr + " (id: " + id + ")";
            }
        }
        #region Propiedades redundantes
        [Display(Name = "Unidad")]
        [Required(ErrorMessage = "El ingreso de la unidad es obligatoria.")]
        public string UnidadId
        {
            get
            {
                return unidad.Id;
            }
            set
            {
                unidad.Id = value;
            }
        }
        public string UnidadDescr
        {
            get
            {
                return unidad.Descr;
            }
        }
        public string Estado
        {
            get
            {
                return wF.Estado;
            }
        }
        [Display(Name = "EstadoId")]
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
        [Display(Name = "IndicacionExentoGravadoId")]
        [MaxLength(1, ErrorMessage = "La longitud máxima del indicador de exento/gravado es de 1 caracter.")]
        [Required(ErrorMessage = "El ingreso del indicador de exento/gravado es obligatorio.")]
        public string IndicacionExentoGravadoId
        {
            set
            {
                indicacionExentoGravado = value;
            }
            get
            {
                return indicacionExentoGravado;
            }
        }
        public int WFId
        {
            set
            {
                wF.Id = value;
            }
            get
            {
                return wF.Id;
            }
        }
        #endregion
    }
}