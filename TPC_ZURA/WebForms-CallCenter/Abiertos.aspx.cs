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
    public partial class Abiertos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<Reclamo> VerTodos()
        {
            ReclamosNegocio negocio = new ReclamosNegocio();
            Usuarios usuario = (Usuarios)HttpContext.Current.Session["Usuarios"];
            return negocio.ListarReclamosPorCreador(usuario.id);
        }
    }
}