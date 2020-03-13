using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using CedFCIC.Models;
using System.Data.SqlClient;
using CedFCIC.Entidades;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace CedFCIC.Controllers
{
    public class UsuarioController : FCICController
    {
        private readonly reCAPTCHAService _ReCAPTCHAService;
        public UsuarioController(IConfiguration configuration, RequestHandler requesthandler, reCAPTCHAService _reCAPTCHAService) : base(configuration, requesthandler)
        {
            _ReCAPTCHAService = _reCAPTCHAService;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewData["Message"] = "";
            CompletarComboEstado("Vigente");
            return View();
        }

        [HttpPost]
        public IActionResult Crear([Bind("Id,Password,Nombre,Telefono,Email,Pregunta,Respuesta,EstadoId,Token")] CedFCIC.Entidades.Usuario usuario)
        {
            try
            {
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (ModelState.IsValid)
                {
                    var captchaResp = _ReCAPTCHAService.VerifyreCAPTCHA(usuario.Token);
                    if (!captchaResp.Result.success && captchaResp.Result.score <= 0.5)
                    {
                        //ModelState.AddModelError("", "Usted no es un humano.");
                        ViewData["Ex"] = "Usted no es un humano.";
                        return View();
                    }
                    else
                    { 
                        string resp = "Cuenta creada satisfactoriamente, verifique su correo electronico.";
                        RN.Usuario.Validar(usuario, "", "", "", sesion);
                        RN.Usuario.Registrar(usuario, false, sesion);
                        ViewData["TextoMensaje"] = resp;
                    }
                }
                else
                {
                    ViewData["Ex"] = "Ingrese los datos requeridos";
                }
            }
            catch (Exception ex)
            {
                ViewData["Ex"] = ex.Message;
            }
            CompletarComboEstado("Vigente");
            return View();
        }
        private void CompletarComboEstado(string valor)
        {
            List<CedFCIC.Entidades.Estado> estadoLista = new List<CedFCIC.Entidades.Estado>();
            estadoLista.Add(new Estado("Vigente", "Vigente", true));
            ViewData["EstadoId"] = new SelectList(estadoLista, "Id", "Descr", valor);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Ingresar()
        {
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (sesion == null) CrearSesion();
            ViewData["Sesion"] = sesion;
            if (TempData["Ex"] != null)
            {
                ViewData["Ex"] = TempData["Ex"];
            }
            return View();
        }

        [HttpPost]
        public IActionResult Ingresar([Bind("Id,Password")] CedFCIC.Entidades.UsuarioIngresar usuarioIngresar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CedFCIC.Entidades.Usuario usuario = new CedFCIC.Entidades.Usuario();
                    usuario.Id = usuarioIngresar.Id;
                    usuario.Password = usuarioIngresar.Password;
                    //sesion.UsuarioDemo = false;
                    Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                    if (!Funciones.SessionOK(sesion))
                    {
                        RN.Usuario.Login(usuario, sesion);
                        sesion.Usuario = usuario;
                        HttpContext.Session.SetObj("Sesion", sesion);
                        _RequestHandler.HandleAboutRequest();
                        sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                        RN.ReporteActividad.EnviarSiCorresponde(sesion);
                        return RedirectToAction("Index", "Factura");
                    }
                    else
                    {
                        TempData["Ex"] = "Sesion finalizada por timeout.";
                        return RedirectToAction("Ingresar", "Usuario");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ViewData["Ex"] = ex.Message;
                }
                else
                {
                    ViewData["Ex"] = string.Format("{0}({1})", ex.Message, ex.InnerException.Message);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Salir()
        {
            try
            {
                CedFCIC.Entidades.Usuario usuario = new CedFCIC.Entidades.Usuario();
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    sesion.Usuario = usuario;
                    HttpContext.Session.SetObj("Sesion", sesion);
                }
                else
                {
                    ViewData["Ex"] = "Sesion finalizada por timeout.";
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ViewData["Ex"] = ex.Message;
                }
                else
                {
                    ViewData["Ex"] = string.Format("{0}({1})", ex.Message, ex.InnerException.Message);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult OlvidoId()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OlvidoId([Bind("Email")] CedFCIC.Entidades.Usuario usuario)
        {
            try
            {
                ModelState.Remove("Id");
                ModelState.Remove("Nombre");
                ModelState.Remove("Password");
                if (ModelState.IsValid)
                {
                    Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                    RN.EnvioCorreo.ReporteIdUsuarios(usuario.Email, sesion);
                    ViewData["TextoMensaje"] = "Se ha enviado, por correo electrónico, el Id.Usuario de su(s) cuenta(s).  La recepción del email puede demorar unos minutos.";
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ViewData["Ex"] = ex.Message;
                }
                else
                {
                    ViewData["Ex"] = string.Format("{0}({1})", ex.Message, ex.InnerException.Message);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult OlvidoClave()
        {
            ViewBag.Boton1ReadOnly = "";
            ViewBag.Boton2ReadOnly = "disabled";
            ViewBag.Boton3ReadOnly = "disabled";
            HttpContext.Session.SetString("OlvidoClavePaso", "1");
            return View();
        }
        [HttpPost]
        public IActionResult OlvidoClave([Bind("Id,Email,Pregunta,Respuesta,Password,PasswordNueva,PasswordConfirmacion")] CedFCIC.Entidades.Usuario usuario)
        {
            try
            {
                string Paso = HttpContext.Session.GetString("OlvidoClavePaso");
                if (Paso == "1")
                {
                    ViewBag.Boton1ReadOnly = "";
                    ViewBag.Boton2ReadOnly = "disabled";
                    ViewBag.Boton3ReadOnly = "disabled";
                    ModelState.Remove("Nombre");
                    ModelState.Remove("Password");
                    ModelState.Remove("PasswordNueva");
                    ModelState.Remove("PasswordConfirmacion");
                }
                else if (Paso == "2")
                {
                    ViewBag.Boton1ReadOnly = "disabled";
                    ViewBag.Boton2ReadOnly = "";
                    ViewBag.Boton3ReadOnly = "disabled";
                    ModelState.Remove("Nombre");
                    ModelState.Remove("Password");
                    ModelState.Remove("PasswordNueva");
                    ModelState.Remove("PasswordConfirmacion");
                }
                else if (Paso == "3")
                {
                    ViewBag.Boton1ReadOnly = "disabled";
                    ViewBag.Boton2ReadOnly = "disabled";
                    ViewBag.Boton3ReadOnly = "";
                    ModelState.Remove("Nombre");
                    ModelState.Remove("Password");
                }
                if (ModelState.IsValid)
                {
                    if (Paso == "1")
                    {
                        Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                        CedFCIC.Entidades.Usuario usr = new CedFCIC.Entidades.Usuario();
                        usr.Id = usuario.Id;
                        RN.Usuario.Leer(usr, sesion);
                        if (usuario.Email.ToLower() != usr.Email.ToLower())
                        {
                            ViewData["Ex"] = "No hay ninguna cuenta en la que el Id.Usuario y el Email ingresados estén relacionados.";
                            return View();
                        }
                        ViewBag.Boton1ReadOnly = "disabled";
                        ViewBag.Boton2ReadOnly = "";
                        ViewBag.Boton3ReadOnly = "disabled";
                        ViewBag.Pregunta = usr.Pregunta;
                        HttpContext.Session.SetString("OlvidoClavePregunta", usr.Pregunta);
                        HttpContext.Session.SetString("OlvidoClaveRespuesta", usr.Respuesta);
                        HttpContext.Session.SetString("OlvidoClavePaso", "2");
                    }
                    else if (Paso == "2")
                    {
                        ViewBag.Pregunta = HttpContext.Session.GetString("OlvidoClavePregunta");
                        string Respuesta = HttpContext.Session.GetString("OlvidoClaveRespuesta");
                        if (usuario.Respuesta == null || usuario.Respuesta.Trim().ToLower() != Respuesta.ToString().Trim().ToLower())
                        {
                            ViewData["Ex"] = "Respuesta incorrecta.";
                            return View();
                        }
                        ViewBag.Boton1ReadOnly = "disabled";
                        ViewBag.Boton2ReadOnly = "disabled";
                        ViewBag.Boton3ReadOnly = "";
                        HttpContext.Session.SetString("OlvidoClavePaso", "3");
                    }
                    else if (Paso == "3")
                    {
                        Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                        CedFCIC.Entidades.Usuario usr = new CedFCIC.Entidades.Usuario();
                        usr.Id = usuario.Id;
                        RN.Usuario.Leer(usr, sesion);
                        usr.Password = usuario.Password + "X";
                        RN.Usuario.CambiarPassword(usr, usr.Password, usuario.Password, "", sesion);
                        if (usuario.Respuesta.Trim().ToLower() != ViewData["Respuesta"].ToString().Trim().ToLower())
                        {
                            ViewData["Ex"] = "Respuesta incorrecta.";
                            return View();
                        }
                        ViewBag.Boton1ReadOnly = "disabled";
                        ViewBag.Boton2ReadOnly = "disabled";
                        ViewBag.Boton3ReadOnly = "disabled";
                        HttpContext.Session.Remove("OlvidoClavePaso");
                    }
                }
            }
            catch (EX.Validaciones.ElementoInexistente)
            {
                ViewData["Ex"] = "No hay ninguna cuenta con el Id.Usuario solicitado.";
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ViewData["Ex"] = ex.Message;
                }
                else
                {
                    ViewData["Ex"] = string.Format("{0}({1})", ex.Message, ex.InnerException.Message);
                }
            }
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
            HttpContext.Session.Set("UsuarioId", System.Text.UTF8Encoding.UTF8.GetBytes(Environment.UserName));
            HttpContext.Session.SetObj("Sesion", s);
            return s;
        }
    }
}
