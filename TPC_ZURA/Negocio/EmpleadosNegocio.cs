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
                //MSF-20190420: agregué todos los datos del heroe. Incluso su universo, que lo traigo con join.
                comando.CommandText = "select emp.id as idempleado, us.id, pr.ID as idpersona, pr.Nombre, pr.Apellido, us.Usuario, us.TipoUsuario as tipo, tius.nombre, pr.FNacimiento, pr.Email, pr.Telefono, us.activo from Empleados as emp inner join Usuarios as us on emp.IDUsuario = us.ID inner join Personas as pr on us.IDPersona = pr.ID inner join TiposUsuarios as tius on us.TipoUsuario = tius.ID where us.activo = 1";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    nuevo = new Empleados(
                        lector.GetInt32(0), lector.GetInt32(1), lector.GetInt32(2), lector.GetString(3),
                        lector.GetString(4), lector.GetString(5), lector.GetInt32(6), lector.GetString(7),
                        lector.GetDateTime(8), lector.GetString(9), lector.GetInt32(10), (bool)lector["activo"]
                    );

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
