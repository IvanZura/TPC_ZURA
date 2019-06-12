using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Personas
    {
        public int idpersona { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fnacimiento { get; set; }
        public string email { get; set; }
        public int telefono { get; set; }
        public string DNI { get; set; }
        public DateTime altaPersona { get; set; }
        public string NombreCompleto { get; set; }
    }
}
