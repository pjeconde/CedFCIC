using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CedFCIC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace CedFCIC.Controllers
{
    public class ComprobanteController : FCICController
    {
        private readonly reCAPTCHAService _ReCAPTCHAService;
        private readonly RequestHandler _RequestHandler;
        private readonly IWebHostEnvironment _env;

        public ComprobanteController(IConfiguration configuracion, RequestHandler requestHandler, reCAPTCHAService _reCAPTCHAService, IWebHostEnvironment env) : base(configuracion, requestHandler)
        {
            _ReCAPTCHAService = _reCAPTCHAService;
            _RequestHandler = requestHandler;
            _env = env;
        }

        // GET: Articulos
        public async Task<IActionResult> Index()
        {
            List<CedFCIC.Entidades.Comprobante> listaComprobantes = new List<CedFCIC.Entidades.Comprobante>();
            CedFCIC.Models.PagedResult<CedFCIC.Entidades.Comprobante, CedFCIC.Models.ComprobanteFiltro> PagedResult = new CedFCIC.Models.PagedResult<CedFCIC.Entidades.Comprobante, CedFCIC.Models.ComprobanteFiltro>();
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
                        PagedResult.Filtro = JsonConvert.DeserializeObject<ComprobanteFiltro>(TempData["ComprobanteFiltro"].ToString());
                        string fecDsd = Convert.ToDateTime(PagedResult.Filtro.FiltroFechaDesde).ToString("yyyyMMdd");
                        string fecHst = Convert.ToDateTime(PagedResult.Filtro.FiltroFechaHasta).ToString("yyyyMMdd");
                        if (PagedResult.Filtro.FiltroRazonSocial != null)
                        { 
                            persona.RazonSocial = PagedResult.Filtro.FiltroRazonSocial;
                        }
                        listaComprobantes = RN.Comprobante.ListaFiltradaPagging(out cantidadFilas, PagedResult.CurrentPage, "", estadosVentas, estadosCompras, tiposComprobantes, fecDsd, fecHst, persona, naturalezaComprobante, "", "", "", sesion);
                        PagedResult.Lista = listaComprobantes;
                        PagedResult.RowCount = ViewBag.CantidadFilas = cantidadFilas;
                        if (listaComprobantes.Count > 0)
                        {
                            ViewBag.Boton1ReadOnly = "";
                        }
                        else 
                        {
                            ViewBag.Boton1ReadOnly = "Disabled";
                        }
                    }
                    else
                    {
                        if (PagedResult.Filtro == null)
                        {
                            PagedResult.Filtro = new ComprobanteFiltro();
                        }
                        PagedResult.Filtro.FiltroFechaDesde = Convert.ToDateTime(DateTime.Now).ToString("01/MM/yyyy");
                        PagedResult.Filtro.FiltroFechaHasta = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
                        PagedResult.Filtro.FiltroRazonSocial = "";
                        ViewBag.Boton1ReadOnly = "Disabled";
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
            CedFCIC.Models.PagedResult<CedFCIC.Entidades.Comprobante, CedFCIC.Models.ComprobanteFiltro> PagedResult = new CedFCIC.Models.PagedResult<CedFCIC.Entidades.Comprobante, CedFCIC.Models.ComprobanteFiltro>();
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
                        PagedResult.Filtro = comprobanteFiltro;
                        PagedResult.PageSize = sesion.Usuario.CantidadFilasXPagina;
                        PagedResult.CurrentPage = 1;
                        PagedResult.RowCount = ViewBag.CantidadFilas = cantidadFilas;
                        if (listaComprobantes.Count > 0)
                        {
                            ViewBag.Boton1ReadOnly = "";
                        }
                        else
                        {
                            ViewBag.Boton1ReadOnly = "Disabled";
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
            TempData["ComprobanteFiltro"] = JsonConvert.SerializeObject(comprobanteFiltro);
            TempData.Keep();
            return View(PagedResult);
        }
        [HttpPost]
        public async Task<IActionResult> ExportarAExcel()
        {
            if (TempData["ComprobanteFiltro"] != null)
            { 
                string webRootPath = _env.WebRootPath;
                string fileName = @"Testingdummy.xlsx";
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);
                FileInfo file = new FileInfo(Path.Combine(webRootPath, fileName));
                var memoryStream = new MemoryStream();

                List<CedFCIC.Entidades.Comprobante> listaComprobantes = new List<CedFCIC.Entidades.Comprobante>();
                CedFCIC.Models.ComprobanteFiltro comprobanteFiltro = JsonConvert.DeserializeObject<ComprobanteFiltro>(TempData["ComprobanteFiltro"].ToString());
                _RequestHandler.HandleAboutRequest();
                Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
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
                listaComprobantes = RN.Comprobante.ListaFiltrada(estadosVentas, estadosCompras, tiposComprobantes, "", fecDsd, fecHst, persona, naturalezaComprobante, "", "", false, false, "", sesion, false);
                if (listaComprobantes.Count > 0)
                {
                    // --- Below code would create excel file with dummy data----  
                    using (var fs = new FileStream(Path.Combine(webRootPath, fileName), FileMode.Create, FileAccess.Write))
                    {
                        IWorkbook workbook = new XSSFWorkbook();
                        ISheet excelSheet = workbook.CreateSheet("Testingdummy");

                        IRow row = excelSheet.CreateRow(0);
                        row.CreateCell(0).SetCellValue("Cuit");
                        row.CreateCell(1).SetCellValue("NaturalezaComp");
                        row.CreateCell(2).SetCellValue("TipoComp");
                        row.CreateCell(3).SetCellValue("Pto.Vta");
                        row.CreateCell(4).SetCellValue("NroComp");
                        row.CreateCell(5).SetCellValue("RazonSocial");
                        row.CreateCell(6).SetCellValue("Moneda");
                        row.CreateCell(7).SetCellValue("Importe");
                        int counter = 1;
                        foreach (var lc in listaComprobantes)
                        {
                            string FirstName = string.Empty;
                            if (lc.RazonSocial.Length > 100)
                                FirstName = lc.RazonSocial.Substring(0, 100);
                            else
                                FirstName = lc.RazonSocial;
                            row = excelSheet.CreateRow(counter);
                            row.CreateCell(0).SetCellValue(lc.Cuit);
                            row.CreateCell(1).SetCellValue(lc.NaturalezaComprobante.Descr);
                            row.CreateCell(2).SetCellValue(lc.TipoComprobante.Descr);
                            row.CreateCell(3).SetCellValue(lc.NroPuntoVta);
                            row.CreateCell(4).SetCellValue(lc.Nro);
                            row.CreateCell(5).SetCellValue(FirstName);
                            row.CreateCell(6).SetCellValue(lc.ImporteMoneda);
                            row.CreateCell(7).SetCellValue(lc.Importe);
                            counter++;
                        }
                        workbook.Write(fs);
                    }
                    using (var fileStream = new FileStream(Path.Combine(webRootPath, fileName), FileMode.Open))
                    {
                        await fileStream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Position = 0;
                    TempData.Keep();
                    return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
                else 
                {
                    TempData["Ex"] = "No hay información para exportar.";
                    return RedirectToAction("Index", "Comprobante");
                }
            }
            else
            {
                TempData["Ex"] = "No hay información para exportar.";
                return RedirectToAction("Index", "Comprobante");
            }
        }
    }
}
