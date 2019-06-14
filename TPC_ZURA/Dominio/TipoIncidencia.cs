using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TipoIncidencia
    {
        public int tipo { get; set; }
        public string nombre { get; set; }

        public override string ToString()
        {
            return nombre;
        }

        //public TipoIncidencia(int Tipo, string Nombre)
        //{
        //    this.tipo = Tipo;
        //    this.nombre = Nombre;
        //}
    }
}
