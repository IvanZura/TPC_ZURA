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
        //public bool DeleteUsuario(Usuarios mod)
        //{
        //    SqlConnection conexion = new SqlConnection();
        //    SqlCommand comando = new SqlCommand();
        //    SqlDataReader lector;

        //    try
        //    {
        //        conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
        //        comando.CommandType = System.Data.CommandType.Text;
        //        comando.CommandText = "update usuarios set activo = 0 where id = " + mod.id;
        //        comando.Connection = conexion;
        //        conexion.Open();
        //        lector = comando.ExecuteReader();
        //        lector.Read();
        //        if (lector.RecordsAffected > 0)
        //        {
        //            return true;
        //        } else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //}
        //public bool UpdateUsuario(Usuarios mod, string pass)
        //{
        //    SqlConnection conexion = new SqlConnection();
        //    SqlCommand comando = new SqlCommand();
        //    SqlDataReader lector;

        //    try
        //    {
        //        conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
        //        comando.CommandType = System.Data.CommandType.Text;
        //        comando.CommandText = "update personas set nombre = '"+mod.nombre+"', apellido = " +
        //            "'"+mod.apellido+"', fnacimiento = '"+mod.fnacimiento.Date.ToString("yyyy-MM-dd") + "'," +
        //            "email = '"+mod.email+"', telefono = "+ mod.telefono+" where id = " + mod.idpersona + "" +
        //            "; update usuarios set usuario = '"+mod.usuario+"', pass = '"+pass+"', " +
        //            "tipousuario = "+mod.tipo.tipo+" where id = " + mod.id;
        //        comando.Connection = conexion;
        //        conexion.Open();
        //        lector = comando.ExecuteReader();
        //        lector.Read();
        //        if (lector.RecordsAffected > 0)
        //        {
        //            return true;
        //        } else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //}
        //public bool InsertarUsuario(Usuarios nuevo, string pass)
        //{
        //    bool estado = false;
        //    SqlConnection conexion = new SqlConnection();
        //    SqlCommand comando = new SqlCommand();
        //    SqlDataReader lector;

        //    try
        //    {
        //        conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
        //        comando.CommandType = System.Data.CommandType.Text;                
        //        comando.CommandText = "insert into personas (nombre, apellido, fnacimiento, email, telefono)" +
        //            " values ('" + nuevo.nombre + "','" + nuevo.apellido + "','" + nuevo.fnacimiento.Date.ToString("yyyy-MM-dd") + "'," +
        //            "'" + nuevo.email + "'," + nuevo.telefono + ")";
        //        comando.Connection = conexion;
        //        conexion.Open();
        //        lector = comando.ExecuteReader();
        //        lector.Read();
        //        int idPersona;
        //        int idUsuario;
        //        if (lector.RecordsAffected > 0)
        //        {
        //            conexion.Close();
        //            comando.CommandText = "Select id from personas where email = '" + nuevo.email + "'";
        //            conexion.Open();
        //            lector = comando.ExecuteReader();
        //            lector.Read();
        //            idPersona = (int)lector["id"];
        //            conexion.Close();
        //            comando.CommandText = "insert into usuarios (idpersona, usuario, pass, tipousuario, activo) " +
        //                "values (" + idPersona + ", '" + nuevo.usuario + "', '" + pass + "', " + nuevo.tipo.tipo + ", 1)";
        //            conexion.Open();
        //            lector = comando.ExecuteReader();
        //            lector.Read();
        //            if (lector.RecordsAffected > 0)
        //            {
        //                conexion.Close();
        //                comando.CommandText = "select id from usuarios where usuario = '" + nuevo.usuario + "' and tipousuario = " + nuevo.tipo.tipo;
        //                conexion.Open();
        //                lector = comando.ExecuteReader();
        //                lector.Read();
        //                idUsuario = (int)lector["id"];
        //                conexion.Close();
        //                if (nuevo.tipo.tipo == 4)
        //                {
        //                    comando.CommandText = "insert into clientes (idusuario) values (" + idUsuario + ")";
        //                    conexion.Open();
        //                    lector = comando.ExecuteReader();
        //                    lector.Read();
        //                    if (lector.RecordsAffected > 0)
        //                    {
        //                        estado = true;
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else
        //                {
        //                    comando.CommandText = "insert into empleados (idusuario) values (" + idUsuario + ")";
        //                    conexion.Open();
        //                    lector = comando.ExecuteReader();
        //                    lector.Read();
        //                    if (lector.RecordsAffected > 0)
        //                    {
        //                        estado = true;
        //                    }
        //                    else
        //                    {
        //                        return false;
        //                    }
        //                }
        //            } else
        //            {
        //                return false;
        //            }
        //        } else
        //        {
        //            estado = false;
        //        }
        //        return estado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //}
        public List<TipoUsuario> ListarTiposUsuarios()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<TipoUsuario> listado = new List<TipoUsuario>();
            TipoUsuario nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                //MSF-20190420: agregué todos los datos del heroe. Incluso su universo, que lo traigo con join.
                comando.CommandText = "select id, nombre from TiposUsuarios";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while(lector.Read())
                {
                    //nuevo = new TipoUsuario((int)lector["id"], lector["nombre"].ToString());
                    nuevo = new TipoUsuario();
                    nuevo.tipo = (int)lector["id"];
                    nuevo.nombre = lector["nombre"].ToString();
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
                //MSF-20190420: agregué todos los datos del heroe. Incluso su universo, que lo traigo con join.
                comando.CommandText = "select us.ID, us.Usuario, us.FechaAlta as altaUsuario, us.TipoUsuario, us.IDCliente, us.IDEmpleado, tus.Nombre as nombreTipo from usuarios as us inner join TiposUsuarios as tus on us.TipoUsuario = tus.ID where us.Activo = 1";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    nuevo = new Usuarios();
                    nuevo.id = (int)lector["ID"];
                    nuevo.usuario = lector["Usuario"].ToString();
                    nuevo.tipo = new TipoUsuario();
                    nuevo.tipo.tipo = (int)lector["TipoUsuario"];
                    nuevo.tipo.nombre = lector["nombreTipo"].ToString();
                    nuevo.altaUsuario = (DateTime)lector["altaUsuario"];
                    if ((int)lector["IDCliente"] != 0)
                    {
                        nuevo.cliente = new Clientes();
                        nuevo.cliente.idcliente = (int)lector["IDCliente"];
                    }
                    if ((int)lector["IDEmpleado"] != 0)
                    {
                        nuevo.empleado = new Empleados();
                        nuevo.empleado.legajo = (int)lector["IDEmpleado"];
                    }

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
