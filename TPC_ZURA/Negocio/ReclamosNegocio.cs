using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;
using System.Data.SqlClient;

namespace Negocio
{
    public class ReclamosNegocio
    {
        public bool CerrarReclamo(Reclamo reclamo, int op, Usuarios usuario)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                if (op == 6 || op == 3)
                {
                    comando.CommandText = "update Reclamos set IDEstado = "+op+", Solucion = '"+reclamo.solucion+"', " +
                    "IDCerro = "+usuario.id+", FechaHoraCerrado = Getdate() where id = "+ reclamo.id;
                }
                if (op == 2)
                {
                    comando.CommandText = "update Reclamos set IDEstado = " + op + ", IDPrioridad ="+reclamo.prioridad.tipo+", " +
                    "IDIncidencia="+reclamo.incidencia.tipo+" where id = " + reclamo.id;
                }
                if (op == 4)
                {
                    comando.CommandText = "update Reclamos set IDEstado = " + op + ", IDReabrio =" + usuario.id + ", " +
                    "FechaHoraReAbierto=GETDATE() where id = " + reclamo.id;
                }
                
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                if (lector.RecordsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public bool InsertarReclamo(Reclamo reclamo)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "insert into Reclamos (IDCliente, IDIncidencia, IDPrioridad, Titulo, Problematica, IDCreador, IDEstado, IDAsignado) " +
                    "values(" + reclamo.cliente.idcliente + ", " + reclamo.incidencia.tipo + ", " + reclamo.prioridad.tipo + ", " +
                    "'" + reclamo.Titulo + "', '" + reclamo.problematica + "', " + reclamo.creador.id + ", 1, " + reclamo.Asignado.id + ")";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                if (lector.RecordsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public List<Prioridad> ListarPrioridades()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Prioridad> listado = new List<Prioridad>();
            Prioridad nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select ID, Nombre from Prioridades";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    nuevo = new Prioridad();
                    nuevo.tipo = (int)lector["ID"];
                    nuevo.nombre = lector["Nombre"].ToString();

                    listado.Add(nuevo);
                }
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public Usuarios AAsignar()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            Usuarios nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select us.id IDAAsignar, count(rc.ID) from Empleados as emp inner join Usuarios as us on us.IDPersona = emp.IDPersona inner join Reclamos rc on rc.IDAsignado = us.ID where emp.IDPuesto = 2 and us.Activo = 1 group by us.ID order by count(rc.ID) asc";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();

                nuevo = new Usuarios();
                nuevo.id = (int)lector["IDAAsignar"];
                return nuevo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public List<TipoIncidencia> ListarIncidencias()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<TipoIncidencia> listado = new List<TipoIncidencia>();
            TipoIncidencia nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select ID, Nombre from TiposIncidencias where activo = 1";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    nuevo = new TipoIncidencia();
                    nuevo.tipo = (int)lector["ID"];
                    nuevo.nombre = lector["Nombre"].ToString();

                    listado.Add(nuevo);
                }
                return listado;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public List<Reclamo> ListarReclamos()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Reclamo> listado = new List<Reclamo>();
            Reclamo nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select rc.*, es.Nombre as Estado, tin.Nombre as Incidencia, pr.Nombre as Prioridad, (select p.Nombre + ' ' + p.Apellido + ' - ' + p.DNI as NombreCompleto from clientes as cl inner join Personas as p on p.ID = cl.IDPersona where cl.ID = rc.IDCliente) as Cliente, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDCreador) as Creador, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDAsignado) as Asignado, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDReabrio) as Reabrio, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDCerro) as Cerro from reclamos as rc inner join Estados as es on es.ID = rc.IDEstado inner join TiposIncidencias as tin on tin.ID = rc.IDIncidencia inner join Prioridades as pr on pr.ID = rc.IDPrioridad where rc.IDEstado not in (3, 6)";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    nuevo = new Reclamo();
                    nuevo.id = (int)lector["ID"];
                    nuevo.cliente = new Clientes();
                    nuevo.cliente.idcliente = (int)lector["idCliente"];
                    nuevo.incidencia = new TipoIncidencia();
                    nuevo.incidencia.tipo = (int)lector["idIncidencia"];
                    nuevo.prioridad = new Prioridad();
                    nuevo.prioridad.tipo = (int)lector["idPrioridad"];
                    nuevo.problematica = lector["problematica"].ToString();
                    nuevo.Titulo = lector["Titulo"].ToString();
                    nuevo.creador = new Usuarios();
                    nuevo.creador.id = (int)lector["idCreador"];
                    nuevo.estado = new Estados();
                    nuevo.estado.tipo = (int)lector["idEstado"];
                    nuevo.estado.nombre = lector["Estado"].ToString();
                    nuevo.incidencia.nombre = lector["Incidencia"].ToString();
                    nuevo.prioridad.nombre = lector["Prioridad"].ToString();
                    nuevo.cliente.NombreCompleto = lector["Cliente"].ToString();
                    nuevo.creador.DNI = lector["Creador"].ToString();
                    nuevo.solucion = lector["solucion"].ToString();
                    nuevo.Reabrio = new Usuarios();
                    nuevo.Reabrio.id = (int)lector["IDReabrio"];
                    nuevo.Asignado = new Usuarios();
                    nuevo.Asignado.id = (int)lector["IDAsignado"];
                    nuevo.Reabrio.DNI = lector["Reabrio"].ToString();
                    nuevo.Asignado.DNI = lector["Asignado"].ToString();
                    nuevo.Cerro = new Usuarios();
                    nuevo.Cerro.id = (int)lector["IDCerro"];
                    nuevo.Cerro.DNI = lector["Cerro"].ToString();
                    nuevo.AltaReclamo = (DateTime)lector["FechaHora"];
                    nuevo.ReAbiertoReclamo = (DateTime)lector["FechaHoraReAbierto"];
                    nuevo.CerradoReclamo = (DateTime)lector["FechaHoraCerrado"];

                    listado.Add(nuevo);
                }
                return listado;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public List<Reclamo> ListarReclamosPorCreador(int IDCreador)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Reclamo> listado = new List<Reclamo>();
            Reclamo nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select rc.*, es.Nombre as Estado, tin.Nombre as Incidencia, pr.Nombre as Prioridad, (select p.Nombre + ' ' + p.Apellido + ' - ' + p.DNI as NombreCompleto from clientes as cl inner join Personas as p on p.ID = cl.IDPersona where cl.ID = rc.IDCliente) as Cliente, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDCreador) as Creador, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDAsignado) as Asignado, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDReabrio) as Reabrio, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDCerro) as Cerro from reclamos as rc inner join Estados as es on es.ID = rc.IDEstado inner join TiposIncidencias as tin on tin.ID = rc.IDIncidencia inner join Prioridades as pr on pr.ID = rc.IDPrioridad where rc.IDEstado not in (3, 6) and rc.IDCliente = " + IDCreador;
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    nuevo = new Reclamo();
                    nuevo.id = (int)lector["ID"];
                    nuevo.cliente = new Clientes();
                    nuevo.cliente.idcliente = (int)lector["idCliente"];
                    nuevo.incidencia = new TipoIncidencia();
                    nuevo.incidencia.tipo = (int)lector["idIncidencia"];
                    nuevo.prioridad = new Prioridad();
                    nuevo.prioridad.tipo = (int)lector["idPrioridad"];
                    nuevo.problematica = lector["problematica"].ToString();
                    nuevo.Titulo = lector["Titulo"].ToString();
                    nuevo.creador = new Usuarios();
                    nuevo.creador.id = (int)lector["idCreador"];
                    nuevo.estado = new Estados();
                    nuevo.estado.tipo = (int)lector["idEstado"];
                    nuevo.estado.nombre = lector["Estado"].ToString();
                    nuevo.incidencia.nombre = lector["Incidencia"].ToString();
                    nuevo.prioridad.nombre = lector["Prioridad"].ToString();
                    nuevo.cliente.NombreCompleto = lector["Cliente"].ToString();
                    nuevo.creador.DNI = lector["Creador"].ToString();
                    nuevo.solucion = lector["solucion"].ToString();
                    nuevo.Reabrio = new Usuarios();
                    nuevo.Reabrio.id = (int)lector["IDReabrio"];
                    nuevo.Asignado = new Usuarios();
                    nuevo.Asignado.id = (int)lector["IDAsignado"];
                    nuevo.Reabrio.DNI = lector["Reabrio"].ToString();
                    nuevo.Asignado.DNI = lector["Asignado"].ToString();
                    nuevo.Cerro = new Usuarios();
                    nuevo.Cerro.id = (int)lector["IDCerro"];
                    nuevo.Cerro.DNI = lector["Cerro"].ToString();
                    nuevo.AltaReclamo = (DateTime)lector["FechaHora"];
                    nuevo.ReAbiertoReclamo = (DateTime)lector["FechaHoraReAbierto"];
                    nuevo.CerradoReclamo = (DateTime)lector["FechaHoraCerrado"];

                    listado.Add(nuevo);
                }
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public List<Reclamo> ListarReclamosCerrados()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Reclamo> listado = new List<Reclamo>();
            Reclamo nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select rc.*, es.Nombre as Estado, tin.Nombre as Incidencia, pr.Nombre as Prioridad, (select p.Nombre + ' ' + p.Apellido + ' - ' + p.DNI as NombreCompleto from clientes as cl inner join Personas as p on p.ID = cl.IDPersona where cl.ID = rc.IDCliente) as Cliente, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDCreador) as Creador, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDAsignado) as Asignado, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDReabrio) as Reabrio, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDCerro) as Cerro from reclamos as rc inner join Estados as es on es.ID = rc.IDEstado inner join TiposIncidencias as tin on tin.ID = rc.IDIncidencia inner join Prioridades as pr on pr.ID = rc.IDPrioridad where rc.IDEstado in (3, 6)";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    nuevo = new Reclamo();
                    nuevo.id = (int)lector["ID"];
                    nuevo.cliente = new Clientes();
                    nuevo.cliente.idcliente = (int)lector["idCliente"];
                    nuevo.incidencia = new TipoIncidencia();
                    nuevo.incidencia.tipo = (int)lector["idIncidencia"];
                    nuevo.prioridad = new Prioridad();
                    nuevo.prioridad.tipo = (int)lector["idPrioridad"];
                    nuevo.problematica = lector["problematica"].ToString();
                    nuevo.Titulo = lector["Titulo"].ToString();
                    nuevo.creador = new Usuarios();
                    nuevo.creador.id = (int)lector["idCreador"];
                    nuevo.estado = new Estados();
                    nuevo.estado.tipo = (int)lector["idEstado"];
                    nuevo.estado.nombre = lector["Estado"].ToString();
                    nuevo.incidencia.nombre = lector["Incidencia"].ToString();
                    nuevo.prioridad.nombre = lector["Prioridad"].ToString();
                    nuevo.cliente.NombreCompleto = lector["Cliente"].ToString();
                    nuevo.creador.DNI = lector["Creador"].ToString();
                    nuevo.solucion = lector["solucion"].ToString();
                    nuevo.Reabrio = new Usuarios();
                    nuevo.Reabrio.id = (int)lector["IDReabrio"];
                    nuevo.Asignado = new Usuarios();
                    nuevo.Asignado.id = (int)lector["IDAsignado"];
                    nuevo.Reabrio.DNI = lector["Reabrio"].ToString();
                    nuevo.Asignado.DNI = lector["Asignado"].ToString();
                    nuevo.Cerro = new Usuarios();
                    nuevo.Cerro.id = (int)lector["IDCerro"];
                    nuevo.Cerro.DNI = lector["Cerro"].ToString();
                    nuevo.AltaReclamo = (DateTime)lector["FechaHora"];
                    nuevo.ReAbiertoReclamo = (DateTime)lector["FechaHoraReAbierto"];
                    nuevo.CerradoReclamo = (DateTime)lector["FechaHoraCerrado"];

                    listado.Add(nuevo);
                }
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public List<Reclamo> ListarReclamosCerradosPorCreador(int IDCreador)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Reclamo> listado = new List<Reclamo>();
            Reclamo nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select rc.*, es.Nombre as Estado, tin.Nombre as Incidencia, pr.Nombre as Prioridad, (select p.Nombre + ' ' + p.Apellido + ' - ' + p.DNI as NombreCompleto from clientes as cl inner join Personas as p on p.ID = cl.IDPersona where cl.ID = rc.IDCliente) as Cliente, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDCreador) as Creador, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDAsignado) as Asignado, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDReabrio) as Reabrio, (select p.DNI from Usuarios as us inner join Personas as p on p.ID = us.IDPersona where us.ID = rc.IDCerro) as Cerro from reclamos as rc inner join Estados as es on es.ID = rc.IDEstado inner join TiposIncidencias as tin on tin.ID = rc.IDIncidencia inner join Prioridades as pr on pr.ID = rc.IDPrioridad where rc.IDEstado in (3, 6) and rc.IDCliente = " + IDCreador;
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    nuevo = new Reclamo();
                    nuevo.id = (int)lector["ID"];
                    nuevo.cliente = new Clientes();
                    nuevo.cliente.idcliente = (int)lector["idCliente"];
                    nuevo.incidencia = new TipoIncidencia();
                    nuevo.incidencia.tipo = (int)lector["idIncidencia"];
                    nuevo.prioridad = new Prioridad();
                    nuevo.prioridad.tipo = (int)lector["idPrioridad"];
                    nuevo.problematica = lector["problematica"].ToString();
                    nuevo.Titulo = lector["Titulo"].ToString();
                    nuevo.creador = new Usuarios();
                    nuevo.creador.id = (int)lector["idCreador"];
                    nuevo.estado = new Estados();
                    nuevo.estado.tipo = (int)lector["idEstado"];
                    nuevo.estado.nombre = lector["Estado"].ToString();
                    nuevo.incidencia.nombre = lector["Incidencia"].ToString();
                    nuevo.prioridad.nombre = lector["Prioridad"].ToString();
                    nuevo.cliente.NombreCompleto = lector["Cliente"].ToString();
                    nuevo.creador.DNI = lector["Creador"].ToString();
                    nuevo.solucion = lector["solucion"].ToString();
                    nuevo.Reabrio = new Usuarios();
                    nuevo.Reabrio.id = (int)lector["IDReabrio"];
                    nuevo.Asignado = new Usuarios();
                    nuevo.Asignado.id = (int)lector["IDAsignado"];
                    nuevo.Reabrio.DNI = lector["Reabrio"].ToString();
                    nuevo.Asignado.DNI = lector["Asignado"].ToString();
                    nuevo.Cerro = new Usuarios();
                    nuevo.Cerro.id = (int)lector["IDCerro"];
                    nuevo.Cerro.DNI = lector["Cerro"].ToString();
                    nuevo.AltaReclamo = (DateTime)lector["FechaHora"];
                    nuevo.ReAbiertoReclamo = (DateTime)lector["FechaHoraReAbierto"];
                    nuevo.CerradoReclamo = (DateTime)lector["FechaHoraCerrado"];

                    listado.Add(nuevo);
                }
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
