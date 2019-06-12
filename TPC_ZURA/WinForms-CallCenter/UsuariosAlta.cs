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
        public Usuarios mod;
        public UsuariosAlta()
        {
            InitializeComponent();
            this.CargarCBOTipos();
            isMod = false;
        }

        public UsuariosAlta(Usuarios mod)
        {
            InitializeComponent();
            this.CargarCBOTipos();
            this.Text = "Modificacion";
            this.btnAgregar.Text = "Modificar";
            isMod = true;
            this.mod = mod;
            //txtNombre.Text = mod.nombre;
            //txtApellido.Text = mod.apellido;
            //dtpNacimiento.Value = mod.fnacimiento;
            //txtEmail.Text = mod.email;
            //txtTelefono.Text = mod.telefono.ToString();
            //txtUsuario.Text = mod.usuario;
            //cboTipos.SelectedIndex = cboTipos.FindString(mod.tipo.nombre);
        }


        private void CargarCBOTipos()
        {
            //UsuariosNegocio listadoNegocio = new UsuariosNegocio();
            //cboTipos.DisplayMember = "nombre";
            //cboTipos.ValueMember = "id";
            //cboTipos.DataSource = listadoNegocio.ListarTiposUsuarios();
        }

        private bool ValidarCampos()
        {
            //string faltan = "";
            //if (txtNombre.Text == "") faltan += "Nombre ";
            //if (txtApellido.Text == "") faltan += "Apellido ";
            //if (txtEmail.Text == "") faltan += "Email ";
            //if (txtTelefono.Text == "") faltan += "Telefono ";
            //if (txtUsuario.Text == "") faltan += "Usuario ";
            //if (txtPass.Text == "") faltan += "Pass ";

            //if (faltan != "")
            //{
            //    MessageBox.Show("Falta: " + faltan);
            //    return false;
            //} else
            //{
            //    return true;
            //}
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

        private void cboTipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*TipoUsuario local;
            local = (TipoUsuario)cboTipos.SelectedItem;
            MessageBox.Show(local.tipo.ToString());*/
        }

        private void dtpNacimiento_ValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(dtpNacimiento.Value.Date.ToString("yyyy-MM-dd"));
        }

        private void UsuariosAlta_Load(object sender, EventArgs e)
        {
            
        }

        private void Alta()
        {
            if (this.ValidarCampos())
            {
                //Usuarios nuevo = new Usuarios();
                //nuevo.nombre = txtNombre.Text;
                //nuevo.apellido = txtApellido.Text;
                //nuevo.email = txtEmail.Text;
                //nuevo.telefono = Int32.Parse(txtTelefono.Text);
                //nuevo.usuario = txtUsuario.Text;
                //string pass = txtPass.Text;
                //nuevo.fnacimiento = dtpNacimiento.Value;
                //TipoUsuario local;
                //local = (TipoUsuario)cboTipos.SelectedItem;
                //nuevo.tipo = new TipoUsuario(local.tipo, local.nombre);
                //UsuariosNegocio negocio = new UsuariosNegocio();
                //if (negocio.InsertarUsuario(nuevo, pass))
                //{
                //    MessageBox.Show("Alta correcta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.Close();
                //}
                //else
                //{
                //    MessageBox.Show("Ocurrio un error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
        }

        private void Modificar()
        {
            if (this.ValidarCampos())
            {
                //TipoUsuario local;
                //local = (TipoUsuario)cboTipos.SelectedItem;
                //if (local.tipo == 4 && this.mod.tipo.tipo != 4)
                //{
                //    MessageBox.Show("Un empleado no puede cambiar a Cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                //if (local.tipo != 4 && this.mod.tipo.tipo == 4)
                //{
                //    MessageBox.Show("Un cliente no puede cambiar a Empleado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                //string pass = txtPass.Text;
                //this.mod.nombre = txtNombre.Text;
                //this.mod.apellido = txtApellido.Text;
                //this.mod.email = txtEmail.Text;
                //this.mod.telefono = Int32.Parse(txtTelefono.Text);
                //this.mod.usuario = txtUsuario.Text;
                //this.mod.fnacimiento = dtpNacimiento.Value;
                //this.mod.tipo = new TipoUsuario(local.tipo, local.nombre);
                //UsuariosNegocio negocio = new UsuariosNegocio();
                //if(negocio.UpdateUsuario(this.mod, pass))
                //{
                //    MessageBox.Show("Modificacion correcta", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.Close();
                //} else
                //{
                //    MessageBox.Show("Ocurrio un error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    this.Close();
                //}
            }
        }
    }
}
