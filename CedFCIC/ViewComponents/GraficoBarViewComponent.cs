using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CedFCIC.ViewComponents
{
    public class GraficoBarViewComponent: ViewComponent
    {
        public GraficoBarViewComponent(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IViewComponentResult Invoke()
        {
            Random rnd = new Random();
            var sr = new List<CedFCIC.Entidades.SimpleReportViewModel>();
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            List<CedFCIC.Entidades.Medio> m = CedFCIC.RN.Medio.Lista(sesion);
            for (int i = 0; i < m.Count; i++)
            {
                sr.Add(new CedFCIC.Entidades.SimpleReportViewModel
                {
                    DimensionOne = m[i].IdMedio.ToString(),
                    Quantity = rnd.Next(10)
                });
            }
            ViewData["Message"] = "Your medios graficos page.";
            ViewData["VarX"] = "Medios";
            return View(sr);
        }
    }
}
