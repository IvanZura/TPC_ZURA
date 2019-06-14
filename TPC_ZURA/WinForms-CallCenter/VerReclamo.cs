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
    public partial class VerReclamo : Form
    {
        ReclamosNegocio negocio = new ReclamosNegocio();
        Reclamo reclamo;
        Usuarios usuario;
        public VerReclamo(Reclamo reclamo, int op, Usuarios usuario)
        {
            this.reclamo = reclamo;
            this.usuario = usuario;
            InitializeComponent();
            this.CargarIncidencias();
            this.CargarPrioridades();
            txtNumero.Text = reclamo.id.ToString();
            txtEstado.Text = reclamo.estado.nombre;
            cboPrioridad.SelectedValue = reclamo.prioridad.tipo;
            txtTitulo.Text = reclamo.Titulo;
            txtProblematica.Text = reclamo.problematica;
            txtSolucion.Text = reclamo.solucion;
            cboIncidencia.SelectedValue = reclamo.incidencia.tipo;
            txtCliente.Text = reclamo.cliente.NombreCompleto;
            txtCreador.Text = reclamo.creador.DNI;
            dtpCreacion.Value = reclamo.AltaReclamo;
            dtpReAbierto.Value = reclamo.ReAbiertoReclamo; dtpReAbierto.Visible = true; lblReAbierto.Visible = true;
            dtpCerrado.Value = reclamo.CerradoReclamo; dtpCerrado.Visible = true; lblCerrado.Visible = true;
            if(op == 0)
            {
                btnReAbrir.Enabled = false;
            } else
            {
                btnCerrar.Enabled = false;
                btnResolver.Enabled = false;
                btnReAsignar.Enabled = false;
                btnModificar.Enabled = false;
            }
            if (reclamo.Cerro.id != 0)
            {
                txtSolucion.Enabled = false;
            }
            if (reclamo.Asignado.id != 0) txtAsignado.Text = reclamo.Asignado.DNI;
        }

        public void CargarPrioridades()
        {
            List<Prioridad> Prioridad = new List<Prioridad>();
            Prioridad = negocio.ListarPrioridades();
            cboPrioridad.DisplayMember = "nombre";
            cboPrioridad.ValueMember = "tipo";
            cboPrioridad.DataSource = Prioridad;
        }

        public void CargarIncidencias()
        {
            List<TipoIncidencia> Incidencias = new List<TipoIncidencia>();
            Incidencias = negocio.ListarIncidencias();
            cboIncidencia.DisplayMember = "nombre";
            cboIncidencia.ValueMember = "tipo";
            cboIncidencia.DataSource = Incidencias;
        }

        private void btnResolver_Click(object sender, EventArgs e)
        {
            if (txtSolucion.Text == "")
            {
                MessageBox.Show("Indique una solucion", "Solucion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                this.reclamo.solucion = txtSolucion.Text;
                if (negocio.CerrarReclamo(this.reclamo, 6, this.usuario))
                {
                    MessageBox.Show("Se resolvio el ticket", "Resuelto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                } else
                {
                    MessageBox.Show("Hubo un problema", "Resolver", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (txtSolucion.Text == "")
            {
                MessageBox.Show("Indique una explicacion del porque se cierra", "Cerrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.reclamo.solucion = txtSolucion.Text;
                if (negocio.CerrarReclamo(this.reclamo, 3, this.usuario))
                {
                    MessageBox.Show("Se cerro el ticket", "Cerrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                } else
                {
                    MessageBox.Show("Hubo un problema", "Cerrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            this.reclamo.prioridad.tipo = (int)cboPrioridad.SelectedValue;
            this.reclamo.incidencia.tipo = (int)cboIncidencia.SelectedValue;
            if (negocio.CerrarReclamo(this.reclamo, 2, this.usuario))
            {
                MessageBox.Show("Se modifico el ticket", "Analisis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            } else
            {
                MessageBox.Show("Hubo un problema", "Analisis", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReAbrir_Click(object sender, EventArgs e)
        {
            if (negocio.CerrarReclamo(this.reclamo, 4, this.usuario))
            {
                MessageBox.Show("Se re-abrio el ticket", "Re abrir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Hubo un problema", "Re Abrir", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
