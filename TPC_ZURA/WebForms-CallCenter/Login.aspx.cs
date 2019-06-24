using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Dominio;
using Negocio;

namespace WebForms_CallCenter
{
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (HttpContext.Current.Session["voucher"])
            //{

            //}
            if (Session["Usuarios"] != null)
            {
                Response.Redirect("/");
            }
        }

        [WebMethod]
        public static Usuarios LoginUsu(string usu, string pass)
        {
            UsuariosNegocio negocio = new UsuariosNegocio();
            Usuarios local = negocio.LoginUsuario(usu, pass);
            if (local.id != 0)
            {
                HttpContext.Current.Session["Usuarios"] = local;
            }
            return local;
        }

    }
}