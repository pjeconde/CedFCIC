using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedFCIC.Entidades
{
    public class Medio
    {
        [Required]
        public string IdMedio { set; get; }
        public string DescrMedio { set; get; }
    }
}
