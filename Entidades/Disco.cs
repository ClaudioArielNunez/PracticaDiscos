using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entidades
{
    public class Disco
    {
        //Atributos
        public int Id { get; set; }       
        public string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public int CantCanciones { get; set; } 
        public string UrlImagen { get; set; }  
        public int IdEstilo { get; set; }    
        public int IdTipoEdicion { get; set; }

        //Getters && Setters
        

    }
}
