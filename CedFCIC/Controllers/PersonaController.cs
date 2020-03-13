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
    public class PersonaController : Controller
    {
        private readonly reCAPTCHAService _ReCAPTCHAService;
        public PersonaController(IConfiguration configuration, reCAPTCHAService _reCAPTCHAService)
        {
            Configuration = configuration;
            _ReCAPTCHAService = _reCAPTCHAService;
        }

        public IConfiguration Configuration { get; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
