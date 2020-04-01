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
            List<CedFCIC.Entidades.Comprobante> listaComprobantes = new List<CedFCIC.Entidades.Comprobante>();
            CedFCIC.Models.PagedResultComprobante<CedFCIC.Entidades.Comprobante> PagedResult = new CedFCIC.Models.PagedResultComprobante<CedFCIC.Entidades.Comprobante>();
            try
            {
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    if (HttpContext.Request.Query["page"].ToString() != "")
                    {
                        int cantidadFilas = 0;
                        PagedResult.PageSize = sesion.Usuario.CantidadFilasXPagina;
                        PagedResult.CurrentPage = Convert.ToInt32(HttpContext.Request.Query["page"].ToString());
                        List<CedFCIC.Entidades.Estado> estadosVentas = new List<CedFCIC.Entidades.Estado>();
                        List<CedFCIC.Entidades.Estado> estadosCompras = new List<CedFCIC.Entidades.Estado>();
                        List<FeaEntidades.TiposDeComprobantes.TipoComprobante> tiposComprobantes = new List<FeaEntidades.TiposDeComprobantes.TipoComprobante>();
                        CedFCIC.Entidades.Persona persona = new CedFCIC.Entidades.Persona();
                        CedFCIC.Entidades.NaturalezaComprobante naturalezaComprobante = new CedFCIC.Entidades.NaturalezaComprobante();
                        naturalezaComprobante.Id = "Venta";
                        naturalezaComprobante.Descr = "Venta";
                        string fecDsd = Convert.ToDateTime(TempData["FiltroFechaDesde"]).ToString("yyyyMMdd");
                        string fecHst = Convert.ToDateTime(TempData["FiltroFechaHasta"]).ToString("yyyyMMdd");
                        if (TempData["FiltroRazonSocial"] != null)
                        { 
                            persona.RazonSocial = TempData["FiltroRazonSocial"].ToString();
                        }
                        listaComprobantes = RN.Comprobante.ListaFiltradaPagging(out cantidadFilas, PagedResult.CurrentPage, "", estadosVentas, estadosCompras, tiposComprobantes, fecDsd, fecHst, persona, naturalezaComprobante, "", "", "", sesion);
                        PagedResult.Lista = listaComprobantes;
                        PagedResult.RowCount = ViewBag.CantidadFilas = cantidadFilas;
                    }
                    else
                    {
                        TempData["FiltroFechaDesde"] = Convert.ToDateTime(DateTime.Now).ToString("01/MM/yyyy");
                        TempData["FiltroFechaHasta"] = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
                        TempData["FiltroRazonSocial"] = "";
                    }
                    _RequestHandler.HandleAboutRequest();
                    sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
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
            TempData.Keep();
            return View(PagedResult);
        }
        [HttpPost]
        //public async Task<IActionResult> Index([FromForm] string FiltroRazonSocial, [Bind("FiltroFechaDesde, FiltroFechaHasta, FiltroRazonSocial")] CedFCIC.Models.ComprobanteFiltro comprobanteFiltro)
        public async Task<IActionResult> Index([Bind("FiltroFechaDesde, FiltroFechaHasta, FiltroRazonSocial")] CedFCIC.Models.ComprobanteFiltro comprobanteFiltro)
        {
            List<CedFCIC.Entidades.Comprobante> listaComprobantes = new List<CedFCIC.Entidades.Comprobante>();
            CedFCIC.Models.PagedResultComprobante<CedFCIC.Entidades.Comprobante> PagedResult = new CedFCIC.Models.PagedResultComprobante<CedFCIC.Entidades.Comprobante>();
            try
            {
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                if (Funciones.SessionOK(sesion))
                {
                    if (ModelState.IsValid)
                    {
                        int cantidadFilas = 0;
                        _RequestHandler.HandleAboutRequest();
                        sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
                        List<CedFCIC.Entidades.Estado> estadosVentas = new List<CedFCIC.Entidades.Estado>();
                        List<CedFCIC.Entidades.Estado> estadosCompras = new List<CedFCIC.Entidades.Estado>();
                        List<FeaEntidades.TiposDeComprobantes.TipoComprobante> tiposComprobantes = new List<FeaEntidades.TiposDeComprobantes.TipoComprobante>();
                        CedFCIC.Entidades.Persona persona = new CedFCIC.Entidades.Persona();
                        CedFCIC.Entidades.NaturalezaComprobante naturalezaComprobante = new CedFCIC.Entidades.NaturalezaComprobante();
                        naturalezaComprobante.Id = "Venta";
                        naturalezaComprobante.Descr = "Venta";
                        string fecDsd = Convert.ToDateTime(comprobanteFiltro.FiltroFechaDesde).ToString("yyyyMMdd");
                        string fecHst = Convert.ToDateTime(comprobanteFiltro.FiltroFechaHasta).ToString("yyyyMMdd");
                        if (comprobanteFiltro.FiltroRazonSocial != null)
                        {
                            persona.RazonSocial = comprobanteFiltro.FiltroRazonSocial;
                        }
                        listaComprobantes = RN.Comprobante.ListaFiltradaPagging(out cantidadFilas, 1, "", estadosVentas, estadosCompras, tiposComprobantes, fecDsd, fecHst, persona, naturalezaComprobante, "", "", "", sesion);
                        PagedResult.Lista = listaComprobantes;
                        PagedResult.PageSize = sesion.Usuario.CantidadFilasXPagina;
                        PagedResult.CurrentPage = 1;
                        PagedResult.RowCount = ViewBag.CantidadFilas = cantidadFilas;
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
            TempData["FiltroFechaDesde"] = comprobanteFiltro.FiltroFechaDesde;
            TempData["FiltroFechaHasta"] = comprobanteFiltro.FiltroFechaHasta;
            TempData["FiltroRazonSocial"] = comprobanteFiltro.FiltroRazonSocial;
            TempData.Keep();
            return View(PagedResult);
        }
    }
}
