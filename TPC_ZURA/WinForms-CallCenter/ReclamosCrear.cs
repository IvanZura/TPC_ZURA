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
    public partial class ReclamosCrear : Form
    {
        ReclamosNegocio negocio = new ReclamosNegocio();
        Clientes cliente;
        Usuarios usuario;
        public ReclamosCrear(Usuarios usuario)
        {
            this.usuario = usuario;
            InitializeComponent();
            txtCliente.Enabled = false;
            this.CargarIncidencias();
            this.CargarPrioridades();
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
            cboIncidencias.DisplayMember = "nombre";
            cboIncidencias.ValueMember = "tipo";
            cboIncidencias.DataSource = Incidencias;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarCliente busca = new BuscarCliente();
            busca.ShowDialog();
            this.cliente = busca.ClienteSeleccionado;
            if (this.cliente != null)
            {
                txtCliente.Text = cliente.nombre + " " + cliente.apellido + " - " + cliente.DNI;
            }
        }

        private bool ValidaCampos()
        {
            if (txtCliente.Text == "") return false;
            if (txtTitulo.Text == "") return false;
            if (txtProblematica.Text == "") return false;

            return true;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (this.ValidaCampos())
            {
                Reclamo reclamo = new Reclamo();
                reclamo.cliente = new Clientes();
                reclamo.cliente.idcliente = this.cliente.idcliente;
                reclamo.incidencia = new TipoIncidencia();
                reclamo.incidencia.tipo = (int)cboIncidencias.SelectedValue;
                reclamo.prioridad = new Prioridad();
                reclamo.prioridad.tipo = (int)cboPrioridad.SelectedValue;
                reclamo.Titulo = txtTitulo.Text;
                reclamo.problematica = txtProblematica.Text;
                reclamo.estado = new Estados();
                reclamo.estado.tipo = 1;
                reclamo.creador = this.usuario;
                reclamo.Asignado = this.usuario;
                if (negocio.InsertarReclamo(reclamo))
                {
                    MessageBox.Show("Reclamo creado", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                } else
                {
                    MessageBox.Show("No se inserto reclamo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Falta llenar algun campo", "Vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
