using CentroComercial.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CentroComercial.CapaLogica
{
    public class GestionarAlquiler
    {
        public List<Alquiler> ListarAlquileres()
        {
            List<Alquiler> lista = new List<Alquiler>();
            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Alquileres", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Alquiler
                            {
                                ID_Alquiler = Convert.ToInt32(dr["ID_Alquiler"]),
                                ID_Local = Convert.ToInt32(dr["ID_Local"]),
                                ID_Usuario = Convert.ToInt32(dr["ID_Usuario"]),
                                Fecha_Inicio = Convert.ToDateTime(dr["Fecha_Inicio"]),
                                Fecha_Fin = Convert.ToDateTime(dr["Fecha_Fin"]),
                                Monto = Convert.ToDecimal(dr["Monto"])
                            });
                        }
                    }
                    return lista;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar los alquileres", ex);
                }
            }
        }
        public Alquiler ObtenerAlquiler(int ID_Alquiler)
        {
            Alquiler alquilerEntidad = new Alquiler();

            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GestionarAlquileres", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accion", "consultar");
                    cmd.Parameters.AddWithValue("@ID_Alquiler", ID_Alquiler);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            alquilerEntidad.ID_Alquiler = Convert.ToInt32(dr["ID_Alquiler"]);
                            alquilerEntidad.ID_Local = Convert.ToInt32(dr["ID_Local"]);
                            alquilerEntidad.ID_Usuario = Convert.ToInt32(dr["ID_Usuario"]);
                            alquilerEntidad.Fecha_Inicio = Convert.ToDateTime(dr["Fecha_Inicio"]);
                            alquilerEntidad.Fecha_Fin = Convert.ToDateTime(dr["Fecha_Fin"]);
                            alquilerEntidad.Monto = Convert.ToDecimal(dr["Monto"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener alquiler", ex);
                }
                finally
                {
                    conn.Close();
                }
                return alquilerEntidad;
            }
        }
        public int InsertarAlquiler(Alquiler alquiler)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GestionarAlquileres", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accion", "agregar");
                    cmd.Parameters.AddWithValue("@ID_Local", alquiler.ID_Local);
                    cmd.Parameters.AddWithValue("@ID_Usuario", alquiler.ID_Usuario);
                    cmd.Parameters.AddWithValue("@Fecha_Inicio", alquiler.Fecha_Inicio);
                    cmd.Parameters.AddWithValue("@Fecha_Fin", alquiler.Fecha_Fin);
                    cmd.Parameters.AddWithValue("@Monto", alquiler.Monto);

                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0) result = 1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al crear alquiler", ex);
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }

        public int ActualizarAlquiler(Alquiler alquiler)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GestionarAlquileres", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accion", "modificar");
                    cmd.Parameters.AddWithValue("@ID_Alquiler", alquiler.ID_Alquiler);
                    cmd.Parameters.AddWithValue("@ID_Local", alquiler.ID_Local);
                    cmd.Parameters.AddWithValue("@ID_Usuario", alquiler.ID_Usuario);
                    cmd.Parameters.AddWithValue("@Fecha_Inicio", alquiler.Fecha_Inicio);
                    cmd.Parameters.AddWithValue("@Fecha_Fin", alquiler.Fecha_Fin);
                    cmd.Parameters.AddWithValue("@Monto", alquiler.Monto);

                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0) result = 1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar alquiler", ex);
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }

        public int EliminarAlquiler(int ID_Alquiler)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(DBcon.conexion))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GestionarAlquileres", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accion", "borrar");
                    cmd.Parameters.AddWithValue("@ID_Alquiler", ID_Alquiler);

                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0) result = 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al eliminar alquiler: {ex.Message}");
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