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
    }
}
