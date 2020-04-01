using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CedFCIC.Models
{
    [Serializable]
    public class SesionModel
    {
        private IFormFile foto;
        private CedFCIC.Entidades.Sesion sesion;

        public SesionModel()
        {
        }
        
        [Display(Name = "Archivo Foto")]
        public IFormFile Foto
        {
            get
            {
                return foto;
            }
            set
            {
                foto = value;
            }
        }

        public CedFCIC.Entidades.Sesion Sesion
        {
            get
            {
                return sesion;
            }
            set
            {
                sesion = value;
            }
        }
        
    }
}