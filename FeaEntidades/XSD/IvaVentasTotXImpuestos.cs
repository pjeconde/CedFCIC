using System;
using System.Collections.Generic;
using System.Text;

namespace CedFCIC.Entidades
{
    
    public class IvaVentasTotXImpuestos
    {
        string descr;
        double importeTotal;
        
        public IvaVentasTotXImpuestos()
        {
        }
        public string Descr
        {
            set
            {
                descr = value;
            }
            get
            {
                return descr;
            }
        }
        public double ImporteTotal
        {
            set
            {
                importeTotal = value;
            }
            get
            {
                return importeTotal;
            }
        }
    }
}
