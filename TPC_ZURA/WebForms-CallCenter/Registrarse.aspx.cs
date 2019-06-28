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
    public partial class Registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Clientes ExisteDNI(string DNI)
        {
            ClientesNegocio negocio = new ClientesNegocio();
            Clientes cliente = negocio.ExisteClienteWeb(DNI);
            return cliente;
        }

        [WebMethod]
        public static bool ExisteUsuario(int idPersona)
        {
            UsuariosNegocio negocio = new UsuariosNegocio();
            Personas persona = new Personas();
            persona.idpersona = idPersona;
            return negocio.ExisteUsuario(persona);
        }
    }
}