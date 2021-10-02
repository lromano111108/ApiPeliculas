using ApiPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.IRepository
{
    //SE AGREGA EL PUBLIC
    //contiene los metodos para el modelo categoria
    public interface ICategoriaRepository
    {
        //METODOS PARA REALIZAR OPERACIONES PARA CATEGORIA
        ICollection<Categoria> GetCategorias();

        Categoria GetCategoria(int CategoriaId);
        bool ExisteCategoria(string nombre);
        bool ExisteCategoria(int categoriaId);
        bool CrearCategoria(Categoria modelo);
        bool ActualizarCategoria(Categoria modelo);
        bool BorrarCategoria(Categoria modelo);
        bool Guardar();




    }
}
