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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CedFCIC.Controllers
{
    public class ListaPrecioController : FCICController
    {
        private readonly reCAPTCHAService _ReCAPTCHAService;
        //private readonly RequestHandler _RequestHandler;
        private readonly IWebHostEnvironment _env;

        public ListaPrecioController(IConfiguration configuracion, RequestHandler requesthandler, reCAPTCHAService _reCAPTCHAService, IWebHostEnvironment env) : base(configuracion, requesthandler)
        {
            _ReCAPTCHAService = _reCAPTCHAService;
            //_RequestHandler = requestHandler;
            _env = env;
        }

        // GET: Articulos
        public async Task<IActionResult> Index()
        {
            List<CedFCIC.Entidades.ListaPrecio> listaPrecio = new List<CedFCIC.Entidades.ListaPrecio>();
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (Funciones.SessionOK(sesion))
            {
                _RequestHandler.HandleAboutRequest();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                listaPrecio = RN.ListaPrecio.ListaPorCuityDescr(sesion.Cuit.Nro, "", sesion);
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View(listaPrecio);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            CedFCIC.Entidades.ListaPrecio listaPrecio = new CedFCIC.Entidades.ListaPrecio();
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (Funciones.SessionOK(sesion))
            {
                _RequestHandler.HandleAboutRequest();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                ViewData["Message"] = "";
                listaPrecio.Cuit = sesion.Cuit.Nro;
                CompletarComboEstado("Vigente");
                CompletarComboTipoListaPrecio("Venta");
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View(listaPrecio);
        }

        [HttpPost]
        public IActionResult Crear([Bind("Cuit,Id,Descr,Orden,IdTipo,WF.Estado")] CedFCIC.Entidades.ListaPrecio listaprecio)
        {
            try
            {
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    if (ModelState.IsValid)
                    {
                        ViewBag.Boton1ReadOnly = "disabled";
                        RN.ListaPrecio.Crear(listaprecio, sesion);
                        string resp = "Lista de precios creada satisfactoriamente.";
                        ViewData["TextoMensaje"] = resp;

                    }
                    else
                    {
                        ViewBag.Boton1ReadOnly = "";
                        ViewData["Ex"] = "Ingrese los datos requeridos";
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
                ViewBag.Boton1ReadOnly = "";
                ViewData["Ex"] = ex.Message;
            }
            CompletarComboEstado(listaprecio.WF.Estado);
            CompletarComboTipoListaPrecio(listaprecio.IdTipo);
            return View(listaprecio);
        }
        private void CompletarComboEstado(string valor)
        {
            List<CedFCIC.Entidades.Estado> estadoLista = new List<CedFCIC.Entidades.Estado>();
            estadoLista.Add(new Estado("Vigente", "Vigente", true));
            ViewData["Estados"] = new SelectList(estadoLista, "Id", "Descr", valor);
        }
        private void CompletarComboTipoListaPrecio(string valor)
        {
            ViewData["TipoLista"] = new SelectList(RN.TipoListaPrecio.Lista(), "Id", "Descr", valor);
        }
        public async Task<IActionResult> Detalle(string cuit, string id)
        {
            if (cuit == null || id == null)
            {
                return NotFound();
            }
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            CedFCIC.Entidades.ListaPrecio listaPrecio = new CedFCIC.Entidades.ListaPrecio();
            if (Funciones.SessionOK(sesion))
            {
                _RequestHandler.HandleAboutRequest();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                listaPrecio = RN.ListaPrecio.Leer(cuit, id, sesion);
                ViewData["Message"] = "";
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View(listaPrecio);
        }
        [HttpGet]
        public IActionResult Importar()
        {
            CedFCIC.Models.ListaPrecioModel listaPrecioModel = new CedFCIC.Models.ListaPrecioModel();
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (Funciones.SessionOK(sesion))
            {
                _RequestHandler.HandleAboutRequest();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                ViewData["Message"] = "";
                listaPrecioModel.ListaPrecio.Cuit = sesion.Cuit.Nro;
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View(listaPrecioModel);
        }
        private string[] getImage(string Usuarioid)
        {
            string rootPath = _env.WebRootPath + @"\ImagenesSubidas";
            var filters = new String[] { Usuarioid + ".xls", Usuarioid + ".xlsx" };
            string[] fileLocations = Directory.GetFiles(rootPath)
                .Select(path => Path.GetFileName(path))
                .Where(fileName => filters.Any(filter => fileName.EndsWith(filter))).ToArray();
            return fileLocations;
        }
        [HttpPost]
        public async Task<IActionResult> Importar(IFormFile file, Models.ListaPrecioModel listaPrecioModel)
        {
            //Entidades.ListaPrecio listaPrecio = new Entidades.ListaPrecio();
            try
            {
                //sesionModel = (Models.SesionModel)TempData["SesionModel"];
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    TempData.Keep();
                    listaPrecioModel.ListaPrecio.Cuit = sesion.Cuit.Nro;
                    if (file == null || file.Length == 0)
                    {
                        ViewData["Ex"] = "Seleccione un archivo excel.";
                    }
                    else
                    {
                        long maxLongitud = 4194304;
                        if (file.Length >= maxLongitud‬)
                        {
                            ViewData["Ex"] = "Seleccione un archivo excel de menor tamaño. Máximo 4096 Kbytes.";
                        }
                        else
                        {
                            string ext = "";
                            string[] fileExt = file.FileName.Split(".");
                            if (fileExt.Length > 1)
                            {
                                ext = fileExt[fileExt.Length - 1];
                                if ("*xls*xlsx*".IndexOf("*" + ext + "*") >= 0)
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
            return View(listaPrecioModel);
        }
        [HttpGet]
        public IActionResult IngresarPrecio()
        {
            ViewData["Cuit"] = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (Funciones.SessionOK(sesion))
            {
                _RequestHandler.HandleAboutRequest();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                List<Entidades.ListaPrecio> listasPrecio = RN.ListaPrecio.ListaPorCuit(true, false, true, sesion);
                if (listasPrecio.Count == 0)
                {
                    ViewData["Ex"] = "No hay ninguna Lista de precios definida";
                }
                else
                {
                    dt = RN.Precio.Matriz(listasPrecio, sesion);
                    //ActualizarGrilla();
                }
                ViewData["Message"] = "";
                ViewData["Cuit"] = sesion.Cuit.Nro;
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View(dt);
        }
        //[HttpGet]
        //public IActionResult Modificar(string cuit, string id)
        //{
        //    if (cuit == null || id == null)
        //    {
        //        TempData["Ex"] = "Problemas para obtener la identificación del artículo";
        //    }
        //    Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
        //    CedFCIC.Entidades.Articulo articulo = new CedFCIC.Entidades.Articulo();
        //    if (Funciones.SessionOK(sesion))
        //    {
        //        _RequestHandler.HandleAboutRequest();
        //        sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
        //        articulo = RN.Articulo.Leer(cuit, id, true, sesion);
        //        ViewData["WFId"] = articulo.WFId;
        //        ViewData["Message"] = "";
        //        CompletarComboEstado(articulo.Estado);
        //        CompletarComboUnidad(articulo.Unidad.Id);
        //        CompletarComboIndicacionExentoGravado(articulo.IndicacionExentoGravado);
        //    }
        //    else
        //    {
        //        TempData["Ex"] = "Sesion finalizada por timeout.";
        //        return RedirectToAction("Ingresar", "Usuario");
        //    }
        //    return View(articulo);
        //}

        //[HttpPost]
        //public IActionResult Modificar([Bind("Cuit,Id,Descr,UnidadId,IndicacionExentoGravadoId,AlicuotaIVA,Stock,EstadoId,WF.Id")] CedFCIC.Entidades.Articulo articulo)
        //{
        //    try
        //    {
        //        Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
        //        if (Funciones.SessionOK(sesion))
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                List<FeaEntidades.CodigosUnidad.CodigoUnidad> listaCU = FeaEntidades.CodigosUnidad.CodigoUnidad.Lista();
        //                FeaEntidades.CodigosUnidad.CodigoUnidad CU = listaCU.Find(element => element.Codigo.Equals(Convert.ToInt16(articulo.UnidadId)));
        //                articulo.Unidad.Descr = CU.Descr;
        //                RN.Articulo.Modificar(articulo, articulo, sesion);
        //                string resp = "Artículo modificado satisfactoriamente.";
        //                ViewData["TextoMensaje"] = resp;
        //            }
        //            else
        //            {
        //                ViewData["Ex"] = "Ingrese los datos requeridos";
        //            }
        //        }
        //        else
        //        {
        //            TempData["Ex"] = "Sesion finalizada por timeout.";
        //            return RedirectToAction("Ingresar", "Usuario");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewData["Ex"] = ex.Message;
        //    }
        //    CompletarComboEstado(articulo.EstadoId);
        //    CompletarComboUnidad(articulo.UnidadId);
        //    CompletarComboIndicacionExentoGravado(articulo.IndicacionExentoGravadoId);
        //    return View(articulo);
        //}

        //[HttpGet]
        //public IActionResult Baja(string cuit, string id)
        //{
        //    if (cuit == null || id == null)
        //    {
        //        TempData["Ex"] = "Problemas para obtener la identificación del artículo";
        //    }
        //    Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
        //    CedFCIC.Entidades.Articulo articulo = new CedFCIC.Entidades.Articulo();
        //    if (Funciones.SessionOK(sesion))
        //    {
        //        _RequestHandler.HandleAboutRequest();
        //        sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
        //        articulo = RN.Articulo.Leer(cuit, id, true, sesion);
        //        ViewData["WFId"] = articulo.WFId;
        //        if (articulo.EstadoId == "Baja")
        //        {
        //            ViewData["Accion"] = "AnulBaja";
        //            ViewData["AccionDescr"] = "Anulación de Baja";
        //        }
        //        else
        //        {
        //            ViewData["Accion"] = "Baja";
        //            ViewData["AccionDescr"] = "Baja";
        //        }
        //        ViewData["Message"] = "";
        //    }
        //    else
        //    {
        //        TempData["Ex"] = "Sesion finalizada por timeout.";
        //        return RedirectToAction("Ingresar", "Usuario");
        //    }
        //    return View(articulo);
        //}
        //[HttpPost]
        //public IActionResult Baja(string accion, CedFCIC.Entidades.Articulo articulo)
        //{
        //    try
        //    {
        //        Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
        //        if (Funciones.SessionOK(sesion))
        //        {
        //            if (accion == "Baja")
        //            { 
        //                RN.Articulo.CambiarEstado(articulo, "DeBaja", sesion);
        //                string resp = "Baja del artículo realizada satisfactoriamente.";
        //                ViewData["TextoMensaje"] = resp;
        //            }
        //            else
        //            {
        //                RN.Articulo.CambiarEstado(articulo, "Vigente", sesion);
        //                string resp = "Anulación de la Baja del artículo realizada satisfactoriamente.";
        //                ViewData["TextoMensaje"] = resp;
        //            }
        //        }
        //        else
        //        {
        //            TempData["Ex"] = "Sesion finalizada por timeout.";
        //            return RedirectToAction("Ingresar", "Usuario");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewData["Ex"] = ex.Message;
        //    }
        //    return View(articulo);
        //}
    }
}
