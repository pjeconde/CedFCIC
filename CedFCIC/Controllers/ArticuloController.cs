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
    public class ArticuloController : FCICController
    {
        private readonly reCAPTCHAService _ReCAPTCHAService;
        //private readonly RequestHandler _RequestHandler;

        public ArticuloController(IConfiguration configuracion, RequestHandler requesthandler, reCAPTCHAService _reCAPTCHAService) : base(configuracion, requesthandler)
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

        [HttpGet]
        public IActionResult Crear()
        {
            CedFCIC.Entidades.Articulo articulo = new CedFCIC.Entidades.Articulo();
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (Funciones.SessionOK(sesion))
            {
                _RequestHandler.HandleAboutRequest();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                ViewData["Message"] = "";
                articulo.Cuit = sesion.Cuit.Nro;
                CompletarComboEstado("Vigente");
                CompletarComboUnidad("7");
                CompletarComboIndicacionExentoGravado("G");
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View(articulo);
        }

        [HttpPost]
        public IActionResult Crear([Bind("Cuit,Id,Descr,UnidadId,IndicacionExentoGravadoId,AlicuotaIVA,Stock,EstadoId")] CedFCIC.Entidades.Articulo articulo)
        {
            try
            {
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    if (ModelState.IsValid)
                    {
                        RN.Articulo.Crear(articulo, sesion);
                        string resp = "Artículo creado satisfactoriamente.";
                        ViewData["TextoMensaje"] = resp;

                    }
                    else
                    {
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
                ViewData["Ex"] = ex.Message;
            }
            CompletarComboEstado(articulo.EstadoId);
            CompletarComboUnidad(articulo.UnidadId);
            CompletarComboIndicacionExentoGravado(articulo.IndicacionExentoGravadoId);
            return View(articulo);
        }
        private void CompletarComboEstado(string valor)
        {
            List<CedFCIC.Entidades.Estado> estadoLista = new List<CedFCIC.Entidades.Estado>();
            estadoLista.Add(new Estado("Vigente", "Vigente", true));
            ViewData["EstadoId"] = new SelectList(estadoLista, "Id", "Descr", valor);
            //List<SelectListItem> litems = new List<SelectListItem>();
            //SelectListItem item = new SelectListItem("Vigente", "V", true, false);
            //litems.Add(item);
            //ViewData["EstadoId"] = new SelectList(litems, "Value", "Text", valor);
        }
        private void CompletarComboUnidad(string valor)
        {
            List<FeaEntidades.CodigosUnidad.CodigoUnidad> unidadLista = new List<FeaEntidades.CodigosUnidad.CodigoUnidad>();
            unidadLista = FeaEntidades.CodigosUnidad.CodigoUnidad.Lista();
            ViewData["UnidadId"] = new SelectList(unidadLista, "Codigo", "Descr", valor);
        }
        private void CompletarComboIndicacionExentoGravado(string valor)
        {
            List<FeaEntidades.CodigosOperacion.CodigoOperacion> codopeLista = new List<FeaEntidades.CodigosOperacion.CodigoOperacion>();
            codopeLista = FeaEntidades.CodigosOperacion.CodigoOperacion.ListaDetalle();
            ViewData["IndicacionExentoGravadoId"] = new SelectList(codopeLista, "Codigo", "Descr", valor);
        }

        public async Task<IActionResult> Detalle(string cuit, string id)
        {
            if (cuit == null || id == null)
            {
                return NotFound();
            }
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            CedFCIC.Entidades.Articulo articulo = new CedFCIC.Entidades.Articulo();
            if (Funciones.SessionOK(sesion))
            {
                _RequestHandler.HandleAboutRequest();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                articulo = RN.Articulo.Leer(cuit, id, true, sesion);
                ViewData["Message"] = "";
                CompletarComboEstado(articulo.Estado);
                CompletarComboUnidad(articulo.Unidad.Id);
                CompletarComboIndicacionExentoGravado(articulo.IndicacionExentoGravado);
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View(articulo);
        }
        [HttpGet]
        public IActionResult Modificar(string cuit, string id)
        {
            if (cuit == null || id == null)
            {
                return NotFound();
            }
            Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            CedFCIC.Entidades.Articulo articulo = new CedFCIC.Entidades.Articulo();
            if (Funciones.SessionOK(sesion))
            {
                _RequestHandler.HandleAboutRequest();
                sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                articulo = RN.Articulo.Leer(cuit, id, true, sesion);
                ViewData["WFId"] = articulo.WFId;
                ViewData["Message"] = "";
                CompletarComboEstado(articulo.Estado);
                CompletarComboUnidad(articulo.Unidad.Id);
                CompletarComboIndicacionExentoGravado(articulo.IndicacionExentoGravado);
            }
            else
            {
                TempData["Ex"] = "Sesion finalizada por timeout.";
                return RedirectToAction("Ingresar", "Usuario");
            }
            return View(articulo);
        }

        [HttpPost]
        public IActionResult Modificar([Bind("Cuit,Id,Descr,UnidadId,IndicacionExentoGravadoId,AlicuotaIVA,Stock,EstadoId,WF.Id")] CedFCIC.Entidades.Articulo articulo)
        {
            try
            {
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    if (ModelState.IsValid)
                    {
                        List<FeaEntidades.CodigosUnidad.CodigoUnidad> listaCU = FeaEntidades.CodigosUnidad.CodigoUnidad.Lista();
                        FeaEntidades.CodigosUnidad.CodigoUnidad CU = listaCU.Find(element => element.Codigo.Equals(Convert.ToInt16(articulo.UnidadId)));
                        articulo.Unidad.Descr = CU.Descr;
                        RN.Articulo.Modificar(articulo, articulo, sesion);
                        string resp = "Artículo modificado satisfactoriamente.";
                        ViewData["TextoMensaje"] = resp;
                    }
                    else
                    {
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
                ViewData["Ex"] = ex.Message;
            }
            CompletarComboEstado(articulo.EstadoId);
            CompletarComboUnidad(articulo.UnidadId);
            CompletarComboIndicacionExentoGravado(articulo.IndicacionExentoGravadoId);
            return View();
        }

    }
}
