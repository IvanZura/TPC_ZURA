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
    public partial class BuscarCliente : Form
    {
        List<Clientes> listadoClientes;
        ClientesNegocio negocio = new ClientesNegocio();
        Clientes cliente;

        public Clientes ClienteSeleccionado
        {
            get
            {
                return this.cliente;
            }
        }
        public BuscarCliente()
        {
            InitializeComponent();
            this.cargarGrilla();
        }

        private void cargarGrilla()
        {
            try
            {
                this.listadoClientes = negocio.ListarClientes();
                dgvClientes.DataSource = this.listadoClientes;
                //dgvClientes.Columns[0].Visible = false;
                //dgvClientes.Columns[2].Visible = false;
                //dgvClientes.Columns[2].Visible = false;
                //dgvClientes.Columns[3].Visible = false;
                //dgvClientes.Columns[5].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cliente = (Clientes)dgvClientes.CurrentRow.DataBoundItem;
            this.Close();
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            if (txtDNI.Text == "")
            {
                dgvClientes.DataSource = this.listadoClientes;
            }
            else
            {
                if (txtDNI.Text.Length >= 3)
                {
                    List<Clientes> lista;
                    lista = this.listadoClientes.FindAll(X => X.DNI.Contains(txtDNI.Text));
                    dgvClientes.DataSource = lista;
                }
            }
        }
    }
}
