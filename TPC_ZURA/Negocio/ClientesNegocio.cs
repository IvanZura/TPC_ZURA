using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Dominio;
using System.Data.SqlClient;

namespace Negocio
{
    public class ClientesNegocio
    {
        public List<Clientes> ListarClientes()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Clientes> listado = new List<Clientes>();
            Clientes nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                //MSF-20190420: agregué todos los datos del heroe. Incluso su universo, que lo traigo con join.
                comando.CommandText = "select cl.ID, cl.IDPersona, cl.FechaAlta as altaCliente, cl.Activo, p.Nombre, p.Apellido, p.FNacimiento, p.Email, p.Telefono, p.DNI, p.FechaAlta as altaPersona from clientes as cl inner join Personas as p on cl.IDPersona = p.ID where cl.activo = 1";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    nuevo = new Clientes();
                    nuevo.idcliente = (int)lector["ID"];
                    nuevo.altaCliente = (DateTime)lector["altaCliente"];
                    nuevo.idpersona = (int)lector["IDPersona"];
                    nuevo.nombre = lector["Nombre"].ToString();
                    nuevo.apellido = lector["Apellido"].ToString();
                    nuevo.fnacimiento = (DateTime)lector["FNacimiento"];
                    nuevo.email = lector["Email"].ToString();
                    nuevo.telefono = (int)lector["Telefono"];
                    nuevo.DNI = lector["DNI"].ToString();
                    nuevo.altaPersona = (DateTime)lector["altaPersona"];

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
        public bool ExisteCliente(string DNI)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select * from clientes as cl inner join Personas as p on cl.IDPersona = p.ID where p.DNI = '" + DNI + "' and cl.activo = 1";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                if (lector.HasRows)
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

        public Clientes ExisteClienteWeb(string DNI)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            Clientes nuevo = null;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select p.id idpersona, cl.id idcliente, p.Nombre, p.Apellido, p.FNacimiento, p.Email, p.Telefono, p.DNI, p.FechaAlta from personas p inner join Clientes cl on cl.IDPersona = p.ID where DNI = '" + DNI + "' and cl.activo = 1";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                if (lector.HasRows)
                {
                    nuevo = new Clientes();
                    nuevo.idpersona = (int)lector["idpersona"];
                    nuevo.idcliente = (int)lector["idcliente"];
                    nuevo.nombre = lector["Nombre"].ToString();
                    nuevo.apellido = lector["Apellido"].ToString();
                    nuevo.email = lector["Email"].ToString();
                    nuevo.telefono = (int)lector["Telefono"];
                    nuevo.DNI = lector["DNI"].ToString();
                    nuevo.altaPersona = (DateTime)lector["FechaAlta"];
                    nuevo.fnacimiento = (DateTime)lector["FNacimiento"];
                }
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

        public bool InsertarCliente(Clientes cliente)
        {
            PersonasNegocio negocioP = new PersonasNegocio();
            int idPersona = 0;
            if (cliente.idpersona == 0)
            {
                idPersona = negocioP.AgregarPersona((Personas)cliente);
                if (idPersona != 0)
                {
                    SqlConnection conexion = new SqlConnection();
                    SqlCommand comando = new SqlCommand();
                    SqlDataReader lector;

                    conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "insert into clientes (IDPersona)" +
                        " values (" + idPersona + ")";
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
                else
                {
                    return false;
                }
            }
            else
            {
                idPersona = cliente.idpersona;
                SqlConnection conexion = new SqlConnection();
                SqlCommand comando = new SqlCommand();
                SqlDataReader lector;

                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "insert into clientes (IDPersona)" +
                    " values (" + idPersona + ")";
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
        }
        public bool BajaCliente(Clientes cliente)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "update clientes set activo = 0 where ID =" + cliente.idcliente;
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
        public bool ModificaCliente(Clientes cliente)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "update personas set Email = '" + cliente.email + "', Telefono = " + cliente.telefono + " where ID =" + cliente.idpersona;
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
    }
}
