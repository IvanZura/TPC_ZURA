﻿using System;
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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuarios"] == null)
            {
                Response.Redirect("/Login");
            }
        }

        [WebMethod]
        public static Clientes ElUsuario()
        {
            ClientesNegocio negocio = new ClientesNegocio();
            Usuarios usuario = (Usuarios)HttpContext.Current.Session["Usuarios"];
            Clientes cliente = negocio.ExisteClienteWeb(usuario.DNI);
            if (cliente == null)
            {
                HttpContext.Current.Session["Usuarios"] = null;
                HttpContext.Current.Response.Redirect("/Login");
            }
            return cliente;
        }
    }
}