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
    public partial class ReclamosListado : Form
    {
        ReclamosNegocio negocio = new ReclamosNegocio();
        List<Reclamo> listadoReclamos;
        Usuarios usuario;
        public ReclamosListado(Usuarios usuario)
        {
            this.usuario = usuario;
            InitializeComponent();
            this.cargarGrilla();
        }
        private void cargarGrilla()
        {
            try
            {
                this.listadoReclamos = negocio.ListarReclamos(this.usuario);
                dgvReclamos.DataSource = this.listadoReclamos;
                dgvReclamos.Columns[0].Width = 60;
                dgvReclamos.Columns[0].HeaderText = "Reclamo N°";
                dgvReclamos.Columns[1].Visible = false;
                dgvReclamos.Columns[5].Width = 300;
                dgvReclamos.Columns[6].Visible = false;
                //dgvUsuarios.Columns[1].Visible = false;
                //dgvUsuarios.Columns[3].Visible = false;
                //dgvUsuarios.Columns[4].Visible = false;*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            ReclamosCrear crear = new ReclamosCrear(this.usuario);
            crear.ShowDialog();
            this.cargarGrilla();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (this.dgvReclamos.CurrentRow == null)
            {
                MessageBox.Show("No se selecciono nada", "Seleccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Reclamo rec = (Reclamo)this.dgvReclamos.CurrentRow.DataBoundItem;
                VerReclamo ver = new VerReclamo(rec, 0, this.usuario);
                ver.ShowDialog();
                this.cargarGrilla();
            }
        }
    }
}
