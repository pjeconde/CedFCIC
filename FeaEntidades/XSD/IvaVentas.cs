using System;
using System.Collections.Generic;
using System.Text;

namespace CedFCIC.Entidades
{
    public class IvaVentas
    {
        string cuit;
        string periodoDsd;
        string periodoHst;
        
        List<CedFCIC.Entidades.IvaVentasComprobantes> ivaVentasComprobantes;
        List<CedFCIC.Entidades.IvaVentasTotXImpuestos> ivaVentasTotXImpuestos;
        List<CedFCIC.Entidades.IvaVentasTotXIVA> ivaVentasTotXIVA;

        public IvaVentas()
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
        [System.Xml.Serialization.XmlElementAttribute("IvaVentasComprobantes")]
        public List<CedFCIC.Entidades.IvaVentasComprobantes> IvaVentasComprobantes
        {
            set
            {
                ivaVentasComprobantes = value;
            }
            get
            {
                return ivaVentasComprobantes;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("IvaVentasTotXImpuestos")]
        public List<CedFCIC.Entidades.IvaVentasTotXImpuestos> IvaVentasTotXImpuestos
        {
            set
            {
                ivaVentasTotXImpuestos = value;
            }
            get
            {
                return ivaVentasTotXImpuestos;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("IvaVentasTotXIVA")]
        public List<CedFCIC.Entidades.IvaVentasTotXIVA> IvaVentasTotXIVA
        {
            set
            {
                ivaVentasTotXIVA = value;
            }
            get
            {
                return ivaVentasTotXIVA;
            }
        }
    }
}
