using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empleados : Usuarios
    {
        public int idempleado { get; set; }

        public Empleados(int IDEmpleado, int ID, int IDPersona, string Nombre,
            string Apellido, string Usuario, int Tipo, string NombreTipo,
            DateTime fnacimiento, string email, int telefono, bool activo)
        {
            this.idempleado = IDEmpleado;
            this.id = ID;
            this.idpersona = IDPersona;
            this.nombre = Nombre;
            this.apellido = Apellido;
            this.usuario = Usuario;
            this.fnacimiento = fnacimiento;
            this.email = email;
            this.telefono = telefono;
            this.tipo = new TipoUsuario(Tipo, NombreTipo);
            this.activo = activo;
        }
    }
}
