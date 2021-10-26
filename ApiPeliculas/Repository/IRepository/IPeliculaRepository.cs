using ApiPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.IRepository
{
    //SE AGREGA EL PUBLIC
    //contiene los metodos para el modelo categoria
    public interface IPeliculaRepository
    {
        //METODOS PARA REALIZAR OPERACIONES PARA PELICULA
        ICollection<Pelicula> GetPeliculas();
        ICollection<Pelicula> GetPeliculasEnCategoria(int categoriaId);
        Pelicula GetPelicula(int PeliculaId);
        bool ExistePelicula(string nombre);
        IEnumerable<Pelicula> BuscarPelicula(string nombre);//busca por pelicula
        bool ExistePelicula(int PeliculaId);
        bool CrearPelicula(Pelicula modelo);
        bool ActualizarPelicula(Pelicula modelo);
        bool BorrarPelicula(Pelicula modelo);
        bool Guardar();




    }
}
