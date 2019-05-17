﻿using System;
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
        public bool DeleteUsuario(Usuarios mod)
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
        public bool UpdateUsuario(Usuarios mod, string pass)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "update personas set nombre = '"+mod.nombre+"', apellido = " +
                    "'"+mod.apellido+"', fnacimiento = '"+mod.fnacimiento.Date.ToString("yyyy-MM-dd") + "'," +
                    "email = '"+mod.email+"', telefono = "+ mod.telefono+" where id = " + mod.idpersona + "" +
                    "; update usuarios set usuario = '"+mod.usuario+"', pass = '"+pass+"', " +
                    "tipousuario = "+mod.tipo.tipo+" where id = " + mod.id;
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
        public bool InsertarUsuario(Usuarios nuevo, string pass)
        {
            bool estado = false;
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;                
                comando.CommandText = "insert into personas (nombre, apellido, fnacimiento, email, telefono)" +
                    " values ('" + nuevo.nombre + "','" + nuevo.apellido + "','" + nuevo.fnacimiento.Date.ToString("yyyy-MM-dd") + "'," +
                    "'" + nuevo.email + "'," + nuevo.telefono + ")";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                int idPersona;
                int idUsuario;
                if (lector.RecordsAffected > 0)
                {
                    conexion.Close();
                    comando.CommandText = "Select id from personas where email = '" + nuevo.email + "'";
                    conexion.Open();
                    lector = comando.ExecuteReader();
                    lector.Read();
                    idPersona = (int)lector["id"];
                    conexion.Close();
                    comando.CommandText = "insert into usuarios (idpersona, usuario, pass, tipousuario, activo) " +
                        "values (" + idPersona + ", '" + nuevo.usuario + "', '" + pass + "', " + nuevo.tipo.tipo + ", 1)";
                    conexion.Open();
                    lector = comando.ExecuteReader();
                    lector.Read();
                    if (lector.RecordsAffected > 0)
                    {
                        conexion.Close();
                        comando.CommandText = "select id from usuarios where usuario = '" + nuevo.usuario + "' and tipousuario = " + nuevo.tipo.tipo;
                        conexion.Open();
                        lector = comando.ExecuteReader();
                        lector.Read();
                        idUsuario = (int)lector["id"];
                        conexion.Close();
                        if (nuevo.tipo.tipo == 4)
                        {
                            comando.CommandText = "insert into clientes (idusuario) values (" + idUsuario + ")";
                            conexion.Open();
                            lector = comando.ExecuteReader();
                            lector.Read();
                            if (lector.RecordsAffected > 0)
                            {
                                estado = true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            comando.CommandText = "insert into empleados (idusuario) values (" + idUsuario + ")";
                            conexion.Open();
                            lector = comando.ExecuteReader();
                            lector.Read();
                            if (lector.RecordsAffected > 0)
                            {
                                estado = true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    } else
                    {
                        return false;
                    }
                } else
                {
                    estado = false;
                }
                return estado;
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
                    nuevo = new TipoUsuario((int)lector["id"], lector["nombre"].ToString());
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
            Usuarios nuevo = new Usuarios();

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                //MSF-20190420: agregué todos los datos del heroe. Incluso su universo, que lo traigo con join.
                comando.CommandText = "select us.activo, us.id, us.idpersona, us.TipoUsuario as idtipo, us.Usuario, pr.nombre, pr.Apellido, pr.FNacimiento, pr.Email, pr.Telefono, tus.nombre as tipousuario  from usuarios as us inner join personas as pr on pr.id = us.IDPersona inner join TiposUsuarios as tus on tus.id = us.TipoUsuario where us.activo = 1";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    nuevo = new Usuarios();
                    nuevo.idpersona = (int)lector["idpersona"];
                    nuevo.nombre = lector["nombre"].ToString();
                    nuevo.apellido = lector["apellido"].ToString();
                    nuevo.fnacimiento = (DateTime)lector["fnacimiento"];
                    nuevo.email = lector["email"].ToString();
                    nuevo.telefono = (int)lector["telefono"];
                    nuevo.id = (int)lector["id"];
                    nuevo.usuario = lector["usuario"].ToString();
                    nuevo.tipo = new TipoUsuario((int)lector["idtipo"], lector["tipousuario"].ToString());
                    nuevo.activo = (bool)lector["activo"];

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