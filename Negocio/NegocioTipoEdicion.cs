using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class NegocioTipoEdicion
    {
        private List<TipoEdicion> listaTiposEdicion = new List<TipoEdicion>();

        public List<TipoEdicion> listar()
        {
                AccesoDatos nuevoAcceso = new AccesoDatos();
            try
            {
                nuevoAcceso.setearConsulta("SELECT Id, Descripcion FROM TIPOSEDICION");
                nuevoAcceso.leerTabla();

                while(nuevoAcceso.Lector.Read())
                {
                    TipoEdicion tipoEdicion = new TipoEdicion();
                    tipoEdicion.Id = (int)nuevoAcceso.Lector["id"];
                    tipoEdicion.Descripcion = (string)nuevoAcceso.Lector["Descripcion"];

                    listaTiposEdicion.Add(tipoEdicion);
                }
                return listaTiposEdicion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                nuevoAcceso.cerrar();
            }
        }
    }
}
