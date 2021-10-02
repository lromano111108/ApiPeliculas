using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.PeliculasMapper
{
    public class PeliculasMapper : Profile //hereda de Profile
    {
        public PeliculasMapper()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap(); //vincula a Categoria con Categoria DTO para pasar de de uno a otro y viceversa
        }
    }
}
