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
    public partial class UsuariosListado : Form
    {
        private List<Usuarios> ListadoUsuarios;
        public UsuariosListado()
        {
            InitializeComponent();
        }

        private void UsuariosListado_Load(object sender, EventArgs e)
        {
            this.cargarGrilla();
        }
        private void cargarGrilla()
        {
            UsuariosNegocio negocioUsuarios = new UsuariosNegocio();
            try
            {
                this.ListadoUsuarios = negocioUsuarios.ListarUsuarios();
                dgvUsuarios.DataSource = this.ListadoUsuarios;
                dgvUsuarios.Columns[0].Visible = false;
                //dgvUsuarios.Columns[4].Visible = false;
                /*dgvUsuarios.Columns[2].Visible = false;
                dgvUsuarios.Columns[4].Visible = false;*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                dgvUsuarios.DataSource = ListadoUsuarios;
            }
            else
            {
                if (txtBuscar.Text.Length >= 3)
                {
                    List<Usuarios> lista;
                    lista = ListadoUsuarios.FindAll(X => X.usuario.Contains(txtBuscar.Text));
                    dgvUsuarios.DataSource = lista;
                }
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            //UsuariosAlta alta = new UsuariosAlta();
            //alta.ShowDialog();
            //this.cargarGrilla();
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            //UsuariosAlta alta = new UsuariosAlta((Usuarios)dgvUsuarios.CurrentRow.DataBoundItem);
            //alta.ShowDialog();
            //this.cargarGrilla();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            //DialogResult res = MessageBox.Show("¿Seguro?", "Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (res == DialogResult.Yes)
            //{
            //    UsuariosNegocio negocio = new UsuariosNegocio();
            //    if (negocio.DeleteUsuario((Usuarios)dgvUsuarios.CurrentRow.DataBoundItem))
            //    {
            //        MessageBox.Show("Baja correcta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        this.cargarGrilla();
            //    } else
            //    {
            //        MessageBox.Show("Ocurrio un error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }
    }
}
