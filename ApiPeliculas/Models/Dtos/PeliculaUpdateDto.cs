using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ApiPeliculas.Models.Pelicula;

namespace ApiPeliculas.Models.Dtos
{
    public class PeliculaUpdateDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es Obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "LA ruta de la imagen es Obligatorio")]
        public string RutaImagen { get; set; }

        [Required(ErrorMessage = "LA descripcion es Obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La Duracion de la Pelicula es es Obligatorio")]
        public string Duracion { get; set; }

        public TipoClasificacion Clasificacion { get; set; }

        //public DateTime FechaCreacion { get; set; }
        public int CategoriaId { get; set; }

    }
}
