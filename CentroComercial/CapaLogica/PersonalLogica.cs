using CentroComercial.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CentroComercial.CapaLogica
{
    public class PersonalLogica
    {
        public List<Personal> ListarPersonal()
        {
            List<Personal> listaPersonal = new List<Personal>();
            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ID_Personal, Nombre, Apellido, Cargo FROM Personal", conn);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaPersonal.Add(new Personal
                            {
                                ID_Personal = Convert.ToInt32(dr["ID_Personal"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Cargo = dr["Cargo"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar el personal: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
            return listaPersonal;
        }
    }
}