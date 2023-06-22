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
                datos.setearConsulta("SELECT D.Id, Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa,D.IdEstilo ,E.Descripcion AS Estilo, D.IdTipoEdicion,T.Descripcion as TipoEdicion FROM DISCOS D , ESTILOS E, TIPOSEDICION T WHERE D.IdEstilo = E.Id AND D.IdTipoEdicion = T.Id");
                datos.leerTabla();

                while (datos.Lector.Read())
                {
                    Disco nuevoDisco = new Disco();
                    nuevoDisco.Id = (int)datos.Lector["id"]; //El nombre entre [] tiene q ser igual a la BD
                    nuevoDisco.Titulo = (string)datos.Lector["titulo"];
                    nuevoDisco.FechaLanzamiento = (DateTime)datos.Lector["fechaLanzamiento"];
                    nuevoDisco.CantCanciones = (int)datos.Lector["cantidadCanciones"];
                    nuevoDisco.UrlImagen = (string)datos.Lector["urlImagenTapa"];

                    nuevoDisco.Estilo = new Estilo();
                    nuevoDisco.Estilo.Id = (int)datos.Lector["IdEstilo"];//necesario para el frm modificar
                    nuevoDisco.Estilo.Descripcion = (string)datos.Lector["Estilo"];

                    nuevoDisco.TipoEdicion = new TipoEdicion();
                    nuevoDisco.TipoEdicion.Id = (int)datos.Lector["IdTipoEdicion"];//necesario para el frm modificar
                    nuevoDisco.TipoEdicion.Descripcion = (string)datos.Lector["TipoEdicion"];

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
        public void agregar(Disco nuevoDisco)
        {
             AccesoDatos nuevoAcceso = new AccesoDatos();
            try
            {                
                nuevoAcceso.setearConsulta("INSERT INTO DISCOS(Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, IdEstilo, IdTipoEdicion) VALUES(@Titulo, @FechaLanzamiento, @CantidadCanciones, @UrlImagenTapa, @IdEstilo, @IdTipoEdicion)");
                
                nuevoAcceso.setearParametros("@Titulo", nuevoDisco.Titulo);
                nuevoAcceso.setearParametros("@FechaLanzamiento", nuevoDisco.FechaLanzamiento);
                nuevoAcceso.setearParametros("@CantidadCanciones", nuevoDisco.CantCanciones);
                nuevoAcceso.setearParametros("@UrlImagenTapa", nuevoDisco.UrlImagen);
                nuevoAcceso.setearParametros("@IdEstilo", nuevoDisco.Estilo.Id);
                nuevoAcceso.setearParametros("@IdTipoEdicion", nuevoDisco.TipoEdicion.Id);


                nuevoAcceso.ejecutarAccion();
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
        public void modificar(Disco disco)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE DISCOS SET Titulo = @titulo, FechaLanzamiento = @fechaLanzamiento, CantidadCanciones=@cantidadCanciones, UrlImagenTapa=@urlImagenTapa, IdEstilo=@idEstilo, IdTipoEdicion=@idTipoEdicion WHERE Id = @id");
                datos.setearParametros("@titulo", disco.Titulo);
                datos.setearParametros("@fechaLanzamiento",disco.FechaLanzamiento);
                datos.setearParametros("@cantidadCanciones",disco.CantCanciones);
                datos.setearParametros("@urlImagenTapa",disco.UrlImagen);
                datos.setearParametros("@idEstilo",disco.Estilo.Id);
                datos.setearParametros("@idTipoEdicion",disco.TipoEdicion.Id);
                datos.setearParametros("@id",disco.Id);
                datos.ejecutarAccion();
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
