using CedFCIC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedFCIC.Models
{
    public class PagedResult<T, T2> : PagedResultBase where T : class 
    {
        public List<T> Lista { get; set; }
        public T2 Filtro { get; set; }
        public PagedResult()
        {
            Lista = new List<T>();
        }
    }
}
