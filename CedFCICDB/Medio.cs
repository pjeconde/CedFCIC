using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using CedFCIC;

namespace CedFCIC.DB
{
    public class Medio : db
    {
        public Medio(Entidades.Sesion Sesion) : base(Sesion)
        {
        }
        public List<Entidades.Medio> LeerLista()
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.Append("select IdMedio, DescrMedio from Medio order by DescrMedio ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            List<Entidades.Medio> lista = new List<Entidades.Medio>();
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entidades.Medio elem = new Entidades.Medio();
                    Copiar(dt.Rows[i], elem);
                    lista.Add(elem);
                }
            }
            return lista;
        }
        private void Copiar(DataRow Desde, Entidades.Medio Hasta)
        {
            Hasta.IdMedio = Convert.ToString(Desde["IdMedio"]);
            Hasta.DescrMedio = Convert.ToString(Desde["DescrMedio"]);
        }

        //public string InsertarMedio(Entidades.Medio medio)
        //{
        //    string Resp = "";
        //    DB db = new DB();
        //    Resp = db.EjecutarQuery(out ds, "Insert Medio (IdMedio, DescrMedio) values ('" + medio.IdMedio + "', '" + medio.DescrMedio + "')", DB.TipoRetorno.None);
        //    return Resp;
        //}

        //public Entidades.Medio ConsultaMedio(string IdMedio, out string Resp)
        //{
        //    DB db = new DB();
        //    Resp = db.EjecutarQuery(out ds, "Select * from Medio where IdMedio = '" + IdMedio + "'", DB.TipoRetorno.DS);
        //    Entidades.Medio medio = CopiarDSaMedio(ds);
        //    return medio;
        //}
        //private Entidades.Medio CopiarDSaMedio(DataSet ds)
        //{
        //    Entidades.Medio m = new Entidades.Medio();
        //    m.IdMedio = ds.Tables[0].Rows[0]["IdMedio"].ToString();
        //    m.DescrMedio = ds.Tables[0].Rows[0]["DescrMedio"].ToString();
        //    return m;
        //}
        //public List<Entidades.Medio> ConsultaListaMedio()
        //{
        //    DB db = new DB();
        //    string a = db.EjecutarQuery(out ds, "Select * from Medio", DB.TipoRetorno.DS);
        //    List<Entidades.Medio> listaMedio = CopiarDSaListaMedio(ds);
        //    return listaMedio;
        //}
        //private List<Entidades.Medio> CopiarDSaListaMedio(DataSet ds)
        //{
        //    List<Entidades.Medio> lm = new List<Entidades.Medio>();
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        Entidades.Medio m = new Entidades.Medio();
        //        m.IdMedio = ds.Tables[0].Rows[i]["IdMedio"].ToString();
        //        m.DescrMedio = ds.Tables[0].Rows[i]["DescrMedio"].ToString();
        //        lm.Add(m);
        //    }
        //    return lm;
        //}
    }
}
