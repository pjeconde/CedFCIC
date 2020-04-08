using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using CedFCIC.Models;
using CedFCIC.Entidades;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace CedFCIC.Controllers
{
    public class UsuarioController : FCICController
    {
        private readonly reCAPTCHAService _ReCAPTCHAService;
        private readonly IWebHostEnvironment _env;
        public UsuarioController(IConfiguration configuration, RequestHandler requesthandler, reCAPTCHAService _reCAPTCHAService, IWebHostEnvironment env) : base(configuration, requesthandler)
        {
            _ReCAPTCHAService = _reCAPTCHAService;
            _env = env;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewData["Message"] = "";
            CompletarComboEstadoCrear("PteConf");
            return View();
        }

        [HttpPost]
        public IActionResult Crear([Bind("Id,Password,ConfirmacionPassword,Nombre,Telefono,Email,Pregunta,Respuesta,EstadoId,Token")] CedFCIC.Entidades.UsuarioCrear usuario)
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
                        CompletarComboEstadoCrear("PteConf");
                        ViewData["Ex"] = "Usted no es un humano.";
                        return View();
                    }
                    else
                    { 
                        string resp = "Cuenta creada satisfactoriamente, verifique su correo electronico.";
                        RN.Usuario.ValidarCrear(usuario, sesion);
                        RN.Usuario.Registrar(usuario, true, sesion);
                        TempData["TextoMensaje"] = resp;
                        return RedirectToAction("Ingresar", "Usuario");
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
            CompletarComboEstadoCrear("PteConf");
            return View();
        }
        [HttpGet]
        public IActionResult Confirmar()
        {
            try
            {
                ViewData["ConfirmarRespuesta"] = "";
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (sesion == null)
                {
                    CrearSesion();
                    sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                }
                ViewData["Message"] = "";
                string a = HttpContext.Request.QueryString.ToString();
                if (a == String.Empty)
                {
                    throw new EX.Usuario.UsuarioConfFormatoMsgErroneo();
                }
                else
                {
                    if (a.Substring(0, 4) == "?Id=")
                    {
                        a = a.Substring(4);
                    }
                    else
                    {
                        throw new EX.Usuario.UsuarioConfFormatoMsgErroneo();
                    }
                    string idUsuario = a;
                    Entidades.Usuario usuario = new Entidades.Usuario();
                    usuario.Id = idUsuario;
                    RN.Usuario.Confirmar(usuario, true, true, sesion);
                    ViewData["ConfirmarRespuesta"] = "Felicitaciones !!!.<br/>br/>Su nueva cuenta '" + usuario.Id + "' ya está disponible.<br/>Para ingresar a la aplicación, haga click en 'Ingresar'";
                }
            }
            catch (Exception ex)
            {
                ViewData["Ex"] = ex.Message;
            }
    
            return View();
        }
        private void CompletarComboEstado(string valor)
        {
            List<CedFCIC.Entidades.Estado> estadoLista = new List<CedFCIC.Entidades.Estado>();
            estadoLista.Add(new Estado("Vigente", "Vigente", true));
            ViewData["EstadoId"] = new SelectList(estadoLista, "Id", "Descr", valor);
        }
        private void CompletarComboEstadoCrear(string valor)
        {
            List<CedFCIC.Entidades.Estado> estadoLista = new List<CedFCIC.Entidades.Estado>();
            estadoLista.Add(new Estado("PteConf", "Pendiente de Confirmación", true));
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
            else if (TempData["TextoMensaje"] != null)
            {
                ViewData["TextoMensaje"] = TempData["TextoMensaje"];
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
                        RN.Sesion.RefrescarDatosUsuario(usuario, sesion);
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
                    //if (ex.StackTrace.ToString() != null)
                    //{
                    //    ViewData["Ex"] = ViewData["Ex"] + ex.StackTrace.ToString();
                    //}
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
                    TempData["Ex"] = "Sesion finalizada por timeout.";
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
        public IActionResult OlvidoClave([Bind("Id,Email,Pregunta,Respuesta,PasswordNueva,PasswordConfirmacion")] CedFCIC.Entidades.UsuarioOlvidoClave usuario)
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
                    ModelState.Remove("Respuesta");
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
                        RN.Usuario.CambiarPassword(usr, usr.Password, usuario.PasswordNueva, usuario.PasswordConfirmacion, sesion);
                        ViewBag.Boton1ReadOnly = "disabled";
                        ViewBag.Boton2ReadOnly = "disabled";
                        ViewBag.Boton3ReadOnly = "disabled";
                        HttpContext.Session.Remove("OlvidoClavePaso");
                        ViewData["TextoMensaje"] = "Clave modificada satisfactoriamente.";
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
        [HttpGet]
        public IActionResult CambiarClave()
        {
            ModelState.Remove("Email");
            ModelState.Remove("Respuesta");
            ViewBag.Boton1ReadOnly = "";
            return View();
        }
        [HttpPost]
        public IActionResult CambiarClave([Bind("Id,Password,PasswordNueva,PasswordConfirmacion")] CedFCIC.Entidades.UsuarioOlvidoClave usuario)
        {
            try
            {
                ViewBag.Boton1ReadOnly = "disabled";
                ModelState.Remove("Email");
                ModelState.Remove("Respuesta");
                if (ModelState.IsValid)
                {
                    Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                    CedFCIC.Entidades.Usuario usr = new CedFCIC.Entidades.Usuario();
                    usr.Id = usuario.Id;
                    RN.Usuario.Leer(usr, sesion);
                    RN.Usuario.CambiarPassword(usr, usuario.Password, usuario.PasswordNueva, usuario.PasswordConfirmacion, sesion);
                    ViewBag.Boton1ReadOnly = "disabled";
                    TempData["TextoMensaje"] = "Clave modificada satisfactoriamente.";
                    return RedirectToAction("Ingresar", "Usuario");
                }
            }
            catch (EX.Validaciones.ElementoInexistente)
            {
                ViewBag.Boton1ReadOnly = "";
                ViewData["Ex"] = "No hay ninguna cuenta con el Id.Usuario solicitado.";
            }
            catch (Exception ex)
            {
                ViewBag.Boton1ReadOnly = "";
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
        public IActionResult Permisos()
        {
            Entidades.Sesion sesion = new Entidades.Sesion();
            try
            {
                CedFCIC.Entidades.Usuario usuario = new CedFCIC.Entidades.Usuario();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (!Funciones.SessionOK(sesion))
                {
                    TempData["Ex"] = "Sesion finalizada por timeout.";
                    return RedirectToAction("Ingresar", "Usuario");
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
            return View(sesion);
        }
        [HttpGet]
        public IActionResult Configurar()
        {
            Models.SesionModel sesionModel = new Models.SesionModel();
            try
            {
                CedFCIC.Entidades.Sesion sesion = new CedFCIC.Entidades.Sesion();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (!Funciones.SessionOK(sesion))
                {
                    TempData["Ex"] = "Sesion finalizada por timeout.";
                    return RedirectToAction("Ingresar", "Usuario");
                }
                TempData["UsuarioImagenes"] = getImage(sesion.Usuario.Id);
                sesionModel.Sesion = sesion;
                TempData.Keep();
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
            return View(sesionModel);
        }
        private string[] getImage(string Usuarioid)
        {
            string rootPath = _env.WebRootPath + @"\ImagenesSubidas";
            var filters = new String[] { Usuarioid + ".jpg", Usuarioid + ".jpeg", Usuarioid + ".gif" };
            string[] fileLocations = Directory.GetFiles(rootPath)
                .Select(path => Path.GetFileName(path))
                .Where(fileName => filters.Any(filter => fileName.EndsWith(filter))).ToArray();
            return fileLocations;
        }
        [HttpPost]
        public async Task<IActionResult> ConfigurarUpload(IFormFile file)
        {
            Models.SesionModel sesionModel = new Models.SesionModel();
            try
            {
                //sesionModel = (Models.SesionModel)TempData["SesionModel"];
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    TempData.Keep();
                    sesionModel.Sesion = sesion;
                    if (file == null || file.Length == 0)
                    {
                        ViewData["Ex"] = "Seleccione una imagen.";
                    }
                    else
                    {
                        long maxLongitud = 4048576;
                        if (file.Length >= maxLongitud‬)
                        {
                            ViewData["Ex"] = "Seleccione una imagen de menor tamaño. Máximo 1024 Kbytes.";
                        }
                        else
                        {
                            string ext = "";
                            string[] fileExt = file.FileName.Split(".");
                            if (fileExt.Length > 1)
                            {
                                ext = fileExt[fileExt.Length - 1];
                                if ("*jpg*gif*".IndexOf("*" + ext + "*") >= 0)
                                {
                                    var filePath = _env.WebRootPath + @"\ImagenesSubidas\" + sesion.Usuario.Id + "." + ext;      //Path.GetTempFileName();
                                    using (var stream = System.IO.File.Create(filePath))
                                    {
                                        await file.CopyToAsync(stream);
                                    }
                                    //Task.Delay(3000);
                                    TempData["UsuarioImagenes"] = getImage(sesion.Usuario.Id);
                                }
                                else 
                                {
                                    ViewData["Ex"] = "El archivo de imagen debe contener una extensión válida (jpg o gif).";
                                }
                            }
                            else
                            {
                                ViewData["Ex"] = "El archivo de imagen debe contener una extensión.";
                            }
                        }
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
            return View("Configurar", sesionModel);
        }
        //[HttpPost]
        //public IActionResult ConfigurarUpload([Bind("Foto")]CedFCIC.Models.SesionModel sesionModel)
        //{
        //    try
        //    {
        //        List<IFormFile> files = new List<IFormFile>();
        //        if (sesionModel.Foto != null)
        //        {
        //            files.Add(sesionModel.Foto);
        //            long size = files.Sum(f => f.Length);
        //            foreach (var formFile in files)
        //            {
        //                if (formFile.Length > 0)
        //                {
        //                    var filePath = _env.WebRootPath + @"\ImagenesSubidas";      //Path.GetTempFileName();
        //                    using (var stream = System.IO.File.Create(filePath))
        //                    {
        //                        //await formFile.CopyToAsync(stream);
        //                        formFile.CopyTo(stream);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ViewData["Ex"] = "Seleccione una foto";
        //        }
        //        // Process uploaded files
        //        // Don't rely on or trust the FileName property without validation.
        //        //return Ok(new { count = files.Count, size });
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException == null)
        //        {
        //            ViewData["Ex"] = ex.Message;
        //        }
        //        else
        //        {
        //            ViewData["Ex"] = string.Format("{0}({1})", ex.Message, ex.InnerException.Message);
        //        }
        //        //return NotFound();
        //    }
        //    return View("Configurar", sesionModel);
        //}
        [HttpPost]
        public IActionResult Configurar(CedFCIC.Models.SesionModel sesionModel)
        {
            try
            {
                CedFCIC.Entidades.Sesion sesion = new CedFCIC.Entidades.Sesion();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    
                    //IFormFile foto = sesionModel.Foto;
                }
                else
                {
                    TempData["Ex"] = "Sesion finalizada por timeout.";
                    return RedirectToAction("Ingresar", "Usuario");
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
            return View(sesionModel);
        }
        [HttpGet]
        public IActionResult DefinirCantFilasXPagdefault(string cantFilasXPag)
        {
            Models.SesionModel sesionModel = new Models.SesionModel();
            try
            {
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    TempData.Keep();
                    sesionModel.Sesion = sesion;
                    if (!string.IsNullOrEmpty(cantFilasXPag))
                    {
                        int cantidadFilasXPagina = 0;

                        if (!int.TryParse(cantFilasXPag, out cantidadFilasXPagina) || cantidadFilasXPagina < 1 || cantidadFilasXPagina > 200)
                        {
                            ViewData["Ex"] = "Valor inválido (ingresar un valor numérico, mayor a cero y menor o igual a 200)";
                        }
                        else
                        {
                            RN.Configuracion.EstablecerCantidadFilasXPagina(cantidadFilasXPagina, sesion);
                            ViewData["TextoMensaje"] = "Cantidad de filas establecidas satisfactoriamente.";
                            HttpContext.Session.SetObj("Sesion", sesion);
                            _RequestHandler.HandleAboutRequest();
                            sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                            sesionModel.Sesion = sesion;
                        }
                    }
                }
                else
                {
                    TempData["Ex"] = "Sesion finalizada por timeout.";
                    return RedirectToAction("Ingresar", "Usuario");
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
            return View("Configurar", sesionModel);
        }
        [HttpGet]
        public IActionResult DefinirCuitUNdefault(string cuit, string idUN)
        {
            Models.SesionModel sesionModel = new Models.SesionModel();
            try
            {
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    TempData.Keep();
                    sesionModel.Sesion = sesion;
                    if (!string.IsNullOrEmpty(cuit) && !string.IsNullOrEmpty(idUN))
                    {
                        RN.Configuracion.EstablecerCUITUNpredef(cuit, Convert.ToInt32(idUN), sesion);
                        TempData["TextoMensaje"] = "Cuit/UN establecida por defecto satisfactoriamente.";
                        HttpContext.Session.SetObj("Sesion", sesion);
                        _RequestHandler.HandleAboutRequest();
                        sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                        sesionModel.Sesion = sesion;
                    }
                }
                else
                {
                    TempData["Ex"] = "Sesion finalizada por timeout.";
                    return RedirectToAction("Ingresar", "Usuario");
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
            return View("Configurar", sesionModel);
        }
        //public IActionResult CambiarCUIT(string CuitsDelUsrNro, string Param2)
        [HttpPost]
        public IActionResult CambiarCUIT(string CuitsDelUsrNro)
        {
            try
            {
                CedFCIC.Entidades.Sesion sesion = new CedFCIC.Entidades.Sesion();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    Entidades.Cuit cuit = new Entidades.Cuit();
                    cuit = sesion.CuitsDelUsuario.Find(x => x.Nro == CuitsDelUsrNro);
                    RN.Sesion.AsignarCuit(cuit, sesion);
                    HttpContext.Session.SetObj("Sesion", sesion);
                    _RequestHandler.HandleAboutRequest();
                    sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                }
                else
                {
                    TempData["Ex"] = "Sesion finalizada por timeout.";
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    TempData["Ex"] = ex.Message;
                }
                else
                {
                    TempData["Ex"] = string.Format("{0}({1})", ex.Message, ex.InnerException.Message);
                }
            }
            return RedirectToAction("Index", "Factura");
        }
        [HttpPost]
        public IActionResult CambiarUN(string UNsDelCuitId)
        {
            try
            {
                CedFCIC.Entidades.Sesion sesion = new CedFCIC.Entidades.Sesion();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    Entidades.UN un = new Entidades.UN();
                    un = sesion.Cuit.UNs.Find(x => x.Id == Convert.ToInt32(UNsDelCuitId));
                    RN.Sesion.AsignarUN(un, sesion);
                    HttpContext.Session.SetObj("Sesion", sesion);
                    _RequestHandler.HandleAboutRequest();
                    sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                }
                else
                {
                    TempData["Ex"] = "Sesion finalizada por timeout.";
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    TempData["Ex"] = ex.Message;
                }
                else
                {
                    TempData["Ex"] = string.Format("{0}({1})", ex.Message, ex.InnerException.Message);
                }
            }
            return RedirectToAction("Index", "Factura");
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
