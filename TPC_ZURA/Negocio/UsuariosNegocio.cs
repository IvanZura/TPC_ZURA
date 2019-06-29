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
    public class UsuariosNegocio
    {
        public bool BorrarUsuario(Usuarios mod)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "update usuarios set activo = 0 where id = " + mod.id;
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                if (lector.RecordsAffected > 0)
                {
                    return true;
                } else
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
        public bool ModificarUsuario(Usuarios mod, Personas elegido)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "update usuarios set pass = '' where ID = " + mod.id;
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                if (lector.RecordsAffected > 0)
                {
                    return true;
                } else
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
        public bool InsertarUsuario(Usuarios nuevo, Personas elegido)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;                
                comando.CommandText = "INSERT INTO Usuarios (IDPersona, Usuario, Pass, Activo) values (" + elegido.idpersona + "" +
                    ", '" + nuevo.usuario + "', '" + nuevo.pass + "', 1)";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                if (lector.RecordsAffected > 0)
                {
                    return true;
                } else
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
        
        public List<Usuarios> ListarUsuarios()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Usuarios> listado = new List<Usuarios>();
            Usuarios nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select us.ID, us.IDPersona, us.Usuario, us.FechaAlta, isnull((select ID from empleados where IDPersona = us.IDPersona and activo = 1), 0) as empleado, isnull((select ID from clientes where IDPersona = us.IDPersona and activo = 1), 0) as cliente, (select DNI from personas where ID = us.IDPersona and activo = 1) as DNI from usuarios as us where us.activo = 1";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    nuevo = new Usuarios();
                    nuevo.id = (int)lector["ID"];
                    nuevo.idPersona = (int)lector["IDPersona"];
                    nuevo.usuario = lector["Usuario"].ToString();
                    nuevo.altaUsuario = (DateTime)lector["FechaAlta"];
                    nuevo.Cliente = (int)lector["cliente"];
                    nuevo.Empleado = (int)lector["empleado"];
                    nuevo.DNI = lector["DNI"].ToString();

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
        public bool ExisteUsuario(Personas elegido)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select * from usuarios where activo = 1 and IDPersona = " + elegido.idpersona;
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    return true;
                } else
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
        public bool ExisteNombreUsuario(string usuario)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select * from usuarios where activo = 1 and Usuario = '" + usuario + "'";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

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
        public Usuarios LoginUsuario(string usuDNI, string pass, int op)
        {
            //1 - cliente
            //2 - empleado
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            Usuarios encontrado;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                if (op == 1)
                {
                    comando.CommandText = "select cl.ID idcliente, p.DNI, us.ID, us.IDPersona, us.Usuario, us.FechaAlta from Usuarios us inner join personas p " +
                    "on p.ID = us.IDPersona inner join clientes cl on cl.IDPersona = p.ID where (p.DNI = '" + usuDNI + "' or us.Usuario = '" + usuDNI + "') " +
                    "and pass='" + pass + "' and us.activo = 1";
                } else
                {
                    comando.CommandText = "select emp.ID idempleado, p.DNI, us.ID, us.IDPersona, us.Usuario, us.FechaAlta from Usuarios us inner join personas p " +
                    "on p.ID = us.IDPersona inner join empleados emp on emp.IDPersona = p.ID where (p.DNI = '" + usuDNI + "' or us.Usuario = '" + usuDNI + "') " +
                    "and pass='" + pass + "' and us.activo = 1";
                }
                
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                
                if (lector.HasRows)
                {
                    encontrado = new Usuarios();
                    encontrado.id = (int)lector["ID"];
                    encontrado.idPersona = (int)lector["IDPersona"];
                    encontrado.usuario = lector["usuario"].ToString();
                    encontrado.altaUsuario = (DateTime)lector["FechaAlta"];
                    encontrado.DNI = lector["DNI"].ToString();
                    if (op == 1)
                    {
                        encontrado.Cliente = (int)lector["idcliente"];
                    } else
                    {
                        encontrado.Empleado = (int)lector["idempleado"];
                    }
                    
                    return encontrado;
                }
                else
                {
                    encontrado = new Usuarios();
                    encontrado.id = 0;
                    return encontrado;
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
    }
}
