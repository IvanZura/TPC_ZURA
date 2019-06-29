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
    public partial class ReclamosListadoCerrado : Form
    {
        ReclamosNegocio negocio = new ReclamosNegocio();
        List<Reclamo> listadoReclamos;
        Usuarios usuario;
        public ReclamosListadoCerrado(Usuarios usuarios)
        {
            this.usuario = usuarios;
            InitializeComponent();
            this.cargarGrilla();
        }

        public void cargarGrilla()
        {
            try
            {
                this.listadoReclamos = negocio.ListarReclamosCerrados(this.usuario);
                dgvReclamosCerrados.DataSource = this.listadoReclamos;
                dgvReclamosCerrados.Columns[0].Width = 60;
                dgvReclamosCerrados.Columns[0].HeaderText = "Ticket N°";
                //dgvReclamos.Columns[1].Visible = false;
                dgvReclamosCerrados.Columns[5].Width = 300;
                /////dgvReclamos.Columns[5].Visible = false;
                //dgvUsuarios.Columns[1].Visible = false;
                //dgvReclamosCerrados.Columns[9].Visible = false;
                //dgvReclamosCerrados.Columns[10].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {

        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (this.dgvReclamosCerrados.CurrentRow == null)
            {
                MessageBox.Show("No se selecciono nada", "Seleccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                Reclamo rec = (Reclamo)this.dgvReclamosCerrados.CurrentRow.DataBoundItem;
                VerReclamo ver = new VerReclamo(rec, 1, this.usuario);
                ver.ShowDialog();
                this.cargarGrilla();
            }
            
        }
    }
}
