using CedFCIC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedFCIC.Models
{
    public class PagedResult<T> : PagedResultBase where T : class 
    {
        //public List<T> Lista { get; set; }
        //public PagedResult()
        //{
        //    Lista = new List<T>();
        //}
        public List<T> Lista { get; set; }
        public PagedResult()
        {
            //Filtros = new CedFCIC.Models.ComprobanteFiltro();
            Lista = new List<T>();
        }
    }
}
