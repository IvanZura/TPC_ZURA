using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace WinForms_CallCenter
{
    public partial class BuscaEmpleado : Form
    {
        int persona;
        public BuscaEmpleado()
        {
            InitializeComponent();
            this.CargarCBOPersonas(0);
        }

        public int PersonaSeleccionada
        {
            get
            {
                return this.persona;
            }
        }

        private void CargarCBOPersonas(int op)
        {
            PersonasNegocio listadoNegocio = new PersonasNegocio();
            cboEmpleados.DisplayMember = "NombreCompleto";
            cboEmpleados.ValueMember = "idpersona";
            cboEmpleados.DataSource = listadoNegocio.ListarPersonas(op);
        }

        private void btnElegir_Click(object sender, EventArgs e)
        {
            this.persona = (int)cboEmpleados.SelectedValue;
            this.Close();
        }
    }
}
