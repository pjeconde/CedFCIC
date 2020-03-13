using Microsoft.AspNetCore.Http;

namespace CedFCIC
{
    public class AppSettings
    {
        public AppSettings()
        {
            
        }
    }
    public class RequestHandler
    {
        IHttpContextAccessor _httpContextAccessor;
        public RequestHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        internal void HandleAboutRequest()
        {
            // handle the request
            Entidades.Sesion sesion = _httpContextAccessor.HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            if (Funciones.SessionOK(sesion))
            {
                RN.Sesion.RefrescarDatosUsuario(sesion.Usuario, sesion);
                _httpContextAccessor.HttpContext.Session.SetObj("Sesion", sesion);
            }
            //var message = "The HttpContextAccessor seems to be working!!";
            //_httpContextAccessor.HttpContext.Session.SetString("message", message);
        }
    }
}
