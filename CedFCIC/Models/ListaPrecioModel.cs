using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CedFCIC.Models
{
    [Serializable]
    public class ListaPrecioModel
    {
        private IFormFile archivo;
        private CedFCIC.Entidades.ListaPrecio listaPrecio;

        public ListaPrecioModel()
        {
            listaPrecio = new Entidades.ListaPrecio();
        }
        
        [Display(Name = "Archivo Excel")]
        public IFormFile Archivo
        {
            get
            {
                return archivo;
            }
            set
            {
                archivo = value;
            }
        }

        public CedFCIC.Entidades.ListaPrecio ListaPrecio
        {
            get
            {
                return listaPrecio;
            }
            set
            {
                listaPrecio = value;
            }
        }
        
    }
}