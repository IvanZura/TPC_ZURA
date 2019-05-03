using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Reclamo
    {
        public int id { get; set; }
        public Clientes cliente { get; set; }
        public TipoIncidencia incidencia { get; set; }
        public Prioridad prioridad { get; set; }
        public string problematica { get; set; }
        public Usuarios creador { get; set; }
        public Estados estado { get; set; }

        public Reclamo (int ID, Clientes cliente, TipoIncidencia incidencia, Prioridad prioridad,
            string problematica, Usuarios creador, Estados estado)
        {
            this.id = ID;
            this.cliente = cliente;
            this.incidencia = incidencia;
            this.prioridad = prioridad;
            this.problematica = problematica;
            this.creador = creador;
            this.estado = estado;
        }
    }
}
