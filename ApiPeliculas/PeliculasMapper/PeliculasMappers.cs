using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.PeliculasMapper
{
    public class PeliculasMappers : Profile //hereda de Profile
    {
        public PeliculasMappers()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap(); //vincula a Categoria con Categoria DTO para pasar de de uno a otro y viceversa
            CreateMap<Pelicula, PeliculaDto>().ReverseMap(); //vincula a Pelicula con PeliculaDTO para pasar de de uno a otro y viceversa
            CreateMap<Pelicula, PeliculaCreateDto>().ReverseMap(); //vincula a Pelicula con PeliculaCreateDTO para pasar de de uno a otro y viceversa
            CreateMap<Pelicula, PeliculaUpdateDto>().ReverseMap(); //vincula a Pelicula con PeliculaUpdate para pasar de de uno a otro y viceversa

        }
    }
}
