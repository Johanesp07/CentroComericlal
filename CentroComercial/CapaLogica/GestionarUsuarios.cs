using CentroComercial.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CentroComercial.CapaLogica
{
    public class GestionarUsuarios
    {
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario
                            {
                                ID_Usuario = Convert.ToInt32(dr["ID_Usuario"]),
                                Nombre_Usuario = dr["Nombre_Usuario"].ToString(),
                                Contrasena = dr["Contrasena"].ToString(),
                                Rol = dr["Rol"].ToString(),
                                ID_Personal = dr["ID_Personal"] != DBNull.Value ? Convert.ToInt32(dr["ID_Personal"]) : (int?)null
                            });
                        }
                    }
                    return lista;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar los usuarios", ex);
                }
            }
        }
        public Usuario ObtenerUsuario(int ID_Usuario)
        {
            Usuario usuarioEntidad = new Usuario();

            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GestionarUsuarios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accion", "consultar");
                    cmd.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuarioEntidad.ID_Usuario = Convert.ToInt32(dr["ID_Usuario"]);
                            usuarioEntidad.Nombre_Usuario = dr["Nombre_Usuario"].ToString();
                            usuarioEntidad.Contrasena = dr["Contrasena"].ToString();
                            usuarioEntidad.Rol = dr["Rol"].ToString();
                            usuarioEntidad.ID_Personal = dr["ID_Personal"] != DBNull.Value ? Convert.ToInt32(dr["ID_Personal"]) : (int?)null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener usuario", ex);
                }
                finally
                {
                    conn.Close();
                }
                return usuarioEntidad;
            }
        }
        public int InsertarUsuario(Usuario usuario)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GestionarUsuarios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accion", "agregar");
                    cmd.Parameters.AddWithValue("@Nombre_Usuario", usuario.Nombre_Usuario);
                    cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("@ID_Personal", (object)usuario.ID_Personal ?? DBNull.Value);

                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0) result = 1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al crear usuario", ex);
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }
        public int ActualizarUsuario(Usuario usuario)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GestionarUsuarios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accion", "modificar");
                    cmd.Parameters.AddWithValue("@ID_Usuario", usuario.ID_Usuario);
                    cmd.Parameters.AddWithValue("@Nombre_Usuario", usuario.Nombre_Usuario);
                    cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("@ID_Personal", (object)usuario.ID_Personal ?? DBNull.Value);

                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0) result = 1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar usuario", ex);
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }

        public int EliminarUsuario(int ID_Usuario)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GestionarUsuarios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accion", "borrar");
                    cmd.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);

                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0) result = 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }
    }
}