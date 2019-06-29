using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace WinForms_CallCenter
{
    public partial class Principal : Form
    {
        EmpleadosListado ListadoEMP;
        ClientesListado ListadoCL;
        UsuariosListado ListadoUS;
        ReclamosListado ListadoRC;
        Usuarios usuario;
        public Principal(Usuarios usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            ListadoEMP = new EmpleadosListado();
            ListadoCL = new ClientesListado();
            ListadoUS = new UsuariosListado();
            ListadoRC = new ReclamosListado(this.usuario);
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sistema 1.0", "Acerca de");
        }

        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmpleadosNegocio negoemp = new EmpleadosNegocio();
            int per = negoemp.PuestoPorEmpleado(this.usuario.Empleado);
            if (per == 2 || per == 3)
            {
                MessageBox.Show("Usted no tiene permisos", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                ListadoUS = new UsuariosListado();
                ListadoUS.Show();
            }
        }

        private void listadoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            EmpleadosNegocio negoemp = new EmpleadosNegocio();
            if (negoemp.PuestoPorEmpleado(this.usuario.Empleado) == 2)
            {
                MessageBox.Show("Usted no tiene permisos", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                ListadoEMP = new EmpleadosListado();
                ListadoEMP.Show();
            }
        }

        private void listadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListadoCL = new ClientesListado();
            ListadoCL.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listadoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ListadoRC = new ReclamosListado(this.usuario);
            ListadoRC.Show();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReclamosListadoCerrado ListadoRCCerrados = new ReclamosListadoCerrado(this.usuario);
            ListadoRCCerrados.Show();
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
