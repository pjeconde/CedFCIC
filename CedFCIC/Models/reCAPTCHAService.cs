using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;

namespace CedFCIC.Models
{
    public class reCAPTCHAService
    {
        private ReCAPTCHASettings _settings;

        public reCAPTCHAService(IOptions<ReCAPTCHASettings> settings)
        {
            _settings = settings.Value; 
        }
        public virtual async Task<reCAPTCHARespuesta> VerifyreCAPTCHA(string _token)
        {
            reCAPTCHAData MyData = new reCAPTCHAData
            {
                response = _token,
                secret = _settings.ReCAPTCHA_Secret_Key
            };

            HttpClient client = new HttpClient();
            //var respaux = client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={MyData.secret}&response={MyData.response}").Result;
            var respaux = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={MyData.secret}&response={MyData.response}");
            var capresp = JsonConvert.DeserializeObject<reCAPTCHARespuesta>(respaux);
            return capresp;
        }
            
    }
    public class reCAPTCHAData
    {
        public string response { get; set; }
        public string secret { get; set; }
    }
    public class reCAPTCHARespuesta
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
