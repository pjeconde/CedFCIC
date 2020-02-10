using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PruebaCore;

namespace PruebaCore.DB
{
    public class DB
    {
        public enum TipoRetorno
        {
            None,
            CantReg,
            DV,
            DS,
            TB
        };

        SqlConnection con = new SqlConnection("Server=.\\sqlexpress;Database=CedeiraServicios;Trusted_Connection=True;MultipleActiveResultSets=true");
        DataSet ds;
        internal string EjecutarQuery(out DataSet ds, string query, TipoRetorno tipoRetorno)
        {
            ds = new DataSet();
            try
            {
                con.Open();
                switch (tipoRetorno)
                {
                    case (TipoRetorno.None):
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        break;
                    case (TipoRetorno.DS):
                        
                        SqlDataAdapter sda = new SqlDataAdapter(query, con);
                        sda.Fill(ds);
                        break;
                }
                con.Close();
                return ("Data save Successfully");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }
    }
}
