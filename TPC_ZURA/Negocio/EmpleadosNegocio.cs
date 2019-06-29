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
    public class EmpleadosNegocio
    {
        public List<Empleados> ListarEmpleados()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Empleados> listado = new List<Empleados>();
            Empleados nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                
                comando.CommandText = "select pu.ID as IDPuesto, pu.nombre as nombrePuesto, pu.nombre, emp.ID, emp.IDPersona, emp.IDPuesto, emp.FechaAlta as altaEmpleado, emp.Activo, p.Nombre, p.Apellido, p.FNacimiento, p.Email, p.Telefono, p.DNI, p.FechaAlta as altaPersona from Empleados as emp inner join Personas as p on emp.IDPersona = p.ID inner join Puestos as pu on pu.ID = emp.IDPuesto where emp.activo = 1";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    nuevo = new Empleados();
                    nuevo.legajo = (int)lector["ID"];
                    nuevo.puesto = new Puestos();
                    nuevo.puesto.id = (int)lector["IDPuesto"];
                    nuevo.puesto.Nombre = lector["nombrePuesto"].ToString();
                    nuevo.altaEmpleado = (DateTime)lector["altaEmpleado"];
                    nuevo.idpersona = (int)lector["IDPersona"];
                    nuevo.nombre = lector["Nombre"].ToString();
                    nuevo.apellido = lector["Apellido"].ToString();
                    nuevo.fnacimiento = (DateTime)lector["FNacimiento"];
                    nuevo.email = lector["Email"].ToString();
                    nuevo.telefono = (int)lector["Telefono"];
                    nuevo.DNI = lector["DNI"].ToString();
                    nuevo.altaPersona = (DateTime)lector["altaPersona"];
                    //nuevo = new Empleados(
                    //    lector.GetInt32(0), lector.GetInt32(1), lector.GetInt32(2), lector.GetString(3),
                    //    lector.GetString(4), lector.GetString(5), lector.GetInt32(6), lector.GetString(7),
                    //    lector.GetDateTime(8), lector.GetString(9), lector.GetInt32(10), (bool)lector["activo"]
                    //);

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
        public List<Puestos> ListarPuestos()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Puestos> listado = new List<Puestos>();
            Puestos nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select id, nombre from puestos";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    //nuevo = new TipoUsuario((int)lector["id"], lector["nombre"].ToString());
                    nuevo = new Puestos();
                    nuevo.id = (int)lector["id"];
                    nuevo.Nombre = lector["nombre"].ToString();
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
        public bool ExisteEmpleado(string DNI)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select * from Empleados as emp inner join Personas as p on emp.IDPersona = p.ID where p.DNI = '"+ DNI +"' and emp.activo = 1";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
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
        public bool InsertarEmpleado (Empleados empleado)
        {
            PersonasNegocio negocioP = new PersonasNegocio();
            int idPersona = 0;
            if (empleado.idpersona == 0)
            {
                idPersona = negocioP.AgregarPersona((Personas)empleado);
                if (idPersona != 0)
                {
                    SqlConnection conexion = new SqlConnection();
                    SqlCommand comando = new SqlCommand();
                    SqlDataReader lector;

                    conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "insert into empleados (IDPersona, IDPuesto)" +
                        " values (" + idPersona + "," + empleado.puesto.id + ")";
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
                } else
                {
                    return false;
                }
            } else
            {
                idPersona = empleado.idpersona;
                SqlConnection conexion = new SqlConnection();
                SqlCommand comando = new SqlCommand();
                SqlDataReader lector;

                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "insert into empleados (IDPersona, IDPuesto)" +
                    " values (" + idPersona + "," + empleado.puesto.id + ")";
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
        public bool BajaEmpleado(Empleados empleado)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "update empleados set activo = 0 where ID =" + empleado.legajo;
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
        public bool ModificaEmpleado(Empleados empleado)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "update personas set Email = '" + empleado.email + "', Telefono = " + empleado.telefono + " where ID =" + empleado.idpersona;
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
            lector.Read();
            if (lector.RecordsAffected > 0)
            {
                conexion.Close();
                comando.CommandText = "update empleados set IDPuesto = " + empleado.puesto.id + " where ID =" + empleado.legajo;
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
        public int PuestoPorEmpleado(int IDEmpleado)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select IDPuesto from empleados where ID = " + IDEmpleado;
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                lector.Read();
                if (lector.HasRows)
                {
                    return (int)lector["IDPuesto"];
                }
                else
                {
                    return 0;
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
