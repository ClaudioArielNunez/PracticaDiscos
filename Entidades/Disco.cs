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
        private int Id { get; set; }
        private string Titulo { get; set; }
        private DateTime FechaLanzamiento { get; set; }
        private int CantCanciones { get; set; } 
        private string UrlImagen { get; set; }  
        private int IdEstilo { get; set; }    
        private int IdTipoEdicion { get; set; }


    }
}
