using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/[Controller]")] //ruta 
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _ctRepo;//a traves de este repositorio vamos a usar estos metodos, por eso lo instanciamos.
        private readonly IMapper _mapper;
        public CategoriasController(ICategoriaRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;//para poder usarlo en toda la aplicacion
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctRepo.GetCategorias();

            var listaCategoriasDto = new List<CategoriaDto>();

            foreach (var lista in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(lista));//Mappea los elementos de la lista del Modelo CAtegoria (Vienen asi de la lista Categorias) a CAtegoria DTo y los pone en la listaCategoriasDTO
            }

            return Ok(listaCategoriasDto);
        }


        [HttpGet("{categoriaId:int}", Name = "GetCategoria")] //se aclaran las rutas y se aclara que va a recibir un ID que es INT y que el nombre es GETCATEGORIA
        public IActionResult GetCategoria(int categoriaId)
        {
            var itemCategoria = _ctRepo.GetCategoria(categoriaId);
            if (itemCategoria == null)
            {
                return NotFound();
            }
                        
            var itemCategoriaDto = _mapper.Map<CategoriaDto>(itemCategoria);//cpmvierte Categoria en CategoriaDTO

            return Ok(itemCategoriaDto);

        }


        [HttpPost]
        public IActionResult CrearCategoria([FromBody] CategoriaDto categoriaDto) //FromBody esta vinculando la peticion o Request con la categoria DTO
        {
            if (categoriaDto== null)
            {
                return BadRequest(ModelState);
            }

            if (_ctRepo.ExisteCategoria(categoriaDto.Nombre))
            {
                ModelState.AddModelError("", "La Categoria Ya Existe");
                return StatusCode(404, ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            if (!_ctRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo Salio Mal Guardando el Registro{categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            
            //return Ok();
            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id },categoria);// para que nos devuelva un 201 created y muestre el registro creado
        }

        [HttpPatch("{categoriaId:int}", Name = "ActualizarCategoria")]
        public IActionResult ActualizarCategoria(int categoriaId, [FromBody] CategoriaDto categoriaDto)
        {
            if (categoriaDto == null || categoriaId != categoriaDto.Id)
            {
                return BadRequest(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            if (!_ctRepo.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo Salio Mal Actualizando el Registro{categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }   
        
        
        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        public IActionResult BorrarCategoria(int categoriaId)
        {
           

            //var categoria = _mapper.Map<Categoria>(categoriaDto);

            if (!_ctRepo.ExisteCategoria(categoriaId))
            {
                return NotFound();
            }


            var categoria = _ctRepo.GetCategoria(categoriaId);

            if (!_ctRepo.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo Salio Mal Borrando el Registro{categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
