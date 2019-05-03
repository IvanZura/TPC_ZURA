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
    public partial class EmpleadosListado : Form
    {
        private List<Empleados> ListadoEmpleados;
        public EmpleadosListado()
        {
            InitializeComponent();
        }
        private void EmpleadosListado_Load(object sender, EventArgs e)
        {
            this.cargarGrilla();
        }

        private void cargarGrilla()
        {
            EmpleadosNegocio negocioEmpleados = new EmpleadosNegocio();
            try
            {
                this.ListadoEmpleados = negocioEmpleados.ListarEmpleados();
                dgvEmpleados.DataSource = this.ListadoEmpleados;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
