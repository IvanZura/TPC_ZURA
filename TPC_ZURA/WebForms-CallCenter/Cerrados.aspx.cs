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
    public partial class Cerrados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuarios"] == null)
            {
                Response.Redirect("/Login");
            }
        }

        [WebMethod]
        public static List<Reclamo> VerTodos()
        {
            ReclamosNegocio negocio = new ReclamosNegocio();
            ClientesNegocio negocioCliente = new ClientesNegocio();
            Usuarios usuario = (Usuarios)HttpContext.Current.Session["Usuarios"];
            if (usuario == null)
            {
                HttpContext.Current.Response.Redirect("/Login");
            }
            Clientes cliente = negocioCliente.ExisteClienteWeb(usuario.DNI);
            return negocio.ListarReclamosCerradosPorCreador(cliente.idcliente);
        }
    }
}