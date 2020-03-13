using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CedFCIC.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;

namespace CedFCIC.Controllers
{
    public class FCICController : Controller
    {
        public readonly RequestHandler _RequestHandler;
        public FCICController(IConfiguration configuration, RequestHandler requestHandler)
        {
            Configuration = configuration;
            _RequestHandler = requestHandler;
        }

        public IConfiguration Configuration { get; }
      
    }
}