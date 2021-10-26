using ApiPeliculas.Data;
using ApiPeliculas.Models;
using ApiPeliculas.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository
{
    public class PeliculaRepository : IPeliculaRepository //HEREDA DE LA INTERFAZ
    {

        private readonly ApplicationDbContext _bd;
        //Instanciamos el contexto

        public PeliculaRepository(ApplicationDbContext bd) //
        {
            _bd = bd;
        }
        public bool ActualizarPelicula(Pelicula modelo)
        {
            _bd.Pelicula.Update(modelo);
            return Guardar();
                
        }

        public bool BorrarPelicula(Pelicula modelo)
        {
            _bd.Pelicula.Remove(modelo);
            return Guardar();

        }

        public IEnumerable<Pelicula> BuscarPelicula(string nombre)
        {
            IQueryable<Pelicula> query = _bd.Pelicula;
            
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Nombre.Contains(nombre) || e.Descripcion.Contains(nombre));
            }

            return query.ToList();
        }

        public bool CrearPelicula(Pelicula modelo)
        {
            _bd.Pelicula.Add(modelo);
            return Guardar();
        }

        public bool ExistePelicula(string nombre)
        {
            bool valor = _bd.Pelicula.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;

        }

        public bool ExistePelicula(int PeliculaId)
        {
            return _bd.Pelicula.Any(c => c.Id == PeliculaId);
        }

        public Pelicula GetPelicula(int PeliculaId)
        {
            return _bd.Pelicula.FirstOrDefault(c => c.Id == PeliculaId);
            //return _bd.Pelicula.Include(ca => ca.categoria).FirstOrDefault(c => c.Id == PeliculaId);
        }

        public ICollection<Pelicula> GetPeliculas()
        {
            return _bd.Pelicula.OrderBy(c => c.Nombre).ToList();
        }

        public ICollection<Pelicula> GetPeliculasEnCategoria(int categoriaId)
        {
            return _bd.Pelicula.Include(ca => ca.categoria).Where(ca => ca.CategoriaId == categoriaId).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
