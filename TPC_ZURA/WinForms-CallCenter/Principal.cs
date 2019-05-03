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
        EmpleadosListado ListadoEMP = new EmpleadosListado();
        ClientesListado ListadoCL = new ClientesListado();
        UsuariosListado ListadoUS = new UsuariosListado();
        ReclamosListado ListadoRC = new ReclamosListado();
        ReclamosCrear CrearRC = new ReclamosCrear();
        public Principal()
        {
            InitializeComponent();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ja! Entraste al dope", "Acerca de");
        }

        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == ListadoUS.Name)
                {
                    abierto = true;
                    f.Show();
                }
            }
            if (!abierto)
            {
                ListadoUS.MdiParent = this;
                ListadoUS.Show();
            }
        }

        private void listadoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == ListadoEMP.Name)
                {
                    abierto = true;
                    f.Show();
                }
            }
            if (!abierto)
            {
                ListadoEMP.MdiParent = this;
                ListadoEMP.Show();
            }
        }

        private void listadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == ListadoCL.Name)
                {
                    abierto = true;
                    f.Show();
                }
            }
            if (!abierto)
            {
                ListadoCL.MdiParent = this;
                ListadoCL.Show();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listadoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == ListadoRC.Name)
                {
                    abierto = true;
                    f.Show();
                }
            }
            if (!abierto)
            {
                ListadoRC.MdiParent = this;
                ListadoRC.Show();
            }
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == CrearRC.Name)
                {
                    abierto = true;
                    f.Show();
                }
            }
            if (!abierto)
            {
                CrearRC.MdiParent = this;
                CrearRC.Show();
            }
        }
    }
}
