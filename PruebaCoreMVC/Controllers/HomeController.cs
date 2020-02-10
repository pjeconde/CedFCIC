using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PruebaCoreMVC.Models;
using System.Data.SqlClient;

namespace PruebaCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        PruebaCore.RN.Medio dbMedio = new PruebaCore.RN.Medio();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page...";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpGet]
        public IActionResult Medios()
        {
            ViewData["Message"] = "Your medios page.";
            ViewData["Medio"] = new PruebaCore.Entidades.Medio();
            //List<Medio> resp = db.ConsultaListaMedio();
            return View();
        }

        [HttpPost]
        public IActionResult Medios([Bind] PruebaCore.Entidades.Medio medio)
        {
            try
            {
                ViewData["Medio"] = new PruebaCore.Entidades.Medio();
                if (ModelState.IsValid)
                {
                    string resp = "";
                    PruebaCore.RN.Medio rnMedio = new PruebaCore.RN.Medio();
                    PruebaCore.Entidades.Medio respMedio = rnMedio.ConsultaMedio(medio.IdMedio, out resp);
                    ViewData["Medio"] = respMedio;
                    TempData["msg"] = resp;
                }
                else
                {
                    //TempData["msg"] = "Ingrese los datos requeridos";
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View();
        }

        public IActionResult MediosInsertar()
        {
            ViewData["Message"] = "Your medios insert page.";
            ViewData["Medio"] = new PruebaCore.Entidades.Medio();
            return View();
        }

        [HttpPost]
        public IActionResult MediosInsertar([Bind] PruebaCore.Entidades.Medio medio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = "";
                    PruebaCore.RN.Medio rnMedio = new PruebaCore.RN.Medio();
                    resp = rnMedio.InsertarMedio(medio);
                    TempData["msg"] = resp;
                }
                else
                {
                    //TempData["msg"] = "Ingrese los datos requeridos";
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult MediosLista()
        {
            ViewData["Message"] = "Your medios lista page.";
            PruebaCore.RN.Medio rnMedio = new PruebaCore.RN.Medio();
            ViewData["Medios"] = rnMedio.ConsultaListaMedio();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
