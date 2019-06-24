using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms_CallCenter
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuarios"] != null)
            {
                Session["Usuarios"] = null;
                Response.Redirect("/Login");
            } else
            {
                Response.Redirect("/Login");
            }
        }
    }
}