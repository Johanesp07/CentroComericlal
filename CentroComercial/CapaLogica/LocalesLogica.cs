using CentroComercial.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CentroComercial.CapaLogica
{
    public class LocalesLogica
    {
        public List<Local> ListarLocales()
        {
            List<Local> listaLocales = new List<Local>();
            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ID_Local, Numero_Local, Tamaño, Ubicacion, Estado FROM Locales", conn);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaLocales.Add(new Local
                            {
                                ID_Local = Convert.ToInt32(dr["ID_Local"]),
                                Numero_Local = Convert.ToInt32(dr["Numero_Local"]),
                                Tamaño = Convert.ToDecimal(dr["Tamaño"]),
                                Ubicacion = dr["Ubicacion"].ToString(),
                                Estado = dr["Estado"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar los locales: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
            return listaLocales;
        }
    }
}