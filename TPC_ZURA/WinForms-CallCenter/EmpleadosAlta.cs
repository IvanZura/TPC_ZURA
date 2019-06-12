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
    public partial class EmpleadosAlta : Form
    {
        Personas local;
        EmpleadosNegocio negocio;
        bool mod = false;
        Empleados empleadoMod;
        public EmpleadosAlta()
        {
            negocio = new EmpleadosNegocio();
            InitializeComponent();
            cboPuesto.DisplayMember = "Nombre";
            cboPuesto.ValueMember = "id";
            cboPuesto.DataSource = negocio.ListarPuestos();
        }

        public EmpleadosAlta(Empleados empleado)
        {
            this.mod = true;
            negocio = new EmpleadosNegocio();
            InitializeComponent();
            cboPuesto.DisplayMember = "Nombre";
            cboPuesto.ValueMember = "id";
            cboPuesto.DataSource = negocio.ListarPuestos();
            txtDNI.Enabled = false;
            btnBuscar.Enabled = false;
            lblResultadoDNI.Text = "Solo algunos campos puede modificar";
            txtDNI.Text = empleado.DNI;
            txtNombre.Text = empleado.nombre;
            txtApellido.Text = empleado.apellido;
            txtEmail.Text = empleado.email;
            txtTelefono.Text = empleado.telefono.ToString();
            dtpNacimiento.Value = empleado.fnacimiento;
            cboPuesto.SelectedValue = empleado.puesto.id;
            this.Text = "Modificar Empleado";
            btnAceptar.Text = "Modificar";
            txtEmail.Enabled = true;
            txtTelefono.Enabled = true;
            cboPuesto.Enabled = true;
            btnAceptar.Enabled = true;
            empleadoMod = new Empleados();
            empleadoMod = empleado;
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
                } else
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
                MessageBox.Show("El empleado debe tener mas de 17 años", "Menor de edad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!this.verificaMail())
            {
                MessageBox.Show("Formato de E-mail inválido", "E-mail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!this.mod)
            {
                Empleados empleado = new Empleados();
                if (this.local != null)
                {
                    empleado.idpersona = this.local.idpersona;
                    empleado.puesto = new Puestos();
                    empleado.puesto.id = (int)cboPuesto.SelectedValue;
                    empleado.puesto.Nombre = cboPuesto.SelectedText;
                }
                else
                {
                    empleado.nombre = txtNombre.Text;
                    empleado.apellido = txtApellido.Text;
                    empleado.email = txtEmail.Text;
                    empleado.telefono = Convert.ToInt32(txtTelefono.Text);
                    empleado.DNI = txtDNI.Text;
                    empleado.fnacimiento = dtpNacimiento.Value;
                    empleado.puesto = new Puestos();
                    empleado.puesto.id = (int)cboPuesto.SelectedValue;
                    empleado.puesto.Nombre = cboPuesto.SelectedText;
                }
                if (negocio.InsertarEmpleado(empleado))
                {
                    MessageBox.Show("Empleado agregado", "Alta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error", "Alta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                empleadoMod.email = txtEmail.Text;
                empleadoMod.telefono = Convert.ToInt32(txtTelefono.Text);
                empleadoMod.puesto.id = (int)cboPuesto.SelectedValue;
                empleadoMod.puesto.Nombre = cboPuesto.SelectedText;
                if (negocio.ModificaEmpleado(empleadoMod))
                {
                    MessageBox.Show("Empleado modificado correctamente", "Modificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error", "Modificacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }            
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
            EmpleadosNegocio negocioEmp = new EmpleadosNegocio();
            bool valida = negocioEmp.ExisteEmpleado(txtDNI.Text);
            if (valida)
            {
                MessageBox.Show("El empleado ya esta registrado", "Ya dado de alta", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                cboPuesto.Enabled = true;
                btnAceptar.Enabled = true;
                txtDNI.Enabled = false;
                btnBuscar.Enabled = false;


            } else
            {
                lblResultadoDNI.Text = "Tenemos los datos, falta alta como empleado.";
                cboPuesto.Enabled = true;
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
    }
}
