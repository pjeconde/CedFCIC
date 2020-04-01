using System;
using System.Collections.Generic;
using System.Text;

namespace CedFCIC.Entidades
{

    public class StockXArticulo
    {
        string cuit;
        string razSoc;
        string periodoDsd;
        string periodoHst;

        List<CedFCIC.Entidades.StockXArticuloDetalle> stockXArticuloXArticuloDetalle;

        public StockXArticulo()
        {
        }
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
        public string RazSoc
        {
            set
            {
                razSoc = value;
            }
            get
            {
                return razSoc;
            }
        }
        public string PeriodoDsd
        {
            set
            {
                periodoDsd = value;
            }
            get
            {
                return periodoDsd;
            }
        }
        public string PeriodoHst
        {
            set
            {
                periodoHst = value;
            }
            get
            {
                return periodoHst;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("StockXArticuloDetalle")]
        public List<CedFCIC.Entidades.StockXArticuloDetalle> StockXArticuloDetalle
        {
            set
            {
                stockXArticuloXArticuloDetalle = value;
            }
            get
            {
                return stockXArticuloXArticuloDetalle;
            }
        }
    }
}
