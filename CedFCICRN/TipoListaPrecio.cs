using System;
using System.Collections.Generic;
using CedFCIC;

namespace CedFCIC.RN
{
    public class TipoListaPrecio
    {
        public static List<Entidades.TipoListaPrecio> Lista()
        {
            List<CedFCIC.Entidades.TipoListaPrecio> listaTipoListaPrecio = new List<CedFCIC.Entidades.TipoListaPrecio>();
            CedFCIC.Entidades.TipoListaPrecio tipoListaPrecio = new CedFCIC.Entidades.TipoListaPrecio();
            tipoListaPrecio.Id = "Compra"; tipoListaPrecio.Descr = "Compra"; listaTipoListaPrecio.Add(tipoListaPrecio);
            tipoListaPrecio = new CedFCIC.Entidades.TipoListaPrecio();
            tipoListaPrecio.Id = "Venta"; tipoListaPrecio.Descr = "Venta"; listaTipoListaPrecio.Add(tipoListaPrecio);
            return listaTipoListaPrecio;
        }
    }
}
