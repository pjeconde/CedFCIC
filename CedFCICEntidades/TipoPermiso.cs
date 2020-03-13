using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedFCIC.Entidades
{
    [Serializable]
    public class TipoPermiso
    {
        private string id;
        private string descr;

        public TipoPermiso()
        {
        }

        public TipoPermiso(string IdTipoPermiso)
        {
            id = IdTipoPermiso;
        }

        public string Id
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
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
    }
}
