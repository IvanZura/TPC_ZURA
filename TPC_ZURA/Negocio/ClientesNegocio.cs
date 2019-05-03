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
                comando.CommandText = "select cl.id as idcliente, us.id, pr.ID as idpersona, pr.Nombre, pr.Apellido, us.Usuario, us.TipoUsuario as tipo, tius.nombre, pr.FNacimiento, pr.Email, pr.Telefono from Clientes as cl inner join Usuarios as us on cl.IDUsuario = us.ID inner join Personas as pr on us.IDPersona = pr.ID inner join TiposUsuarios as tius on us.TipoUsuario = tius.ID";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    nuevo = new Clientes(
                        lector.GetInt32(0), lector.GetInt32(1), lector.GetInt32(2), lector.GetString(3),
                        lector.GetString(4), lector.GetString(5), lector.GetInt32(6), lector.GetString(7),
                        lector.GetDateTime(8).ToString(), lector.GetString(9), lector.GetInt32(10)
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
