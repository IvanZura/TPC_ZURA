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
            return negocio.ListarReclamosPorCreador(cliente.idcliente);
        }

        [WebMethod]
        public static List<TipoIncidencia> TodasIncidencias()
        {
            ReclamosNegocio negocio = new ReclamosNegocio();
            return negocio.ListarIncidencias();
        }

        [WebMethod]
        public static List<Prioridad> TodasPrioridades()
        {
            ReclamosNegocio negocio = new ReclamosNegocio();
            return negocio.ListarPrioridades();
        }
        
        [WebMethod]
        public static Usuarios UsAAsignar()
        {
            ReclamosNegocio negocio = new ReclamosNegocio();
            Usuarios usuario = negocio.AAsignar();
            return usuario;
        }

        [WebMethod]
        public static bool AltaRec(int idcliente, int IDIncidencia, int IDPrioridad, string Titulo, string Problematica, int IDAsignado)
        {
            Usuarios usuario = (Usuarios)HttpContext.Current.Session["Usuarios"];
            if (usuario == null)
            {
                HttpContext.Current.Response.Redirect("/Login");
            }
            ReclamosNegocio negocio = new ReclamosNegocio();
            Reclamo reclamo = new Reclamo();
            reclamo.cliente = new Clientes();
            reclamo.cliente.idcliente = idcliente;
            reclamo.incidencia = new TipoIncidencia();
            reclamo.incidencia.tipo = IDIncidencia;
            reclamo.prioridad = new Prioridad();
            reclamo.prioridad.tipo = IDPrioridad;
            reclamo.Titulo = Titulo;
            reclamo.problematica = Problematica;
            reclamo.Asignado = new Usuarios();
            reclamo.Asignado.id = IDAsignado;
            reclamo.creador = new Usuarios();
            
            reclamo.creador.id = usuario.id;

            return negocio.InsertarReclamo(reclamo);
        }
    }
}