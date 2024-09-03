using CentroComercial.CapaDatos;
using CentroComercial.CapaLogica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CentroComercial.CapaVista.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginLogica loginLogica = new LoginLogica();
            Usuario usuarios = loginLogica.ValidarLogin(txtUsername.Text, txtpassword.Text);
            if (usuarios != null)
            {
                Response.Redirect("~/CapaVista/Inicio.aspx");
            }
            else
            {
                Alertas("Credenciales incorrectas. Inténtalo de nuevo.");
            }
        }
        private void Alertas(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{mensaje}');", true);
        }
    }
}