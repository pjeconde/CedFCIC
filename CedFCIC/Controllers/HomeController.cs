using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CedFCIC.Models;
using System.Data.SqlClient;
using CedFCIC.Entidades;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace CedFCIC.Controllers
{
    public class HomeController : FCICController
    {
        public HomeController(IConfiguration configuracion, RequestHandler requesthandler) : base(configuracion, requesthandler)
        {
        }

        public IActionResult Index()
        {
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (sesion == null) CrearSesion();
            return View();
        }

        public IActionResult Contacto()
        {
            ViewData["Message"] = "Contacto";
            return View();
        }

        public IActionResult Ayuda()
        {
            ViewData["Message"] = "";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult MediosGraficos()
        {
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            Random rnd = new Random();
            List<CedFCIC.Entidades.SimpleReportViewModel> sr = new List<CedFCIC.Entidades.SimpleReportViewModel>();
            List<CedFCIC.Entidades.Medio> m = new List<CedFCIC.Entidades.Medio>();
            m = CedFCIC.RN.Medio.Lista(sesion);
            for (int i = 0; i < m.Count; i++)
            {
                sr.Add(new CedFCIC.Entidades.SimpleReportViewModel
                {
                    DimensionOne = m[i].IdMedio.ToString(),
                    Quantity = rnd.Next(10)
                });
            }
            ViewData["Message"] = "Your medios graficos page.";
            ViewData["SRVM"] = sr;
            return View(sr);

        }
        [HttpGet]
        public IActionResult PruebaSinLayout()
        {
            ViewData["Message"] = "Your medios graficos page.";
            return View();
        }
        public Entidades.Sesion CrearSesion()
        {
            Entidades.Sesion s = new Entidades.Sesion();
            s.CnnStr = Configuration.GetConnectionString("DefaultConnection");
            s.AdministradoresSiteEmail = Configuration.GetValue<string>("AppSettings:Mantenedores");
            s.Ambiente = Configuration.GetValue<string>("AppSettings:Ambiente");
            s.Opciones = CedFCIC.RN.Sesion.Opciones(s);
            s.OpcionesHabilitadas = CedFCIC.RN.Sesion.OpcionesHabilitadas(s);
            s.Usuario = new Entidades.Usuario();
            s.URLsite = HttpContext.Request.Host.Value.ToString();  //HttpContext.Request.Path.Value.ToString();
            HttpContext.Session.Set("UsuarioId", System.Text.UTF8Encoding.UTF8.GetBytes(Environment.UserName));
            HttpContext.Session.SetObj("Sesion", s);
            return s;
        }
    }
}
