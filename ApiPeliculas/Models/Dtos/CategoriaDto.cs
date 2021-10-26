using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Models.Dtos
{
    public class CategoriaDto
    {
        //CREAR CAMPOS EN BD
        public int Id { get; set; }
        [Required(ErrorMessage ="El Nombre es Obligatorio")]
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
