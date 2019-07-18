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
    public class AuditoriasNegocio
    {
        public List<Auditorias> ListarMovimientos()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Auditorias> listado = new List<Auditorias>();
            Auditorias nuevo;

            try
            {
                conexion.ConnectionString = AccesoDatosManager.cadenaConexion;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select p.DNI, mov.id, us.id usuid, us.usuario, mov.idreclamo reclamo, mov.observaciones, mov.accion, mov.fechahora from movimientosreclamos as mov inner join Usuarios as us on us.id = mov.idusuarioejecuto inner join Personas as p on p.id = us.IDPersona";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    nuevo = new Auditorias();
                    nuevo.id = (int)lector["id"];
                    nuevo.Reclamo = new Reclamo();
                    nuevo.Reclamo.id = (int)lector["reclamo"];
                    nuevo.UsuarioEjecuto = new Usuarios();
                    nuevo.UsuarioEjecuto.id = (int)lector["usuid"];
                    nuevo.UsuarioEjecuto.usuario = lector["usuario"].ToString();
                    nuevo.UsuarioEjecuto.DNI = lector["DNI"].ToString();
                    nuevo.Observaciones = lector["observaciones"].ToString();
                    nuevo.FechaHora = (DateTime)lector["fechahora"];
                    nuevo.Accion = lector["accion"].ToString();

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
