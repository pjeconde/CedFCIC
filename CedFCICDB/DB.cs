using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace CedFCIC.DB
{
    public class db : IDisposable
    {
        protected Entidades.Sesion sesion = new CedFCIC.Entidades.Sesion();
        protected CultureInfo cedeiraCultura;
        private string[] sqls;
        private SqlConnection sqlConexion;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlTransaction sqlTransaccion;
        private bool usaTransaccion = false;
        private int i;
        private int[] cantReg;
        private DataSet ds;
        private DataView[] dv;
        private DataTable[] tb;

        public db(CedFCIC.Entidades.Sesion Sesion)
        {
            // Constructor
            this.sesion = Sesion;
            //cedeiraCultura = new CultureInfo(System.Configuration.ConfigurationManager.AppSettings["Cultura"]);
        }
        #region " Codigo de acceso a Base de datos "

        public enum TipoRetorno
        {
            None,
            CantReg,
            DV,
            DS,
            TB
        };
        public enum Transaccion
        {
            Acepta,
            NoAcepta,
            Usa
        };


        protected object Ejecutar(object Sql, TipoRetorno TipoRetorno, Transaccion Transaccion, string CnnStrDB)
        {
            /*
            ESTE ES EL UNICO METODO QUE ACCEDE A LA BASE DE DATOS Y NO DEBE SER PUBLICO PARA QUE NO SEA INVOCADO NI
            DESDE LA CAPA DE REGLA DE NEGOCIO NI DE LA DE PRESENTACION
                    Sql: 1) un string con una o mas instrucciones SQL.
                         2) un string array con uno o mas strings que contengan una o mas instrucciones SQL 
                            cada uno. 
            TipoRetorno: 1) None: se ejecuta como un comando y no devuelve ningun resultado
                         2) CantReg: se ejecuta como un comando y devuelve un integer, o un integer array 
                            (uno por cada Sql string array), indicando la cantidad registros se vieron afectados.
                         3) Dv: devuelve un dataview o un dataview array (cada uno con una tabla), 
                            segun los datos obtenidos de la DB.
                         4) ds: devuelve un dataset con una o mas tablas segun los datos obtenidos de la DB. 
            Transaccion: 1) Acepta: en el caso de recibir un Sql string array, activa la transaccion.
                         2) NoAcepta: no activa la trasaccion
                         3) Usa: activa la transaccion
            */
            switch (Sql.GetType().FullName.ToString())
            {
                case "System.String[]":
                    sqls = (string[])Sql;
                    break;
                default:	//case "System.String":
                    sqls = new String[] { (string)Sql };
                    break;
            }
            cantReg = new int[sqls.Length];
            try
            {
                using (sqlConexion = new SqlConnection(CnnStrDB))
                {
                    sqlConexion.Open();
                    switch (Transaccion)
                    {
                        case (Transaccion.Acepta):
                            usaTransaccion = (sqls.Length > 1);
                            break;
                        case (Transaccion.NoAcepta):
                            usaTransaccion = false;
                            break;
                        default: //(Transaccion.Usa):
                            usaTransaccion = true;
                            break;
                    }
                    if (usaTransaccion)
                        sqlTransaccion = sqlConexion.BeginTransaction();
                    switch (TipoRetorno)
                    {
                        case (TipoRetorno.None):
                        case (TipoRetorno.CantReg):
                            sqlCommand = new SqlCommand();
                            sqlCommand.Connection = sqlConexion;
                            sqlCommand.CommandTimeout = 90;
                            if (usaTransaccion)
                            {
                                sqlCommand.Transaction = sqlTransaccion;
                            }
                            for (i = 0; i < sqls.Length; i++)
                            {
                                sqlCommand.CommandText = sqls[i];
                                System.Diagnostics.Debug.WriteLine(sqlCommand.CommandText);
                                cantReg[i] = sqlCommand.ExecuteNonQuery();
                            }
                            break;
                        case (TipoRetorno.DS):
                        case (TipoRetorno.DV):
                        case (TipoRetorno.TB):
                            ds = new DataSet();
                            for (i = 0; i < sqls.Length; i++)
                            {
                                System.Diagnostics.Debug.WriteLine(sqls[i]);
                                sqlAdapter = new SqlDataAdapter(sqls[i], sqlConexion);
                                if (usaTransaccion)
                                {
                                    sqlAdapter.SelectCommand.Transaction = sqlTransaccion;
                                }
                                sqlAdapter.SelectCommand.CommandTimeout = 90;
                                if (i == 0)
                                {
                                    sqlAdapter.Fill(ds);
                                }
                                else
                                {
                                    ds.Tables.Add();
                                    sqlAdapter.Fill(ds.Tables[ds.Tables.Count - 1]);
                                }
                                sqlAdapter.Dispose();
                            }
                            switch (TipoRetorno)
                            {
                                case (TipoRetorno.DV):
                                    if (ds.Tables.Count > 0)
                                    {
                                        dv = new DataView[ds.Tables.Count];
                                        for (i = 0; i < ds.Tables.Count; i++)
                                        {
                                            dv[i] = ds.Tables[i].DefaultView;
                                        }
                                    }
                                    break;
                                case (TipoRetorno.TB):
                                    if (ds.Tables.Count > 0)
                                    {
                                        tb = new DataTable[ds.Tables.Count];
                                        for (i = 0; i < ds.Tables.Count; i++)
                                        {
                                            tb[i] = ds.Tables[i];
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;
                    }
                    if (usaTransaccion)
                    {
                        sqlTransaccion.Commit();
                    }
                    sqlConexion.Close();
                    switch (TipoRetorno)
                    {
                        case TipoRetorno.None:
                            return new System.Object();
                        case TipoRetorno.CantReg:
                            if (sqls.Length > 1)
                            {
                                return cantReg;
                            }
                            else
                            {
                                return cantReg[0];
                            }
                        case TipoRetorno.DS:
                            return ds;
                        case TipoRetorno.TB:
                            try
                            {
                                switch (ds.Tables.Count)
                                {
                                    case 0:
                                        return new DataTable();
                                    case 1:
                                        var valor = tb[0];
                                        return valor;
                                    default:
                                        return tb;
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                if (ds != null)
                                    ds.Dispose();
                            }

                        default: //case TipoRetorno.Dv:
                            try
                            {
                                switch (ds.Tables.Count)
                                {
                                    case 0:
                                        return new DataView();
                                    case 1:
                                        var valor = dv[0];
                                        ds.Dispose();
                                        return valor;
                                    default:
                                        return dv;
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                if (ds != null)
                                    ds.Dispose();
                            }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex1)
            {
                if (((System.Data.SqlClient.SqlException)(ex1)).Procedure == "ConnectionOpen (Connect()).")
                {
                    throw new CedFCIC.EX.db.Conexion(ex1);
                }
                else
                {
                    if (usaTransaccion)
                    {
                        try
                        {
                            sqlTransaccion.Rollback();
                            throw new CedFCIC.EX.db.EjecucionConRollback(ex1);
                        }
                        catch (CedFCIC.EX.db.EjecucionConRollback)
                        {
                            throw new CedFCIC.EX.db.EjecucionConRollback(ex1);
                        }
                        catch
                        {
                            throw new CedFCIC.EX.db.Rollback(ex1);
                        }
                    }
                    else
                    {
                        throw new CedFCIC.EX.db.Ejecucion(ex1);
                    }
                }
            }
        }
        public void TesteoConexion(string CnnStrDB)
        {
            try
            {
                sqlConexion = new SqlConnection(CnnStrDB);
                sqlConexion.Open();
                sqlConexion.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new CedFCIC.EX.db.Conexion(ex);
            }
        }
        #endregion

        public DateTime FechaDB
        {
            get
            {
                DateTime valor;
                using (DataView dv = (DataView)Ejecutar("select getdate()", TipoRetorno.DV, Transaccion.NoAcepta, sesion.CnnStr))
                {
                    valor = (DateTime)dv.Table.Rows[0][0];
                }
                return valor;
            }
        }
        public static string ByteArray2TimeStamp(byte[] ts)
        {
            string a = "0x";
            for (int i = 0; i < ts.Length; i++)
            {
                if (ts[i].ToString("X").Length == 1)
                {
                    a += "0" + ts[i].ToString("X");
                }
                else
                {
                    a += ts[i].ToString("X");
                }
            }
            return a;
        }
        public static byte[] TimeStamp2ByteArray(string value)
        {
            byte[] b = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                string a = value.Substring(i * 2 + 2, 2);
                b[i] = Convert.ToByte(a, 16);
            }
            return b;
        }
        public DataTable CamposHabilitados_qry(string Tabla, string ListaCamposHabilitados)
        {
            string a;
            if (ListaCamposHabilitados.Trim() != String.Empty)
            {
                a = "select " + ListaCamposHabilitados + " from " + Tabla + " where 0=1";
            }
            else
            {
                a = "select null from " + Tabla + " where 0=1";
            }
            return (DataTable)Ejecutar(
                a,
                TipoRetorno.TB,
                Transaccion.NoAcepta,
                sesion.CnnStr);
        }
        public DataTable InfoCamposHabilitados_qry(string Tabla, DataTable CamposHabilitados)
        {
            string a = "and syscolumns.name in (";
            for (int i = 0; i < CamposHabilitados.Columns.Count; i++)
            {
                a += "'" + CamposHabilitados.Columns[i].ColumnName + "', ";
            }
            a = "select syscolumns.name Propiedad,systypes.name Tipo,syscolumns.length Long,syscolumns.xprec-syscolumns.xscale Enteros, syscolumns.xscale Decimales, isnull(sysproperties.value, syscolumns.name) Descripcion from syscolumns, systypes, sysproperties where syscolumns.id=(select id from dbo.sysobjects where name='" + Tabla + "') and systypes.xtype=syscolumns.xtype and syscolumns.id*=sysproperties.id and syscolumns.colid*=sysproperties.smallid " +
                a.Substring(0, a.Length - 2) + ")";
            return (DataTable)Ejecutar(
                a,
                TipoRetorno.TB,
                Transaccion.NoAcepta,
                sesion.CnnStr);
        }
        public DataTable InfoCampos_qry(string NombreTabla)
        {
            string a = "select syscolumns.name Propiedad,systypes.name Tipo,syscolumns.length Long,syscolumns.xprec-syscolumns.xscale Enteros, syscolumns.xscale Decimales, isnull(sysproperties.value, syscolumns.name) Descripcion from syscolumns, systypes, sysproperties where syscolumns.id=(select id from dbo.sysobjects where name='" + NombreTabla + "') and systypes.xtype=syscolumns.xtype and syscolumns.id*=sysproperties.id and syscolumns.colid*=sysproperties.smallid";
            return (DataTable)Ejecutar(a, TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
        }
        public void DepurarFila(string NombreTabla, string Id)
        {
            string a = "delete " + NombreTabla + " where Id" + NombreTabla + "='" + Id + "'";
            Ejecutar(
                a,
                TipoRetorno.None,
                Transaccion.NoAcepta,
                sesion.CnnStr);
        }
        public string SevidoryDB()
        {
            return SevidoryDB(sesion.CnnStr);
        }
        public string SevidoryDB(string CnnStrDB)
        {
            try
            {
                sqlConexion = new SqlConnection(CnnStrDB);
                return sqlConexion.DataSource + "." + sqlConexion.Database;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new CedFCIC.EX.db.Conexion(ex);
            }
        }
        public string ReemplazarApostrofos(string cadena)
        {
            string aux = cadena.Replace("'", "''");
            return aux;
        }

        public void Dispose()
        {

            this.ds = null;
            this.dv = null;
            this.tb = null;
            if (this.sqlAdapter != null) this.sqlAdapter.Dispose();
            if (this.sqlCommand != null) this.sqlCommand.Dispose();
            if (this.sqlConexion != null) this.sqlConexion.Dispose();
            if (this.sqlTransaccion != null) this.sqlTransaccion.Dispose();
            if (this.cedeiraCultura != null) this.cedeiraCultura = null;
            this.sqls = null;
        }
    }
}
