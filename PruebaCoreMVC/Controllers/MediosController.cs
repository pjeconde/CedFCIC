using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PruebaCoreMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediosController : ControllerBase
    {
        // GET: api/Medios
        /// <returns></returns>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        // GET: api/Medios/5
        /// <summary>
        /// Consultar un Id. de Medio
        /// </summary>
        /// <param name="id">Mail</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            string resp = "";
            PruebaCore.RN.Medio rnMedio = new PruebaCore.RN.Medio();
            PruebaCore.Entidades.Medio medio = rnMedio.ConsultaMedio(id, out resp);
            return medio.IdMedio + "-" + medio.DescrMedio;
        }

        // GET: api/Medios/5
        /// <summary>
        /// Consultar un Id. de Medio
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<PruebaCore.Entidades.Medio> Get()
        {
            PruebaCore.RN.Medio rnMedio = new PruebaCore.RN.Medio();
            List<PruebaCore.Entidades.Medio> lMedio = rnMedio.ConsultaListaMedio();
            return lMedio;
        }

        // POST: api/Medios
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Medios/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
