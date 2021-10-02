using ApiPeliculas.Data;
using ApiPeliculas.Models;
using ApiPeliculas.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository
{
    public class CategoriaRepository : ICategoriaRepository //HEREDA DE LA INTERFAZ
    {

        private readonly ApplicationDbContext _bd;
        //Instanciamos el contexto

        public CategoriaRepository(ApplicationDbContext bd) //
        {
            _bd = bd;
        }
        public bool ActualizarCategoria(Categoria modelo)
        {
            _bd.Categoria.Update(modelo);
            return Guardar();
        }

        public bool BorrarCategoria(Categoria modelo)
        {
            _bd.Categoria.Remove(modelo);
            return Guardar();
        }

        public bool CrearCategoria(Categoria modelo)
        {
            _bd.Categoria.Add(modelo);
            return Guardar();
        }

        public bool ExisteCategoria(string nombre)
        {
            bool valor = _bd.Categoria.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            //busca en Categoria si hay algun nombre en minuscula sin espacios que sea igual al parametro nombre en minuscula sin espacios

            return valor;
        }

        public bool ExisteCategoria(int categoriaId)
        {
            return _bd.Categoria.Any(c => c.Id == categoriaId);
              //pregunta si existe algun id en tabla categoria q sea igual al parametro categoriaId
        }

        public Categoria GetCategoria(int CategoriaId)
        {
            return _bd.Categoria.FirstOrDefault(c => c.Id == CategoriaId);
            //nos busca el primero o por Defecto de la consulta segun el ID q le pasamos
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _bd.Categoria.OrderBy(c => c.Nombre).ToList();
            //devuelve tabla categoria ordenada por c.Nombre de manera ASCENDENTE
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
            //si es verdadero devuelve (?) un numero mayor que cero devuelve true sino devuelve false
        }
    }
}
