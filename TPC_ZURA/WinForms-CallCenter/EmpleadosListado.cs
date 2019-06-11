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
                //dgvEmpleados.Columns[0].Visible = false;
                dgvEmpleados.Columns[3].Visible = false;
                //dgvEmpleados.Columns[2].Visible = false;
                //dgvEmpleados.Columns[5].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            EmpleadosAlta alta = new EmpleadosAlta();
            alta.ShowDialog();
            this.cargarGrilla();
            //UsuariosAlta alta = new UsuariosAlta();
            //alta.ShowDialog();
            //this.cargarGrilla();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //UsuariosAlta alta = new UsuariosAlta((Usuarios)dgvEmpleados.CurrentRow.DataBoundItem);
            //alta.ShowDialog();
            //this.cargarGrilla();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                dgvEmpleados.DataSource = ListadoEmpleados;
            }
            else
            {
                if (txtBuscar.Text.Length >= 3)
                {
                    List<Empleados> lista;
                    lista = ListadoEmpleados.FindAll(X => X.nombre.Contains(txtBuscar.Text) || X.apellido.Contains(txtBuscar.Text));
                    dgvEmpleados.DataSource = lista;
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            //DialogResult res = MessageBox.Show("¿Seguro?", "Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (res == DialogResult.Yes)
            //{
            //    UsuariosNegocio negocio = new UsuariosNegocio();
            //    if (negocio.DeleteUsuario((Usuarios)dgvEmpleados.CurrentRow.DataBoundItem))
            //    {
            //        MessageBox.Show("Baja correcta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        this.cargarGrilla();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Ocurrio un error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }
    }
}
