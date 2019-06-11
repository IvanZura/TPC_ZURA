﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;
using System.Data.SqlClient;

namespace Negocio
{
    public class PersonasNegocio
    {
        // Inserta la persona y devuelve su ID. Si no es posible, devuelve 0
        public int AgregarPersona (Personas persona)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            int idPersona = 0;
            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "insert into personas (nombre, apellido, fnacimiento, email, telefono, DNI)" +
                    " values ('" + persona.nombre + "','" + persona.apellido + "','" + persona.fnacimiento.Date.ToString("yyyy-MM-dd") + "'," +
                    "'" + persona.email + "'," + persona.telefono + ",'" + persona.DNI +"')";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                if (lector.RecordsAffected > 0)
                {
                    conexion.Close();
                    comando.CommandText = "Select ID from personas where DNI = '" + persona.DNI +"'";
                    conexion.Open();
                    lector = comando.ExecuteReader();
                    lector.Read();
                    if (lector.HasRows)
                    {
                        idPersona = (int)lector["ID"];
                        return idPersona;
                    } else
                    {
                        return idPersona;
                    }
                } else
                {
                    return idPersona;
                }
            }
            catch(Exception ex)
            {
                return idPersona;
            }
        }
        public Personas ExistePersona (string DNI)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            Personas nuevo = null;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select id, Nombre, Apellido, FNacimiento, Email, Telefono, DNI, FechaAlta from personas where DNI = '" + DNI +"'";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                if (lector.HasRows)
                {
                    nuevo = new Personas();
                    nuevo.idpersona = (int)lector["id"];
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
            catch(Exception ex)
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
