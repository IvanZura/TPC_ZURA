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

namespace WinForms_CallCenter
{
    public partial class Principal : Form
    {
        EmpleadosListado ListadoEMP;
        ClientesListado ListadoCL;
        UsuariosListado ListadoUS;
        ReclamosListado ListadoRC;
        ReclamosCrear CrearRC;
        public Principal()
        {
            InitializeComponent();
            ListadoEMP = new EmpleadosListado();
            ListadoCL = new ClientesListado();
            ListadoUS = new UsuariosListado();
            ListadoRC = new ReclamosListado();
            CrearRC = new ReclamosCrear();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ja! Entraste al dope", "Acerca de");
        }

        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListadoUS = new UsuariosListado();
            ListadoUS.Show();
        }

        private void listadoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ListadoEMP = new EmpleadosListado();
            ListadoEMP.Show();
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
            ListadoRC = new ReclamosListado();
            ListadoRC.Show();
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrearRC = new ReclamosCrear();
            CrearRC.Show();
        }
    }
}
