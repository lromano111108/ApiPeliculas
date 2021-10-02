using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Models
{
    public class Categoria
    {
        //CREAR CAMPOS EN BD
        [Key] //CREA CLAVE PRIMARIA
        public int Id { get; set; }

        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
