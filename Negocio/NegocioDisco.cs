using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class NegocioDisco
    {
        public List<Disco> Listar()
        {
            List<Disco> ListaDiscos = new List<Disco>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * from DISCOS");
                datos.leerTabla();

                while (datos.Lector.Read())
                {
                    Disco nuevoDisco = new Disco();
                    nuevoDisco.Id = (int)datos.Lector["id"]; //El nombre entre [] tiene q ser igual a la BD
                    nuevoDisco.Titulo = (string)datos.Lector["titulo"];
                    nuevoDisco.FechaLanzamiento = (DateTime)datos.Lector["fechaLanzamiento"];
                    nuevoDisco.CantCanciones = (int)datos.Lector["cantidadCanciones"];
                    nuevoDisco.UrlImagen = (string)datos.Lector["urlImagenTapa"];
                    nuevoDisco.IdEstilo = (int)datos.Lector["idEstilo"];
                    nuevoDisco.IdTipoEdicion = (int)datos.Lector["idTipoEdicion"];

                    ListaDiscos.Add(nuevoDisco);
                }
                return ListaDiscos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrar();
            }

        }
    }
}
