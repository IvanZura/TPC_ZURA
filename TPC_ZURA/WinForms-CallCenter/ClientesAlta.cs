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
using System.Text.RegularExpressions;

namespace WinForms_CallCenter
{
    public partial class ClientesAlta : Form
    {
        Personas local;
        ClientesNegocio negocio = new ClientesNegocio();
        bool mod = false;
        Clientes clienteMod;
        public ClientesAlta()
        {
            InitializeComponent();
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtEmail.Enabled = false;
            txtTelefono.Enabled = false;
            dtpNacimiento.Enabled = false;
            btnAceptar.Enabled = false;
        }

        public ClientesAlta(Clientes cliente)
        {
            InitializeComponent();
            txtDNI.Enabled = false;
            btnBuscar.Enabled = false;
            lblResultadoDNI.Text = "Solo algunos campos puede modificar";
            txtDNI.Text = cliente.DNI;
            txtNombre.Text = cliente.nombre;
            txtApellido.Text = cliente.apellido;
            txtEmail.Text = cliente.email;
            txtTelefono.Text = cliente.telefono.ToString();
            dtpNacimiento.Value = cliente.fnacimiento;
            this.Text = "Modificar Cliente";
            btnAceptar.Text = "Modificar";
            txtEmail.Enabled = true;
            txtTelefono.Enabled = true;
            btnAceptar.Enabled = true;
            this.mod = true;
            this.clienteMod = new Clientes();
            this.clienteMod = cliente;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            PersonasNegocio negocioPer = new PersonasNegocio();
            this.local = negocioPer.ExistePersona(txtDNI.Text);

            if (txtDNI.Text == "")
            {
                MessageBox.Show("Dejo vacio el campo", "vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            negocio = new ClientesNegocio();
            bool valida = negocio.ExisteCliente(txtDNI.Text);
            if (valida)
            {
                MessageBox.Show("El cliente ya esta registrado", "Ya dado de alta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.local == null)
            {
                lblResultadoDNI.Text = "No existe persona, registrela.";
                txtNombre.Enabled = true;
                txtApellido.Enabled = true;
                txtEmail.Enabled = true;
                txtTelefono.Enabled = true;
                dtpNacimiento.Enabled = true;
                btnAceptar.Enabled = true;
                txtDNI.Enabled = false;
                btnBuscar.Enabled = false;
            }
            else
            {
                lblResultadoDNI.Text = "Tenemos los datos, falta alta como cliente.";
                btnAceptar.Enabled = true;
                txtNombre.Text = this.local.nombre;
                txtApellido.Text = this.local.apellido;
                txtEmail.Text = this.local.email;
                txtTelefono.Text = this.local.telefono.ToString();
                dtpNacimiento.Value = this.local.fnacimiento;
                txtDNI.Enabled = false;
                btnBuscar.Enabled = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool VerificarCampos()
        {
            bool valida = false;
            if (txtNombre.Text == "") valida = true;
            if (txtApellido.Text == "") valida = true;
            if (txtEmail.Text == "") valida = true;
            if (txtDNI.Text == "") valida = true;
            if (txtTelefono.Text == "") valida = true;
            return valida;
        }

        private bool VerificaEdad()
        {
            bool valida = false;
            DateTime local = dtpNacimiento.Value;
            DateTime ahora = DateTime.Now;
            int year = ahora.Year - local.Year;
            int mes = ahora.Month - local.Month;
            int dia = ahora.Day - local.Day;
            if (year >= 17)
            {
                if (year > 17)
                {
                    valida = true;
                }
                else
                {
                    if (mes >= 0)
                    {
                        if (dia >= 0)
                        {
                            valida = true;
                        }
                    }
                }
            }
            return valida;
        }

        private bool verificaMail()
        {
            string local = txtEmail.Text;
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(local, expresion))
            {
                if (Regex.Replace(local, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.VerificarCampos())
            {
                MessageBox.Show("Algun campo vacio", "Llene todo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!this.VerificaEdad())
            {
                MessageBox.Show("El cliente debe tener mas de 17 años", "Menor de edad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!this.verificaMail())
            {
                MessageBox.Show("Formato de E-mail inválido", "E-mail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!this.mod)
            {
                Clientes cliente = new Clientes();
                if (this.local != null)
                {
                    cliente.idpersona = this.local.idpersona;
                }
                else
                {
                    cliente.nombre = txtNombre.Text;
                    cliente.apellido = txtApellido.Text;
                    cliente.email = txtEmail.Text;
                    cliente.telefono = Convert.ToInt32(txtTelefono.Text);
                    cliente.DNI = txtDNI.Text;
                    cliente.fnacimiento = dtpNacimiento.Value;
                }
                if (negocio.InsertarCliente(cliente))
                {
                    MessageBox.Show("Cliente agregado", "Alta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error", "Alta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                clienteMod.email = txtEmail.Text;
                clienteMod.telefono = Convert.ToInt32(txtTelefono.Text);
                if (negocio.ModificaCliente(clienteMod))
                {
                    MessageBox.Show("Cliente modificado correctamente", "Modificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error", "Modificacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
