using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CedFCIC.Models
{
    [Serializable]
    public class ComprobanteFiltro
    {
        public ComprobanteFiltro()
        {
        }

        [Display(Name = "Fecha Desde")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Las fecha deben tener el siguiente formato dd/MM/aaaa.")]
        [Required(ErrorMessage = "El ingreso de la fecha desde es obligatoria.")]
        public string FiltroFechaDesde { get; set; }

        [Display(Name = "Fecha Hasta")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Las fecha deben tener el siguiente formato dd/MM/aaaa.")]
        [Required(ErrorMessage = "El ingreso de la fecha hasta es obligatoria.")]
        public string FiltroFechaHasta { get; set; }

        [Display(Name = "Razon Social")]
        public string FiltroRazonSocial { get; set; }
    }
}