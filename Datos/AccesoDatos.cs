using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace Datos
{
    public class AccesoDatos
    {
        static string cadenaConexion = "server = .\\SQLEXPRESS; database = DISCOS_DB; integrated security=true";
        private SqlConnection conexion = new SqlConnection(cadenaConexion);
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader lector;
        public SqlDataReader Lector { get { return lector; } }
        /*
         private SqlDataReader lector; declara una variable privada llamada "lector" 
        de tipo SqlDataReader. Esta variable se utiliza para almacenar el resultado 
        de una consulta a la base de datos.

        public SqlDataReader Lector define una propiedad pública llamada "Lector" 
        que permite acceder al valor almacenado en la variable "lector". 
        Una propiedad es una forma de acceder y modificar el valor de una variable privada 
        de una clase desde fuera de la clase.

        get { return lector; } define un bloque de código que se ejecuta cuando se accede
        a la propiedad "Lector". En este caso, simplemente devuelve el valor almacenado
        en la variable "lector". Esto significa que cuando se accede a la propiedad "Lector", 
        se obtiene el objeto SqlDataReader almacenado en la variable "lector".

        En resumen, esta función define una propiedad pública llamada "Lector" que proporciona
        acceso al objeto SqlDataReader almacenado en la variable privada "lector". 
        Esto permite que otras partes del código accedan y utilicen el lector de resultados 
        de una consulta a la base de datos.
         */

        //Metodos
        public SqlConnection abrir()
        {
            if(conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
            return conexion;
        }
        public SqlConnection cerrar()
        {
            if(conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
            return conexion;
        }
    }
}
