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
    public partial class UsuariosAlta : Form
    {
        public bool isMod;
        Usuarios usuarioMod;
        UsuariosNegocio negocio = new UsuariosNegocio();
        public UsuariosAlta()
        {
            InitializeComponent();
            this.CargarCBOPersonas(0);
            isMod = false;
        }

        public UsuariosAlta(Usuarios mod)
        {
            InitializeComponent();
            this.CargarCBOPersonas(1);
            this.Text = "Modificacion";
            this.btnAgregar.Text = "Modificar";
            isMod = true;
            usuarioMod = mod;
            txtUsuario.Enabled = false;
            cboPersonas.Enabled = false;
            cboPersonas.SelectedValue = mod.idPersona;
            txtUsuario.Text = mod.usuario;
            lblmsg1.Text = "";
            lblmsg2.Text = "Solo se puede modificar contraseña";
        }


        private void CargarCBOPersonas(int op)
        {
            PersonasNegocio listadoNegocio = new PersonasNegocio();
            cboPersonas.DisplayMember = "NombreCompleto";
            cboPersonas.ValueMember = "idpersona";
            cboPersonas.DataSource = listadoNegocio.ListarPersonas(op);
        }

        private bool ValidarCampos()
        {
            errorProvider1.Clear();
            if (txtUsuario.Text == "")
            {
                errorProvider1.SetError(txtUsuario, "Ingrese un usuario");
                return false;
            }
            if (txtPass.Text == "")
            {
                errorProvider1.SetError(txtPass, "Ingrese una contraseña");
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (isMod)
            {
                this.Modificar();
            } else
            {
                this.Alta();
            }
        }

        private void UsuariosAlta_Load(object sender, EventArgs e)
        {
            
        }

        private void Alta()
        {
            if (this.ValidarCampos())
            {
                Usuarios nuevo = new Usuarios();
                nuevo.usuario = txtUsuario.Text;
                nuevo.pass = txtPass.Text;
                Personas elegido = (Personas)cboPersonas.SelectedItem;
                if (!negocio.ExisteUsuario(elegido))
                {
                    if (this.negocio.ExisteNombreUsuario(nuevo.usuario))
                    {
                        MessageBox.Show("Ya existe este usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    } else
                    {
                        if (this.negocio.InsertarUsuario(nuevo, elegido))
                        {
                            MessageBox.Show("Alta correcta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                } else
                {
                    MessageBox.Show("Ya existe un usuario para esta persona", "Vacios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                
            }
        }

        private void Modificar()
        {
            if (this.ValidarCampos())
            {
                Personas elegido = (Personas)cboPersonas.SelectedItem;
                this.usuarioMod.pass = txtPass.Text;
                if (negocio.ExisteUsuario(elegido))
                {
                    if (this.negocio.ModificarUsuario(this.usuarioMod, elegido))
                    {
                        MessageBox.Show("Modificacion correcta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se puede modificar un usuario que no existe o esta dado de baja", "Vacios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                
            }
        }
    }
}
