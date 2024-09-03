using CentroComercial.CapaDatos;
using CentroComercial.CapaLogica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CentroComercial.CapaVista
{
    public partial class RegistrarAlquileres : System.Web.UI.Page
    {
        private static int ID_Alquiler = 0;
        GestionarAlquiler alquilerLogica = new GestionarAlquiler();
        LocalesLogica localesLogica = new LocalesLogica();
        GestionarUsuarios usuarioLogica = new GestionarUsuarios(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarLocales();
                CargarUsuarios();
                CargarAlquileres();
            }
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlIDLocal.SelectedValue) ||
                string.IsNullOrWhiteSpace(ddlIDUsuario.SelectedValue) ||
                string.IsNullOrWhiteSpace(txtFechaInicio.Text) ||
                string.IsNullOrWhiteSpace(txtFechaFin.Text) ||
                string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                Alertas("Por favor, complete todos los campos obligatorios.");
                return;
            }
            try
            {
                Alquiler alquiler = new Alquiler()
                {
                    ID_Alquiler = ID_Alquiler,
                    ID_Local = Convert.ToInt32(ddlIDLocal.SelectedValue),
                    ID_Usuario = Convert.ToInt32(ddlIDUsuario.SelectedValue),
                    Fecha_Inicio = Convert.ToDateTime(txtFechaInicio.Text),
                    Fecha_Fin = Convert.ToDateTime(txtFechaFin.Text),
                    Monto = Convert.ToDecimal(txtMonto.Text)
                };
                if (ID_Alquiler == 0)
                {
                    int resultado = alquilerLogica.InsertarAlquiler(alquiler);

                    if (resultado > 0)
                    {
                        string url = VirtualPathUtility.ToAbsolute("~/CapaVista/RegistrarAlquileres.aspx");
                        string script = $"alert('Alquiler registrado con éxito'); window.location.href='{url}';";
                        ClientScript.RegisterStartupScript(this.GetType(), "AlertRedirect", script, true);
                    }
                    else
                    {
                        Alertas("Error al registrar alquiler");
                    }
                }
            }
            catch (Exception ex)
            {
                Alertas("Ocurrió un error al procesar la solicitud: " + ex.Message);
            }
        }

        private void Alertas(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{mensaje}');", true);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int ID_Alquiler = Convert.ToInt32(ddlAlquileres.SelectedValue);
            btnActualizar.Visible = true;
            btnAgregar.Visible = false;
            if (ID_Alquiler != 0)
            {
                Alquiler alquiler = alquilerLogica.ObtenerAlquiler(ID_Alquiler);

                if (alquiler != null)
                {
                    ddlIDLocal.SelectedValue = alquiler.ID_Local.ToString();
                    ddlIDUsuario.SelectedValue = alquiler.ID_Usuario.ToString();
                    txtFechaInicio.Text = alquiler.Fecha_Inicio.ToString("yyyy-MM-dd");
                    txtFechaFin.Text = alquiler.Fecha_Fin.ToString("yyyy-MM-dd");
                    txtMonto.Text = alquiler.Monto.ToString();
                }
                else
                {
                    Alertas("No se encontró el alquiler seleccionado.");
                }
            }
            else
            {
                Alertas("Por favor, seleccione un alquiler válido.");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int ID_Alquiler = Convert.ToInt32(ddlAlquileres.SelectedValue);
            int respuesta = alquilerLogica.EliminarAlquiler(ID_Alquiler);

            if (respuesta > 0)
            {
                Alertas("El alquiler ha sido eliminado con éxito");
                CargarAlquileres();
            }
            else
            {
                Alertas("Error: No se pudo eliminar el alquiler");
            }
        }

        protected void CargarLocales()
        {
            List<Local> listaLocales = localesLogica.ListarLocales();
            ddlIDLocal.DataTextField = "Numero_Local";
            ddlIDLocal.DataValueField = "ID_Local";
            ddlIDLocal.DataSource = listaLocales;
            ddlIDLocal.DataBind();
            ddlIDLocal.Items.Insert(0, new ListItem("-- Seleccione Local --", ""));
        }

        protected void CargarUsuarios()
        {
            List<Usuario> listaUsuarios = usuarioLogica.ListarUsuarios();
            ddlIDUsuario.DataTextField = "Nombre_Usuario";
            ddlIDUsuario.DataValueField = "ID_Usuario";
            ddlIDUsuario.DataSource = listaUsuarios;
            ddlIDUsuario.DataBind();
            ddlIDUsuario.Items.Insert(0, new ListItem("-- Seleccione Usuario --", ""));
        }

        protected void CargarAlquileres()
        {
            List<Alquiler> listAlquileres = alquilerLogica.ListarAlquileres();
            ddlAlquileres.DataTextField = "ID_Alquiler";
            ddlAlquileres.DataValueField = "ID_Alquiler";
            ddlAlquileres.DataSource = listAlquileres;
            ddlAlquileres.DataBind();
        }

        protected void Actualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlIDLocal.SelectedValue) ||
                string.IsNullOrWhiteSpace(ddlIDUsuario.SelectedValue) ||
                string.IsNullOrWhiteSpace(txtFechaInicio.Text) ||
                string.IsNullOrWhiteSpace(txtFechaFin.Text) ||
                string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                Alertas("Por favor, complete todos los campos obligatorios.");
                return;
            }

            btnActualizar.Visible = false;
            btnAgregar.Visible = true;
            try
            {
                Alquiler alquiler = new Alquiler()
                {
                    ID_Alquiler = Convert.ToInt32(ddlAlquileres.SelectedValue),
                    ID_Local = Convert.ToInt32(ddlIDLocal.SelectedValue),
                    ID_Usuario = Convert.ToInt32(ddlIDUsuario.SelectedValue),
                    Fecha_Inicio = Convert.ToDateTime(txtFechaInicio.Text),
                    Fecha_Fin = Convert.ToDateTime(txtFechaFin.Text),
                    Monto = Convert.ToDecimal(txtMonto.Text)
                };
                if (alquiler.ID_Alquiler != 0)
                {
                    int resultado = alquilerLogica.ActualizarAlquiler(alquiler);

                    if (resultado > 0)
                    {
                        string url = VirtualPathUtility.ToAbsolute("~/CapaVista/RegistrarAlquileres.aspx");
                        string script = $"alert('Alquiler actualizado con éxito'); window.location.href='{url}';";
                        ClientScript.RegisterStartupScript(this.GetType(), "AlertRedirect", script, true);
                    }
                    else
                    {
                        Alertas("Error al actualizar alquiler");
                    }
                }
            }
            catch (Exception ex)
            {
                Alertas($"Error: {ex.Message}");
            }
        }
    }
}