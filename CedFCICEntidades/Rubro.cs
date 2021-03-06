﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CedFCIC.Entidades
{
    [Serializable]
    public class Rubro
    {
        private string id;
        private string descr;

        public Rubro()
        {
        }

        public Rubro(string IdRubro)
        {
            id = IdRubro;
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
