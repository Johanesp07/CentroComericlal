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
    public partial class RegistrarUsuarios : System.Web.UI.Page
    {
        private static int ID_Usuario = 0;
        GestionarUsuarios gestionarUsuarios = new GestionarUsuarios();
        PersonalLogica personalLogica = new PersonalLogica();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarPersonal();
                CargarUsuarios();
            }
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtRol.Text) ||
                string.IsNullOrWhiteSpace(ddlPersonal.SelectedValue))
            {
                Alertas("Por favor, complete todos los campos obligatorios.");
                return;
            }
            try
            {
                Usuario usuario = new Usuario()
                {
                    ID_Usuario = ID_Usuario,
                    Nombre_Usuario = txtNombre.Text,
                    Contrasena = txtPassword.Text,
                    Rol = txtRol.Text,
                    ID_Personal = string.IsNullOrEmpty(ddlPersonal.SelectedValue) ? (int?)null : Convert.ToInt32(ddlPersonal.SelectedValue)
                };
                if (ID_Usuario == 0)
                {
                    int resultado = gestionarUsuarios.InsertarUsuario(usuario);

                    if (resultado > 0)
                    {
                        string url = VirtualPathUtility.ToAbsolute("~/CapaVista/RegistrarUsuarios.aspx");
                        string script = $"alert('Usuario ingresado con éxito'); window.location.href='{url}';";
                        ClientScript.RegisterStartupScript(this.GetType(), "AlertRedirect", script, true);
                    }
                    else
                    {
                        Alertas("Error al ingresar usuario");
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
            int ID_Usuario = Convert.ToInt32(ddlUsuarios.SelectedValue);
            btnActualizar.Visible = true;
            btnAgregar.Visible = false;
            if (ID_Usuario != 0)
            {
                Usuario usuario = gestionarUsuarios.ObtenerUsuario(ID_Usuario);

                if (usuario != null)
                {
                    txtNombre.Text = usuario.Nombre_Usuario;
                    txtPassword.Text = usuario.Contrasena;
                    txtRol.Text = usuario.Rol;
                    ddlPersonal.SelectedValue = usuario.ID_Personal?.ToString() ?? "";
                }
                else
                {
                    Alertas("No se encontró al usuario seleccionado.");
                }
            }
            else
            {
                Alertas("Por favor, seleccione un usuario válido.");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int ID_Usuario = Convert.ToInt32(ddlUsuarios.SelectedValue);
            int respuesta = gestionarUsuarios.EliminarUsuario(ID_Usuario);

            if (respuesta > 0)
            {
                Alertas("El usuario ha sido eliminado con éxito");
                CargarUsuarios();
            }
            else
            {
                Alertas("Error: No se pudo eliminar el usuario");
            }
        }

        protected void CargarPersonal()
        {
            List<Personal> listaPersonal = personalLogica.ListarPersonal();
            ddlPersonal.DataTextField = "Nombre";
            ddlPersonal.DataValueField = "ID_Personal";
            ddlPersonal.DataSource = listaPersonal;
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem("-- Seleccione Personal --", ""));
        }

        protected void CargarUsuarios()
        {
            List<Usuario> listUsuarios = gestionarUsuarios.ListarUsuarios();
            ddlUsuarios.DataTextField = "Nombre_Usuario";
            ddlUsuarios.DataValueField = "ID_Usuario";
            ddlUsuarios.DataSource = listUsuarios;
            ddlUsuarios.DataBind();
        }

        protected void Actualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtRol.Text))
            {
                Alertas("Por favor, complete todos los campos obligatorios.");
                return;
            }

            btnActualizar.Visible = false;
            btnAgregar.Visible = true;
            try
            {
                Usuario usuario = new Usuario()
                {
                    ID_Usuario = Convert.ToInt32(ddlUsuarios.SelectedValue),
                    Nombre_Usuario = txtNombre.Text,
                    Contrasena = txtPassword.Text,
                    Rol = txtRol.Text,
                    ID_Personal = string.IsNullOrEmpty(ddlPersonal.SelectedValue) ? (int?)null : Convert.ToInt32(ddlPersonal.SelectedValue)
                };
                if (usuario.ID_Usuario != 0)
                {
                    int resultado = gestionarUsuarios.ActualizarUsuario(usuario);

                    if (resultado > 0)
                    {
                        string url = VirtualPathUtility.ToAbsolute("~/CapaVista/RegistrarUsuarios.aspx");
                        string script = $"alert('Usuario actualizado con éxito'); window.location.href='{url}';";
                        ClientScript.RegisterStartupScript(this.GetType(), "AlertRedirect", script, true);
                    }
                    else
                    {
                        Alertas("Error al actualizar usuario");
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