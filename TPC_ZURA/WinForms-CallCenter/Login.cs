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
    public partial class Login : Form
    {
        UsuariosNegocio negocio = new UsuariosNegocio();
        public Login()
        {
            InitializeComponent();
        }

        private bool ValidaCampos()
        {
            if (txtUsuDNI.Text == "") return false;
            if (txtPass.Text == "") return false;

            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.ValidaCampos())
            {
                Usuarios usuario = negocio.LoginUsuario(txtUsuDNI.Text, txtPass.Text);
                if (usuario.id != 0)
                {
                    Principal principal = new Principal(usuario);
                    principal.Show();
                    this.Hide();
                } else
                {
                    MessageBox.Show("Usuario inexistente o contraseña incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("Algun campo vacio", "Vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
