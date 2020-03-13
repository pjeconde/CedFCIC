using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace CedFCIC.EX
{
    /// <summary>
    /// Descripción breve de Excepciones.
    /// </summary>
    namespace Usuario
    {
        
        [Serializable]
        public class BaseApplicationException : Exception
        {
            public BaseApplicationException(string TextoError)
                : base(TextoError)
            {
            }
            public BaseApplicationException(string TextoError, Exception inner)
                : base(TextoError, inner)
            {
            }
            public BaseApplicationException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class PasswordYConfirmacionNoCoincidente : CedFCIC.EX.Usuario.BaseApplicationException
        {
            static string TextoError = "La Contraseña no coincide con su Confirmación";
            public PasswordYConfirmacionNoCoincidente()
                : base(TextoError)
            {
            }
            public PasswordYConfirmacionNoCoincidente(Exception inner)
                : base(TextoError, inner)
            {
            }
            public PasswordYConfirmacionNoCoincidente(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class PasswordNuevaIgualAActual : CedFCIC.EX.Usuario.BaseApplicationException
        {
            static string TextoError = "La Contraseña nueva no debe ser igual a la actual";
            public PasswordNuevaIgualAActual()
                : base(TextoError)
            {
            }
            public PasswordNuevaIgualAActual(Exception inner)
                : base(TextoError, inner)
            {
            }
            public PasswordNuevaIgualAActual(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class IdUsuarioNoDisponible : CedFCIC.EX.Usuario.BaseApplicationException
        {
            static string TextoError = "El IdUsuario, que ingresó, ya ha sido usado por otra persona.  Modifiquelo hasta encontrar un valor único.";
            public IdUsuarioNoDisponible()
                : base(TextoError)
            {
            }
            public IdUsuarioNoDisponible(Exception inner)
                : base(TextoError, inner)
            {
            }
            public IdUsuarioNoDisponible(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class ParametrosAccionCompradorErroneo : CedFCIC.EX.Usuario.BaseApplicationException
        {
            static string TextoError = "Acción inválida sobre Comprador.  Por favor, póngase en contacto con Cedeira Software Factory, para solucionar el inconveniente.  Muchas gracias.";
            public ParametrosAccionCompradorErroneo()
                : base(TextoError)
            {
            }
            public ParametrosAccionCompradorErroneo(Exception inner)
                : base(TextoError, inner)
            {
            }
            public ParametrosAccionCompradorErroneo(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class ErrorDeConfirmacion : CedFCIC.EX.Usuario.BaseApplicationException
        {
            static string TextoError = "El evento de confirmación (de creación de la cuenta eFact) no puede ejecutarse.  Es probable que la confirmación ya haya sido registrada.  Verifique si puede identificarse.  En paso contrario, póngase en contacto con Cedeira Software Factory, para solucionar el inconveniente.  Muchas gracias.";
            public ErrorDeConfirmacion()
                : base(TextoError)
            {
            }
            public ErrorDeConfirmacion(Exception inner)
                : base(TextoError, inner)
            {
            }
            public ErrorDeConfirmacion(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class LoginRechazadoXEstadoCuenta : CedFCIC.EX.Usuario.BaseApplicationException
        {
            static string TextoError = "Login inválido (la cuenta está pendiente de confirmación o dada de baja)";
            public LoginRechazadoXEstadoCuenta()
                : base(TextoError)
            {
            }
            public LoginRechazadoXEstadoCuenta(Exception inner)
                : base(TextoError, inner)
            {
            }
            public LoginRechazadoXEstadoCuenta(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class LoginRechazadoXPasswordInvalida : CedFCIC.EX.Usuario.BaseApplicationException
        {
            static string TextoError = "Contraseña inválida";
            public LoginRechazadoXPasswordInvalida()
                : base(TextoError)
            {
            }
            public LoginRechazadoXPasswordInvalida(Exception inner)
                : base(TextoError, inner)
            {
            }
            public LoginRechazadoXPasswordInvalida(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class NoHayUsuariosAsociadasAEmail : CedFCIC.EX.Usuario.BaseApplicationException
        {
            static string TextoError = "No hay cuentas asociadas a la dirección de correo electrónico especificada";
            public NoHayUsuariosAsociadasAEmail()
                : base(TextoError)
            {
            }
            public NoHayUsuariosAsociadasAEmail(Exception inner)
                : base(TextoError, inner)
            {
            }
            public NoHayUsuariosAsociadasAEmail(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class UsuarioConfFormatoMsgErroneo : CedFCIC.EX.Usuario.BaseApplicationException
        {
            static string TextoError = "El mensaje de confirmación (de creación de la cuenta eFact) tiene un formato erróneo.  Por favor, póngase en contacto con Cedeira Software Factory, para solucionar el inconveniente.  Muchas gracias.";
            public UsuarioConfFormatoMsgErroneo()
                : base(TextoError)
            {
            }
            public UsuarioConfFormatoMsgErroneo(Exception inner)
                : base(TextoError, inner)
            {
            }
            public UsuarioConfFormatoMsgErroneo(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class PasswordNoMatch : CedFCIC.EX.Usuario.BaseApplicationException
        {
            static string TextoError = "Contraseña incorrecta";
            public PasswordNoMatch()
                : base(TextoError)
            {
            }
            public PasswordNoMatch(Exception inner)
                : base(TextoError, inner)
            {
            }
            public PasswordNoMatch(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
    }
    namespace WF
    {
        [Serializable]
        public class BaseApplicationException : Exception
        {
            public BaseApplicationException(string TextoError)
                : base(TextoError)
            {
            }
            public BaseApplicationException(string TextoError, Exception inner)
                : base(TextoError, inner)
            {
            }
            public BaseApplicationException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class OpInexistente : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "Operacion inexistente (WF)";
            public OpInexistente() : base(TextoError)
            {
            }
            public OpInexistente(Exception inner) : base(TextoError, inner)
            {
            }
            public OpInexistente(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class EventoInvalido : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "Evento invalido (WF)";
            public EventoInvalido()
                : base(TextoError)
            {
            }
            public EventoInvalido(Exception inner)
                : base(TextoError, inner)
            {
            }
            public EventoInvalido(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class EstadoHstInvalido : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "Seleccion inválida de EstadoHst (WF)";
            public EstadoHstInvalido()
                : base(TextoError)
            {
            }
            public EstadoHstInvalido(Exception inner)
                : base(TextoError, inner)
            {
            }
            public EstadoHstInvalido(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class EstadoHstNoDefinido : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "EstadoHst no definido (WF)";
            public EstadoHstNoDefinido()
                : base(TextoError)
            {
            }
            public EstadoHstNoDefinido(Exception inner)
                : base(TextoError, inner)
            {
            }
            public EstadoHstNoDefinido(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class FlowInvalido : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "Flow invalido (WF)";
            public FlowInvalido() : base(TextoError)
            {
            }
            public FlowInvalido(Exception inner) : base(TextoError, inner)
            {
            }
            public FlowInvalido(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class CircuitoInvalido : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "Circuito invalido (WF)";
            public CircuitoInvalido() : base(TextoError)
            {
            }
            public CircuitoInvalido(Exception inner) : base(TextoError, inner)
            {
            }
            public CircuitoInvalido(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class NivSegInvalido : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "Nivel de seguridad invalido (WF)";
            public NivSegInvalido() : base(TextoError)
            {
            }
            public NivSegInvalido(Exception inner) : base(TextoError, inner)
            {
            }
            public NivSegInvalido(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class EventoSinSeg : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "Evento sin esquema de seguridad asociado (WF)";
            public EventoSinSeg() : base(TextoError)
            {
            }
            public EventoSinSeg(Exception inner) : base(TextoError, inner)
            {
            }
            public EventoSinSeg(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class UsuarioNoCumpleSeg : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "Usuario no cumple con el esquema de seguridad del evento (WF)";
            public UsuarioNoCumpleSeg() : base(TextoError)
            {
            }
            public UsuarioNoCumpleSeg(Exception inner) : base(TextoError, inner)
            {
            }
            public UsuarioNoCumpleSeg(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class EventoIniMalConfigurado : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "El esquema de seguridad del evento inicial esta configurado de manera incorrecta.  Los eventos iniciales no deben requerir mas de una intervencion (WF)";
            public EventoIniMalConfigurado() : base(TextoError)
            {
            }
            public EventoIniMalConfigurado(Exception inner) : base(TextoError, inner)
            {
            }
            public EventoIniMalConfigurado(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class EstadoVirtualMalConfigurado : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "No es posible encontrar el valor del estado (virtual) de destino, definido en el evento (WF)";
            public EstadoVirtualMalConfigurado() : base(TextoError)
            {
            }
            public EstadoVirtualMalConfigurado(Exception inner) : base(TextoError, inner)
            {
            }
            public EstadoVirtualMalConfigurado(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class CXO : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "Usuario no cumple con el esquema de control por oposicion (WF)";
            public CXO() : base(TextoError)
            {
            }
            public CXO(Exception inner) : base(TextoError, inner)
            {
            }
            public CXO(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class FechaProcesoNoMatch : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "La fecha de proceso no coincide con la fecha de la base de datos";
            public FechaProcesoNoMatch() : base(TextoError)
            {
            }
            public FechaProcesoNoMatch(Exception inner) : base(TextoError, inner)
            {
            }
            public FechaProcesoNoMatch(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class FechaNoMatch : CedFCIC.EX.WF.BaseApplicationException
        {
            static string TextoError = "La fecha no coincide con la fecha de proceso";
            public FechaNoMatch() : base(TextoError)
            {
            }
            public FechaNoMatch(Exception inner) : base(TextoError, inner)
            {
            }
            public FechaNoMatch(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
    namespace db
    {
        [Serializable]
        public class Conexion : Exception
        {
            static string TextoError = "Problema de conexión a base de datos";
            public Conexion() : base(TextoError)
            {
            }
            public Conexion(Exception inner) : base(TextoError, inner)
            {
            }
            public Conexion(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class Ejecucion : Exception
        {
            static string TextoError = "Problema en ejecución de script de SQL";
            public Ejecucion() : base(TextoError)
            {
            }
            public Ejecucion(Exception inner) : base(TextoError, inner)
            {
            }
            public Ejecucion(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class EjecucionConRollback : Exception
        {
            static string TextoError = "Problema en ejecución de script de SQL ( Se deshizo la operacion )";
            public EjecucionConRollback() : base(TextoError)
            {
            }
            public EjecucionConRollback(Exception inner) : base(TextoError, inner)
            {
            }
            public EjecucionConRollback(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class Rollback : Exception
        {
            static string TextoError = "Problema al tratar de deshacer la ejecución de un script de SQL";
            public Rollback() : base(TextoError)
            {
            }
            public Rollback(Exception inner) : base(TextoError, inner)
            {
            }
            public Rollback(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class FlowEventoInvalido : Exception
        {
            static string TextoError = "Combinación Flow-Evento invalida";
            public FlowEventoInvalido() : base(TextoError)
            {
            }
            public FlowEventoInvalido(Exception inner) : base(TextoError, inner)
            {
            }
            public FlowEventoInvalido(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
    namespace Validaciones
    {
        [Serializable]
        public class BaseApplicationException : Exception
        {
            public BaseApplicationException(string TextoError) : base(TextoError)
            {
            }
            public BaseApplicationException(string TextoError, Exception inner) : base(TextoError, inner)
            {
            }
            public BaseApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class PrecioNoEncontrado : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
        {
            static string TextoError = "Precio no encontrado";
            public PrecioNoEncontrado(string DescrEspecie, DateTime Fecha) : base(TextoError + ".  Especie: '" + DescrEspecie + "', Dia: " + Fecha.ToString("dd/MM/yyyy"))
            {
            }
            public PrecioNoEncontrado(Exception inner) : base(TextoError, inner)
            {
            }
            public PrecioNoEncontrado(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class PrecioNoEncontradoEnExcel : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
        {
            static string TextoError = "Precio no encontrado en planilla Excel: ";
            public PrecioNoEncontradoEnExcel(string DescrEspecie) : base(TextoError + DescrEspecie)
            {
            }
            public PrecioNoEncontradoEnExcel(Exception inner) : base(TextoError, inner)
            {
            }
            public PrecioNoEncontradoEnExcel(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ConexionInactiva : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "Conexión inactiva con ";
            public ConexionInactiva(string Sistema) : base(TextoError + Sistema)
            {
            }
            public ConexionInactiva(Exception inner) : base(TextoError, inner)
            {
            }
            public ConexionInactiva(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ElementoInexistente : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "inexistente";
            public ElementoInexistente(string Descripcion) : base(Descripcion + " " + TextoError)
            {
            }
            public ElementoInexistente(Exception inner) : base(TextoError, inner)
            {
            }
            public ElementoInexistente(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ElementoNoEncontrado : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            private static string TextoError = "no encontrado";
            public ElementoNoEncontrado(string descrProp) : base(descrProp + " " + TextoError)
            {
            }
            public ElementoNoEncontrado(Exception inner) : base(TextoError, inner)
            {
            }
            public ElementoNoEncontrado(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class AjustePrecision : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "No se puede ajustar ";
            public AjustePrecision(double Numero, int LongitudIncluyendoSeparador) : base(TextoError + Numero + " a " + LongitudIncluyendoSeparador + " posiciones")
            {
            }
            public AjustePrecision(Exception inner) : base(TextoError, inner)
            {
            }
            public AjustePrecision(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ValorInvalido : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "valor inválido";
            public ValorInvalido(string descrProp) : base(descrProp + ": " + TextoError)
            {
            }
            public ValorInvalido(Exception inner) : base(TextoError, inner)
            {
            }
            public ValorInvalido(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ValorNoInfo : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "sin informar";
            public ValorNoInfo(string descrProp) : base(descrProp + " " + TextoError)
            {
            }
            public ValorNoInfo(Exception inner) : base(TextoError, inner)
            {
            }
            public ValorNoInfo(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ValorDesconocido : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "desconocido";
            public ValorDesconocido(string descrProp) : base(descrProp + " " + TextoError)
            {
            }
            public ValorDesconocido(Exception inner) : base(TextoError, inner)
            {
            }
            public ValorDesconocido(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class FormatoPlanillaIncorrecto : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "Formato planilla incorrecto";
            public FormatoPlanillaIncorrecto()
                : base(TextoError)
            {
            }
            public FormatoPlanillaIncorrecto(Exception inner)
                : base(TextoError, inner)
            {
            }
            public FormatoPlanillaIncorrecto(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class CanalIndeterminable : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "No se puede determinar el canal al que pertenece el ";
            public CanalIndeterminable(string descrProp) : base(TextoError + " " + descrProp)
            {
            }
            public CanalIndeterminable(Exception inner) : base(TextoError, inner)
            {
            }
            public CanalIndeterminable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class PerfilIndeterminable : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "No se puede determinar el perfil del ";
            public PerfilIndeterminable(string descrProp) : base(TextoError + " " + descrProp)
            {
            }
            public PerfilIndeterminable(Exception inner) : base(TextoError, inner)
            {
            }
            public PerfilIndeterminable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class RolDesconocido : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "Rol desconocido: ";
            public RolDesconocido(string descrProp)
                : base(TextoError + " " + descrProp)
            {
            }
            public RolDesconocido(Exception inner)
                : base(TextoError, inner)
            {
            }
            public RolDesconocido(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class MultiploDe : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            private static string TextoError = " debe ser múltiplo de ";
            public MultiploDe(string multiplo, string descrProp) : base(multiplo + TextoError + descrProp)
            {
            }
            public MultiploDe(Exception inner) : base(TextoError, inner)
            {
            }
            public MultiploDe(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ReadOnly : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "modificación inhabilitada";
            public ReadOnly(string descrProp) : base(descrProp + ": " + TextoError)
            {
            }
            public ReadOnly(Exception inner) : base(TextoError, inner)
            {
            }
            public ReadOnly(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ValorNoMatch : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "no coincide, debe ser";
            public ValorNoMatch(string descrProp, string descrPropMatch) : base(descrProp + " " + TextoError + " " + descrPropMatch)
            {
            }
            public ValorNoMatch(Exception inner) : base(TextoError, inner)
            {
            }
            public ValorNoMatch(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ValorNegativo : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "debe ser mayor a cero";
            public ValorNegativo(string descrProp) : base(descrProp + ": " + TextoError)
            {
            }
            public ValorNegativo(Exception inner) : base(TextoError, inner)
            {
            }
            public ValorNegativo(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ValorNoNumerico : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "debe ser numerico";
            public ValorNoNumerico(string descrProp) : base(descrProp + ": " + TextoError)
            {
            }
            public ValorNoNumerico(Exception inner) : base(TextoError, inner)
            {
            }
            public ValorNoNumerico(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ValorNoCombo : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "opción inválida";
            public ValorNoCombo(string descrProp) : base(descrProp + ": " + TextoError)
            {
            }
            public ValorNoCombo(Exception inner) : base(TextoError, inner)
            {
            }
            public ValorNoCombo(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class OpcionInvalida : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "Opción inválida";
            public OpcionInvalida() : base(TextoError)
            {
            }
            public OpcionInvalida(string Descr) : base(TextoError + ": " + Descr)
            {
            }
            public OpcionInvalida(Exception inner) : base(TextoError, inner)
            {
            }
            public OpcionInvalida(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class LenInvalida : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "longitud inválida";
            public LenInvalida(string descrProp) : base(descrProp + ": " + TextoError)
            {
            }
            public LenInvalida(Exception inner) : base(TextoError, inner)
            {
            }
            public LenInvalida(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class TipoNoCoincidente : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "el tipo de la base de datos no coincide con la propiedad";
            public TipoNoCoincidente(string descrProp) : base(descrProp + ": " + TextoError)
            {
            }
            public TipoNoCoincidente(Exception inner) : base(TextoError, inner)
            {
            }
            public TipoNoCoincidente(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class NoEsMultiploDe24 : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "debe ser multiplo de 24";
            public NoEsMultiploDe24(string descrProp) : base(descrProp + ": " + TextoError)
            {
            }
            public NoEsMultiploDe24(Exception inner) : base(TextoError, inner)
            {
            }
            public NoEsMultiploDe24(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ListaDeExcepciones : CedFCIC.EX.Validaciones.BaseApplicationException
        {
            static string TextoError = "Lista de errores: ";
            private List<System.Exception> listaE;
            public ListaDeExcepciones(List<System.Exception> l) : base(TextoError)
            {
                listaE = l;
            }
            public List<System.Exception> ListaE
            {
                get
                {
                    return listaE;
                }
            }
        }

        namespace CashFlow
        {
            [Serializable]
            public class BaseApplicationException : CedFCIC.EX.Validaciones.BaseApplicationException
            {
                public BaseApplicationException(string TextoError) : base(TextoError)
                {
                }
                public BaseApplicationException(string TextoError, Exception inner) : base(TextoError, inner)
                {
                }
                public BaseApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class CtasMonConceptos : CedFCIC.EX.Validaciones.CashFlow.BaseApplicationException
            {
                static string TextoError = "Se modificaron los conceptos de ingresos y/o egresos de cuentas monetarias. Actualize parámetros del sistema";
                public CtasMonConceptos() : base(TextoError)
                {
                }
                public CtasMonConceptos(Exception inner) : base(TextoError, inner)
                {
                }
                public CtasMonConceptos(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
        }
        namespace Fechas
        {
            [Serializable]
            public class BaseApplicationException : CedFCIC.EX.Validaciones.BaseApplicationException
            {
                public BaseApplicationException(string TextoError) : base(TextoError)
                {
                }
                public BaseApplicationException(string TextoError, Exception inner) : base(TextoError, inner)
                {
                }
                public BaseApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class FechaNoHabil : CedFCIC.EX.Validaciones.Fechas.BaseApplicationException
            {
                static string TextoError = "Según host la fecha no es un día hábil.";
                public FechaNoHabil(string DescrProp) : base(DescrProp + ":" + TextoError)
                {
                }
                public FechaNoHabil(Exception inner) : base(TextoError, inner)
                {
                }
                public FechaNoHabil(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class FechaDsdCashFlow : CedFCIC.EX.Validaciones.Fechas.BaseApplicationException
            {
                static string TextoError = "La fecha desde debe ser siempre la del día hábil anterior.";
                public FechaDsdCashFlow(string DescrProp) : base(DescrProp + ":" + TextoError)
                {
                }
                public FechaDsdCashFlow(Exception inner) : base(TextoError, inner)
                {
                }
                public FechaDsdCashFlow(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class FechaFormatoNoValido : CedFCIC.EX.Validaciones.Fechas.BaseApplicationException
            {
                static string TextoError = "Formato incorrecto en: ";
                public FechaFormatoNoValido(string Nombre)
                    : base(TextoError + Nombre)
                {
                }
                public FechaFormatoNoValido(Exception inner)
                    : base(TextoError, inner)
                {
                }
                public FechaFormatoNoValido(SerializationInfo info, StreamingContext context)
                    : base(info, context)
                {
                }
            }
            [Serializable]
            public class FechaAñoInvalido : CedFCIC.EX.Validaciones.Fechas.BaseApplicationException
            {
                static string TextoError = "Año incorrecto en";
                public FechaAñoInvalido(string Nombre)
                    : base(TextoError + Nombre)
                {
                }
                public FechaAñoInvalido(Exception inner)
                    : base(TextoError, inner)
                {
                }
                public FechaAñoInvalido(SerializationInfo info, StreamingContext context)
                    : base(info, context)
                {
                }
            }

            [Serializable]
            public class FechaMesInvalido : CedFCIC.EX.Validaciones.Fechas.BaseApplicationException
            {
                static string TextoError = "Mes incorrecto en ";
                public FechaMesInvalido(string Nombre)
                    : base(TextoError + Nombre)
                {
                }
                public FechaMesInvalido(Exception inner)
                    : base(TextoError, inner)
                {
                }
                public FechaMesInvalido(SerializationInfo info, StreamingContext context)
                    : base(info, context)
                {
                }
            }
            [Serializable]
            public class FechaDiaInvalido : CedFCIC.EX.Validaciones.Fechas.BaseApplicationException
            {
                static string TextoError = "Dia Incorrecto en ";
                public FechaDiaInvalido(string Nombre)
                    : base(TextoError + Nombre)
                {
                }
                public FechaDiaInvalido(Exception inner)
                    : base(TextoError, inner)
                {
                }
                public FechaDiaInvalido(SerializationInfo info, StreamingContext context)
                    : base(info, context)
                {
                }
            }
        }
        namespace Operaciones
        {
            [Serializable]
            public class BaseApplicationException : CedFCIC.EX.Validaciones.BaseApplicationException
            {
                public BaseApplicationException(string TextoError) : base(TextoError)
                {
                }
                public BaseApplicationException(string TextoError, Exception inner) : base(TextoError, inner)
                {
                }
                public BaseApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class NoBalancea : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
            {
                static string TextoError = "La operación no balancea.  Verifique la Diferencia.";
                public NoBalancea() : base(TextoError)
                {
                }
                public NoBalancea(Exception inner) : base(TextoError, inner)
                {
                }
                public NoBalancea(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class DetalleNoIngresado : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
            {
                static string TextoError = "La operación debe contener, al menos, dos minutas";
                public DetalleNoIngresado() : base(TextoError)
                {
                }
                public DetalleNoIngresado(Exception inner) : base(TextoError, inner)
                {
                }
                public DetalleNoIngresado(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class MinutaAutomatica : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
            {
                static string TextoError = "Opcion invalida en minuta automatica";
                public MinutaAutomatica() : base(TextoError)
                {
                }
                public MinutaAutomatica(Exception inner) : base(TextoError, inner)
                {
                }
                public MinutaAutomatica(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class CierreCambioNoIngresado : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
            {
                static string TextoError = "Cierre de Cambio no ingresado";
                public CierreCambioNoIngresado() : base(TextoError)
                {
                }
                public CierreCambioNoIngresado(Exception inner) : base(TextoError, inner)
                {
                }
                public CierreCambioNoIngresado(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class UNincongruente : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
            {
                static string TextoError = "Hay, al menos, una minuta que referencia a una cuenta de otra unidad de negocio";
                public UNincongruente() : base(TextoError)
                {
                }
                public UNincongruente(Exception inner) : base(TextoError, inner)
                {
                }
                public UNincongruente(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class Fechaincongruente : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
            {
                static string TextoError = "Hay, al menos, una minuta con un vencimiento establecido. Para modificar la fecha de la operación, dé de baja la minuta y depure.";
                public Fechaincongruente() : base(TextoError)
                {
                }
                public Fechaincongruente(Exception inner) : base(TextoError, inner)
                {
                }
                public Fechaincongruente(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class NoEnEstadoFinal : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
            {
                static string TextoError = "Existe al menos una operación que no se encuentra en estado final.";
                public NoEnEstadoFinal() : base(TextoError)
                {
                }
                public NoEnEstadoFinal(Exception inner) : base(TextoError, inner)
                {
                }
                public NoEnEstadoFinal(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class PreciosNoEnEstadoFinal : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
            {
                static string TextoError = "Existe al menos un precio que no se encuentra en estado final.";
                public PreciosNoEnEstadoFinal() : base(TextoError)
                {
                }
                public PreciosNoEnEstadoFinal(Exception inner) : base(TextoError, inner)
                {
                }
                public PreciosNoEnEstadoFinal(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class TasaCAyCCENoEnEstadoFinal : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
            {
                static string TextoError = "Existe al menos una tasa que no se encuentra en estado final.";
                public TasaCAyCCENoEnEstadoFinal() : base(TextoError)
                {
                }
                public TasaCAyCCENoEnEstadoFinal(Exception inner) : base(TextoError, inner)
                {
                }
                public TasaCAyCCENoEnEstadoFinal(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class CierreDeCambioNoEnEstadoFinal : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
            {
                static string TextoError = "Existe al menos un cierre de cambio que no se encuentra en estado final.";
                public CierreDeCambioNoEnEstadoFinal() : base(TextoError)
                {
                }
                public CierreDeCambioNoEnEstadoFinal(Exception inner) : base(TextoError, inner)
                {
                }
                public CierreDeCambioNoEnEstadoFinal(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            namespace Minutas
            {
                [Serializable]
                public class BaseApplicationException : CedFCIC.EX.Validaciones.Operaciones.BaseApplicationException
                {
                    public BaseApplicationException(string TextoError) : base(TextoError)
                    {
                    }
                    public BaseApplicationException(string TextoError, Exception inner) : base(TextoError, inner)
                    {
                    }
                    public BaseApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
                    {
                    }
                }
                [Serializable]
                public class UNincongruente : CedFCIC.EX.Validaciones.Operaciones.Minutas.BaseApplicationException
                {
                    static string TextoError = "Unidad de Negocio incongruente.  No se incorporará la minuta.";
                    public UNincongruente() : base(TextoError)
                    {
                    }
                    public UNincongruente(Exception inner) : base(TextoError, inner)
                    {
                    }
                    public UNincongruente(SerializationInfo info, StreamingContext context) : base(info, context)
                    {
                    }
                }
                [Serializable]
                public class IndiceFueraDeRango : CedFCIC.EX.Validaciones.Operaciones.Minutas.BaseApplicationException
                {
                    static string TextoError = "Unidad de Negocio incongruente.  No se incorporará la minuta.";
                    public IndiceFueraDeRango() : base(TextoError)
                    {
                    }
                    public IndiceFueraDeRango(Exception inner) : base(TextoError, inner)
                    {
                    }
                    public IndiceFueraDeRango(SerializationInfo info, StreamingContext context) : base(info, context)
                    {
                    }
                }
                [Serializable]
                public class ConvinacionTipoMovProductoInvalido : CedFCIC.EX.Validaciones.Operaciones.Minutas.BaseApplicationException
                {
                    static string TextoError = "Tipo de movimiento, en Producto, inválido";
                    public ConvinacionTipoMovProductoInvalido() : base(TextoError)
                    {
                    }
                    public ConvinacionTipoMovProductoInvalido(Exception inner) : base(TextoError, inner)
                    {
                    }
                    public ConvinacionTipoMovProductoInvalido(SerializationInfo info, StreamingContext context) : base(info, context)
                    {
                    }
                }
                [Serializable]
                public class CantidadCPResc : CedFCIC.EX.Validaciones.Operaciones.Minutas.BaseApplicationException
                {
                    static string TextoError = "No se pueden rescatar mas cuotapartes de las suscriptas";
                    public CantidadCPResc() : base(TextoError)
                    {
                    }
                    public CantidadCPResc(Exception inner) : base(TextoError, inner)
                    {
                    }
                    public CantidadCPResc(SerializationInfo info, StreamingContext context) : base(info, context)
                    {
                    }
                }
                [Serializable]
                public class ImporteAPagarNegativo : CedFCIC.EX.Validaciones.Operaciones.Minutas.BaseApplicationException
                {
                    static string TextoError = "Las deducciones deben ser menor al capital más los intereses";
                    public ImporteAPagarNegativo() : base(TextoError)
                    {
                    }
                    public ImporteAPagarNegativo(Exception inner) : base(TextoError, inner)
                    {
                    }
                    public ImporteAPagarNegativo(SerializationInfo info, StreamingContext context) : base(info, context)
                    {
                    }
                }
                [Serializable]
                public class VentaTitulos : CedFCIC.EX.Validaciones.Operaciones.Minutas.BaseApplicationException
                {
                    static string TextoError = "La cantidad(VN) disponible no es suficiente: ";
                    public VentaTitulos(decimal Cantidad) : base(TextoError + Cantidad)
                    {
                    }
                    public VentaTitulos(Exception inner) : base(TextoError, inner)
                    {
                    }
                    public VentaTitulos(SerializationInfo info, StreamingContext context) : base(info, context)
                    {
                    }
                }
                [Serializable]
                public class DifCamEnCtaPesos : CedFCIC.EX.Validaciones.Operaciones.Minutas.BaseApplicationException
                {
                    static string TextoError = "Los movimientos de diferencia de cambio no se pueden aplicar a rubros en pesos";
                    public DifCamEnCtaPesos() : base(TextoError)
                    {
                    }
                    public DifCamEnCtaPesos(Exception inner) : base(TextoError, inner)
                    {
                    }
                    public DifCamEnCtaPesos(SerializationInfo info, StreamingContext context) : base(info, context)
                    {
                    }
                }
                [Serializable]
                public class DifCamTipoMovInvalido : CedFCIC.EX.Validaciones.Operaciones.Minutas.BaseApplicationException
                {
                    static string TextoError = "Tipo de movimiento invalido en operacion de Diferencia de Cambio";
                    public DifCamTipoMovInvalido() : base(TextoError)
                    {
                    }
                    public DifCamTipoMovInvalido(Exception inner) : base(TextoError, inner)
                    {
                    }
                    public DifCamTipoMovInvalido(SerializationInfo info, StreamingContext context) : base(info, context)
                    {
                    }
                }
            }
        }
        namespace Cuentas
        {
            [Serializable]
            public class BaseApplicationException : CedFCIC.EX.Validaciones.BaseApplicationException
            {
                public BaseApplicationException(string TextoError) : base(TextoError)
                {
                }
                public BaseApplicationException(string TextoError, Exception inner) : base(TextoError, inner)
                {
                }
                public BaseApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class Duplicada : CedFCIC.EX.Validaciones.Cuentas.BaseApplicationException
            {
                static string TextoError = "No se pudo dar de alta esa cuenta porque ya está cargada.";
                public Duplicada() : base(TextoError)
                {
                }
                public Duplicada(Exception inner) : base(TextoError, inner)
                {
                }
                public Duplicada(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            public class IdCuentaDuplicado : CedFCIC.EX.Validaciones.Cuentas.BaseApplicationException
            {
                static string TextoError = "El nuevo Id. de cuenta generaría valores duplicados";
                public IdCuentaDuplicado() : base(TextoError)
                {
                }
                public IdCuentaDuplicado(Exception inner) : base(TextoError, inner)
                {
                }
                public IdCuentaDuplicado(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class TasaDuplicada : CedFCIC.EX.Validaciones.Cuentas.BaseApplicationException
            {
                static string TextoError = "Esa tasa ya existe en el sistema.";
                public TasaDuplicada() : base(TextoError)
                {
                }
                public TasaDuplicada(Exception inner) : base(TextoError, inner)
                {
                }
                public TasaDuplicada(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
        }
    }
    namespace Sesion
    {
        [Serializable]
        public class BaseApplicationException : Exception
        {
            public BaseApplicationException(string TextoError) : base(TextoError)
            {
            }
            public BaseApplicationException(string TextoError, Exception inner) : base(TextoError, inner)
            {
            }
            public BaseApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class Crear : Exception
        {
            static string TextoError = "No se puede crear una sesion de trabajo";
            public Crear() : base(TextoError)
            {
            }
            public Crear(Exception inner) : base(TextoError, inner)
            {
            }
            public Crear(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class AplicInvalida : Exception
        {
            static string TextoError = "Codigo de aplicación inválido";
            public AplicInvalida() : base(TextoError)
            {
            }
            public AplicInvalida(Exception inner) : base(TextoError, inner)
            {
            }
            public AplicInvalida(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ParametroInexistente : Exception
        {
            static string TextoError = "Parámetro AppConfig inexistente: ";
            public ParametroInexistente(string NombreParametro) : base(TextoError + NombreParametro)
            {
            }
            public ParametroInexistente(Exception inner) : base(TextoError, inner)
            {
            }
            public ParametroInexistente(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        namespace Usuario
        {
            [Serializable]
            public class NoHabilitado : Exception
            {
                static string TextoError = "Usuario no habilitado";
                public NoHabilitado() : base(TextoError)
                {
                }
                public NoHabilitado(Exception inner) : base(TextoError, inner)
                {
                }
                public NoHabilitado(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class NoActivo : Exception
            {
                static string TextoError = "Usuario no activo";
                public NoActivo() : base(TextoError)
                {
                }
                public NoActivo(Exception inner) : base(TextoError, inner)
                {
                }
                public NoActivo(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }
            [Serializable]
            public class DeBaja : Exception
            {
                static string TextoError = "Usuario dado de baja";
                public DeBaja() : base(TextoError)
                {
                }
                public DeBaja(Exception inner) : base(TextoError, inner)
                {
                }
                public DeBaja(SerializationInfo info, StreamingContext context) : base(info, context)
                {
                }
            }

        }
    }
    namespace Archivo
    {
        [Serializable]
        public class ProcesarArchivo : Exception
        {
            static string TextoError = "Problema procesando archivo";
            public ProcesarArchivo() : base(TextoError)
            {
            }
            public ProcesarArchivo(string msg) : base(TextoError + ": " + msg)
            {
            }
            public ProcesarArchivo(Exception inner) : base(TextoError, inner)
            {
            }
            public ProcesarArchivo(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ArchivoInconsistente : Exception
        {
            static string TextoError = "Archivo inconsistente";
            public ArchivoInconsistente() : base(TextoError)
            {
            }
            public ArchivoInconsistente(string msg) : base(TextoError + ": " + msg)
            {
            }
            public ArchivoInconsistente(Exception inner) : base(TextoError, inner)
            {
            }
            public ArchivoInconsistente(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class ArchivoInexistente : Exception
        {
            static string TextoError = "Archivo inexistente";
            public ArchivoInexistente() : base(TextoError)
            {
            }
            public ArchivoInexistente(string NombreArchivo) : base(TextoError + ": " + NombreArchivo)
            {
            }
            public ArchivoInexistente(Exception inner) : base(TextoError, inner)
            {
            }
            public ArchivoInexistente(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
    namespace XML
    {
        [Serializable]
        public class Transformacion : Exception
        {
            static string TextoError = "Transformación fallida";
            public Transformacion() : base(TextoError)
            {
            }
            public Transformacion(Exception inner) : base(TextoError, inner)
            {
            }
            public Transformacion(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
    namespace Login
    {
        [Serializable]
        public class AutenticacionInvalida : Exception
        {
            static string TextoError = "Tipo de autenticacion invalida.  Corrija el archivo de configuración.";
            public AutenticacionInvalida() : base(TextoError)
            {
            }
            public AutenticacionInvalida(Exception inner) : base(TextoError, inner)
            {
            }
            public AutenticacionInvalida(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class IdUsuarioNoInfo : Exception
        {
            static string TextoError = "Nombre del usuario no informado";
            public IdUsuarioNoInfo() : base(TextoError)
            {
            }
            public IdUsuarioNoInfo(Exception inner) : base(TextoError, inner)
            {
            }
            public IdUsuarioNoInfo(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class PasswordNoInfo : Exception
        {
            static string TextoError = "Contraseña no informada";
            public PasswordNoInfo() : base(TextoError)
            {
            }
            public PasswordNoInfo(Exception inner) : base(TextoError, inner)
            {
            }
            public PasswordNoInfo(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class PasswordNoMatch : Exception
        {
            static string TextoError = "Contraseña incorrecta";
            public PasswordNoMatch() : base(TextoError)
            {
            }
            public PasswordNoMatch(Exception inner) : base(TextoError, inner)
            {
            }
            public PasswordNoMatch(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class UsuarioRevocado : Exception
        {
            static string TextoError = "El código de usuario ha sido revocado";
            public UsuarioRevocado() : base(TextoError)
            {
            }
            public UsuarioRevocado(Exception inner) : base(TextoError, inner)
            {
            }
            public UsuarioRevocado(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class Indefinido : Exception
        {
            static string TextoError = "Problema en el login";
            public Indefinido()
                : base(TextoError)
            {
            }
            public Indefinido(int CodError)
                : base(TextoError + ":" + CodError)
            {
            }
            public Indefinido(Exception inner)
                : base(TextoError, inner)
            {
            }
            public Indefinido(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class NoInformado : Exception
        {
            static string TextoError = "Usuario no informado";
            public NoInformado()
                : base(TextoError)
            {
            }
            public NoInformado(Exception inner)
                : base(TextoError, inner)
            {
            }
            public NoInformado(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class UsuarioInvalido : Exception
        {
            static string TextoError = "El código de usuario es inválido";
            public UsuarioInvalido() : base(TextoError)
            {
            }
            public UsuarioInvalido(Exception inner) : base(TextoError, inner)
            {
            }
            public UsuarioInvalido(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class UsuarioExpirado : Exception
        {
            static string TextoError = "El código de usuario ha expirado";
            public UsuarioExpirado() : base(TextoError)
            {
            }
            public UsuarioExpirado(Exception inner) : base(TextoError, inner)
            {
            }
            public UsuarioExpirado(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
        [Serializable]
        public class DestinoInvalido : Exception
        {
            static string TextoError = "El destino es inválido";
            public DestinoInvalido() : base(TextoError)
            {
            }
            public DestinoInvalido(Exception inner) : base(TextoError, inner)
            {
            }
            public DestinoInvalido(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
    namespace Aplicacion
    {
        [Serializable]
        public class AssemblyVersionInvalida : CedFCIC.EX.Sesion.BaseApplicationException
        {
            static string TextoError = "Versión desactualizada";
            public AssemblyVersionInvalida(string VersionNoActualizada, string VersionVigente)
                : base("La versión que se esta ejecutando (" + VersionNoActualizada + ") no está actualizada.\r\nSolicite la instalación del release más actualizado de la  versión " + VersionVigente + " en el servidor " + System.Net.Dns.GetHostName())
            {
            }
            public AssemblyVersionInvalida(Exception inner) : base(TextoError, inner)
            {
            }
            public AssemblyVersionInvalida(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
    namespace WS
    {
        [Serializable]
        public class MensajeErrorWSCP : Exception
        {
            static string TextoError = "Error en WS: ";
            public MensajeErrorWSCP()
                : base(TextoError)
            {
            }
            public MensajeErrorWSCP(string Elemento) : base(TextoError + Elemento)
            {
            }
            public MensajeErrorWSCP(Exception inner)
                : base(TextoError, inner)
            {
            }
            public MensajeErrorWSCP(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
    }
    namespace Permiso
    {
        [Serializable]
        public class BaseApplicationException : Exception
        {
            public BaseApplicationException(string TextoError)
                : base(TextoError)
            {
            }
            public BaseApplicationException(string TextoError, Exception inner)
                : base(TextoError, inner)
            {
            }
            public BaseApplicationException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class Existente : CedFCIC.EX.Permiso.BaseApplicationException
        {
            static string TextoError = "Este permiso ya ha sido solicitado y está en estado ";
            public Existente(string estado)
                : base(TextoError + " '" + estado + "'")
            {
            }
            public Existente(Exception inner)
                : base(TextoError, inner)
            {
            }
            public Existente(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
    }

    namespace Cuit
    {
        [Serializable]
        public class BaseApplicationException : Exception
        {
            public BaseApplicationException(string TextoError)
                : base(TextoError)
            {
            }
            public BaseApplicationException(string TextoError, Exception inner)
                : base(TextoError, inner)
            {
            }
            public BaseApplicationException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class NingunServicioSeleccionado : CedFCIC.EX.Cuit.BaseApplicationException
        {
            static string TextoError = "Servicio no informado (debe elegir al menos uno)";
            public NingunServicioSeleccionado()
                : base(TextoError)
            {
            }
            public NingunServicioSeleccionado(Exception inner)
                : base(TextoError, inner)
            {
            }
            public NingunServicioSeleccionado(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
        [Serializable]
        public class NingunDestinoComprobanteSeleccionado : CedFCIC.EX.Cuit.BaseApplicationException
        {
            static string TextoError = "Destino de Comprobante no informado (debe elegir al menos uno)";
            public NingunDestinoComprobanteSeleccionado()
                : base(TextoError)
            {
            }
            public NingunDestinoComprobanteSeleccionado(Exception inner)
                : base(TextoError, inner)
            {
            }
            public NingunDestinoComprobanteSeleccionado(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
    }

}

