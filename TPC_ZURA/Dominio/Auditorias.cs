using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Auditorias
    {
        public int id { get; set; }
        public Reclamo Reclamo { get; set; }
        public Usuarios UsuarioEjecuto { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaHora { get; set; }
        public string Accion { get; set; }
    }
}
