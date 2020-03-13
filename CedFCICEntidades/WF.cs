using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedFCIC.Entidades
{
    [Serializable]
    public class WF
    {
        protected int id;
        protected string estado;

        public int Id
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
        public string Estado
        {
            set
            {
                estado = value;
            }
            get
            {
                return estado;
            }
        }
    }
}
