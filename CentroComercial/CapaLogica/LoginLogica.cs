using CentroComercial.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CentroComercial.CapaLogica
{
    public class LoginLogica
    {
        public Usuario ValidarLogin(string nombreUsuario, string contrasena)
        {
            Usuario usuario = null;
            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("ValidarLogin", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre_Usuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new Usuario
                            {
                                ID_Usuario = Convert.ToInt32(dr["ID_Usuario"]),
                                Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                                Contrasena = dr["Contrasena"].ToString(),
                                Rol = dr["Rol"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al validar login: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
            return usuario;
        }
    }
}