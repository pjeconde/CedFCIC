using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PruebaCoreMVC.ViewComponents
{
    public class MediosViewComponent : ViewComponent
    {
        public MediosViewComponent()
        {
            
        }

        public IViewComponentResult Invoke()
        {
            PruebaCore.RN.Medio rnMedios = new PruebaCore.RN.Medio();
            var medios = rnMedios.ConsultaListaMedio();
            return View(medios);
        }
    }
}
