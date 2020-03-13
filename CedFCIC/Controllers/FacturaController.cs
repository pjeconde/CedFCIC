using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CedFCIC.Controllers
{
    public class FacturaController : Controller
    {
        public IActionResult Index()
        {
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (Funciones.SessionOK(sesion))
            {
                ViewData["Usuario"] = sesion.Usuario;
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View();
        }
        public IActionResult PaginaAyudaInstOFE001()
        {
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (Funciones.SessionOK(sesion))
            {
                ViewData["Usuario"] = sesion.Usuario;
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View("Ayuda/Instructivas/OperarFacturaElectronica001");
        }
    }
}