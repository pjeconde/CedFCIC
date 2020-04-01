using CedFCIC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedFCIC.Models
{
    public class PagedResultComprobante<T> : PagedResultBase where T : class 
    {
        public ComprobanteFiltro Filtro { get; set; }
        public List<T> Lista { get; set; }
        public PagedResultComprobante()
        {
            //Filtros = new CedFCIC.Models.ComprobanteFiltro();
            Lista = new List<T>();
            Filtro = new ComprobanteFiltro();
            Filtro.FiltroFechaDesde = "";
            Filtro.FiltroFechaHasta = "";
            Filtro.FiltroRazonSocial = "";
        }
    }
}
