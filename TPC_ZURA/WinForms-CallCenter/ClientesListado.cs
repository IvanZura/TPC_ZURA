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
    public partial class ClientesListado : Form
    {
        private List<Clientes> ListadoClientes;
        public ClientesListado()
        {
            InitializeComponent();
        }
        private void ClientesListado_Load(object sender, EventArgs e)
        {
            this.cargarGrilla();
        }

        private void cargarGrilla()
        {
            ClientesNegocio negocioClientes = new ClientesNegocio();
            try
            {
                this.ListadoClientes = negocioClientes.ListarClientes();
                dgvClientes.DataSource = this.ListadoClientes;
                dgvClientes.Columns[0].Visible = false;
                dgvClientes.Columns[1].Visible = false;
                dgvClientes.Columns[2].Visible = false;
                dgvClientes.Columns[3].Visible = false;
                dgvClientes.Columns[4].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ClientesListado_Load_1(object sender, EventArgs e)
        {
            this.cargarGrilla();
        }
    }
}
