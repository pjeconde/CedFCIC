using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CedFCIC.Models;
using CedFCIC.Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace CedFCIC.Controllers
{
    public class ComprobanteController : FCICController
    {
        private readonly reCAPTCHAService _ReCAPTCHAService;
        //private readonly RequestHandler _RequestHandler;

        public ComprobanteController(IConfiguration configuracion, RequestHandler requesthandler, reCAPTCHAService _reCAPTCHAService) : base(configuracion, requesthandler)
        {
            _ReCAPTCHAService = _reCAPTCHAService;
            //_RequestHandler = requestHandler;
        }

        // GET: Articulos
        public async Task<IActionResult> Index()
        {
            List<CedFCIC.Entidades.Articulo> listaArticulos = new List<CedFCIC.Entidades.Articulo>();
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (Funciones.SessionOK(sesion))
            {
                _RequestHandler.HandleAboutRequest();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                listaArticulos = RN.Articulo.ListaPorCuit(false, false, sesion);
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View(listaArticulos);
        }
    }
}
