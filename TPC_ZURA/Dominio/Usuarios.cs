using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuarios
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public TipoUsuario tipo { get; set; }
        public DateTime altaUsuario { get; set; }
        public Clientes cliente { get; set; }
        public Empleados empleado { get; set; }

        /*public Usuarios (int ID, int IDPersona, string Nombre,
            string Apellido, string Usuario, int Tipo, string NombreTipo,
            string fnacimiento, string email, int telefono)
        {
            this.id = ID;
            this.idpersona = IDPersona;
            this.nombre = Nombre;
            this.apellido = Apellido;
            this.usuario = Usuario;
            this.fnacimiento = fnacimiento;
            this.email = email;
            this.telefono = telefono;
            this.tipo = new TipoUsuario(Tipo, NombreTipo);
        }*/
    }
}
