using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CedFCIC.ViewComponents
{
    public class PieViewComponent : ViewComponent
    {
        public PieViewComponent()
        {
            
        }

        public IViewComponentResult Invoke()
        {
            //List<CedFCIC.Entidades.Medio> m = new List<CedFCIC.Entidades.Medio>();
            //m = CedFCIC.RN.Medio.Lista(sesion);
            //return View(m);
            return View();
        }
    }
}
