using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CedFCIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediosController : ControllerBase
    {
        // GET: api/Medios/5
        /// <summary>
        /// Consultar un Id. de Medio
        /// </summary>
        /// <param name="id">Mail</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            Entidades.Sesion sesion = new Entidades.Sesion();
            string resp = "";
            List<Entidades.Medio> medios = new List<Entidades.Medio>();
            medios = CedFCIC.RN.Medio.Lista(sesion);
            return medios[0].IdMedio + "-" + medios[0].DescrMedio;
        }

        // GET: api/Medios/5
        /// <summary>
        /// Consultar lista de Medios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<CedFCIC.Entidades.Medio> Get()
        {
            Entidades.Sesion sesion = new Entidades.Sesion();
            List<Entidades.Medio> lmedios = new List<Entidades.Medio>();
            lmedios = CedFCIC.RN.Medio.Lista(sesion);
            return lmedios;
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
