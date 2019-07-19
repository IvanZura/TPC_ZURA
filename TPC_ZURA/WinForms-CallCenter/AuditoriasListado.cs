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
    public partial class AuditoriasListado : Form
    {
        AuditoriasNegocio negAuditorias = new AuditoriasNegocio();
        List<Auditorias> listadoAuditorias;
        public AuditoriasListado()
        {
            InitializeComponent();
            this.cargarGrilla();
        }

        private void cargarGrilla()
        {
            try
            {
                this.listadoAuditorias = negAuditorias.ListarMovimientos();
                dgvAuditorias.DataSource = this.listadoAuditorias;
                dgvAuditorias.Columns[3].Width = 300;
                dgvAuditorias.Columns[1].HeaderText = "Reclamo N°";
                dgvAuditorias.Columns[0].Visible = false;
                //dgvUsuarios.Columns[3].Visible = false;
                //dgvUsuarios.Columns[4].Visible = false;*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                dgvAuditorias.DataSource = this.listadoAuditorias;
            }
            else
            {
                if (txtBuscar.Text.Length >= 0)
                {
                    List<Auditorias> lista;
                    lista = this.listadoAuditorias.FindAll(X => X.Reclamo.id.ToString().Equals(txtBuscar.Text));
                    dgvAuditorias.DataSource = lista;
                }
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (txtBuscar.Text == "")
                {
                    dgvAuditorias.DataSource = this.listadoAuditorias;
                }
                else
                {
                    if (txtBuscar.Text.Length >= 0)
                    {
                        List<Auditorias> lista;
                        lista = this.listadoAuditorias.FindAll(X => X.Reclamo.id.ToString().Equals(txtBuscar.Text));
                        dgvAuditorias.DataSource = lista;
                    }
                }
            }
        }
    }
}
