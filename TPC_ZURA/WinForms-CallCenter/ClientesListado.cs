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
        ClientesNegocio negocioClientes;
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
            negocioClientes = new ClientesNegocio();
            try
            {
                this.ListadoClientes = negocioClientes.ListarClientes();
                dgvClientes.DataSource = this.ListadoClientes;
                //dgvClientes.Columns[0].Visible = false;
                dgvClientes.Columns[2].Visible = false;
                //dgvClientes.Columns[2].Visible = false;
                //dgvClientes.Columns[3].Visible = false;
                //dgvClientes.Columns[5].Visible = false;
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                dgvClientes.DataSource = ListadoClientes;
            }
            else
            {
                if (txtBuscar.Text.Length >= 3)
                {
                    List<Clientes> lista;
                    lista = ListadoClientes.FindAll(X => X.nombre.Contains(txtBuscar.Text) || X.apellido.Contains(txtBuscar.Text));
                    dgvClientes.DataSource = lista;
                }
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            ClientesAlta alta = new ClientesAlta();
            alta.ShowDialog();
            this.cargarGrilla();
            //UsuariosAlta alta = new UsuariosAlta();
            //alta.ShowDialog();
            //this.cargarGrilla();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ClientesAlta mod = new ClientesAlta((Clientes)dgvClientes.CurrentRow.DataBoundItem);
            mod.ShowDialog();
            this.cargarGrilla();
            //UsuariosAlta alta = new UsuariosAlta((Usuarios)dgvClientes.CurrentRow.DataBoundItem);
            //alta.ShowDialog();
            //this.cargarGrilla();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Seguro?", "Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                if (negocioClientes.BajaCliente((Clientes)dgvClientes.CurrentRow.DataBoundItem))
                {
                    MessageBox.Show("Baja correcta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.cargarGrilla();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
