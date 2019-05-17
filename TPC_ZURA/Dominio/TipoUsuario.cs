using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TipoUsuario
    {
        public int tipo { get; set; }
        public string nombre { get; set; }

        public TipoUsuario (int Tipo, string Nombre)
        {
            this.tipo = Tipo;
            this.nombre = Nombre;
        }

        public override string ToString()
        {
            return this.nombre;
        }
    }
}
