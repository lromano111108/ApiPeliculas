using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/[Controller]")] //ruta 
    [ApiController]
    public class PeliculasController : Controller
    {
        private readonly IPeliculaRepository _pelRepo;//a traves de este repositorio vamos a usar estos metodos, por eso lo instanciamos.
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;//IMPORT Microsoft.AspNetCore.Hosting;

        public PeliculasController(IPeliculaRepository pelRepo, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _pelRepo = pelRepo;//para poder usarlo en toda la aplicacion
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult GetPeliculas()
        {
            var listaPeliculas = _pelRepo.GetPeliculas();

            var listaPeliculasDto = new List<PeliculaDto>();

            foreach (var lista in listaPeliculas)
            {
                listaPeliculasDto.Add(_mapper.Map<PeliculaDto>(lista));//Mappea los elementos de la lista del Modelo Pelicula (Vienen asi de la lista Peliculas) a Pelicula DTo y los pone en la listaPeliculasDTO
            }

            return Ok(listaPeliculasDto);
        }


        [HttpGet("{PeliculaId:int}", Name = "GetPelicula")] //se aclaran las rutas y se aclara que va a recibir un ID que es INT y que el nombre es GETPelicula
        public IActionResult GetPelicula(int PeliculaId)
        {
            var itemPelicula = _pelRepo.GetPelicula(PeliculaId);
            if (itemPelicula == null)
            {
                return NotFound();
            }

            var itemPeliculaDto = _mapper.Map<PeliculaDto>(itemPelicula);//cpmvierte Pelicula en PeliculaDTO

            return Ok(itemPeliculaDto);

        }

        [HttpGet("GetPeliculasEnCategoria/{categoriaId:int}")]
        public ActionResult GetPeliculasEnCategoria(int categoriaId)
        {
            var listaPelicula = _pelRepo.GetPeliculasEnCategoria(categoriaId);
            if (listaPelicula == null)
            {
                return NotFound();
            }

            var itemPelicula = new List<PeliculaDto>();
            foreach (var item in listaPelicula)
            {
                itemPelicula.Add(_mapper.Map<PeliculaDto>(item));
            }
            return Ok(itemPelicula);
        }

        [HttpGet ("Buscar")]
        public IActionResult Buscar(string nombre)
        {
            try
            {
                var resultado = _pelRepo.BuscarPelicula(nombre);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos de la aplicacion");
                throw;
            }
        }




        [HttpPost]
        public IActionResult CrearPelicula([FromForm] PeliculaCreateDto PeliculaDto) //FromForm esta vinculando a un formulario
     
        {
            if (PeliculaDto == null) //sino encuentra la pelicula
            {
                return BadRequest(ModelState);
            }

            if (_pelRepo.ExistePelicula(PeliculaDto.Nombre))
            {
                ModelState.AddModelError("", "La Pelicula Ya Existe");
                return StatusCode(404, ModelState);
            }

            //Subida de Archivos//

            var archivo = PeliculaDto.Foto;//
            string rutaPrincipal = _hostingEnvironment.WebRootPath;//para crear la ruta donde van a quedar los archivos alojados
            var archivos = HttpContext.Request.Form.Files;
            if (archivo.Length > 0)
            {//nueva imagen
                var nombreFoto = Guid.NewGuid().ToString(); //Guid es un campo que contiene unan secuencia de caracteres irrepetibles q sirve para no superponer nombres iguales en las fotos
                var subidas = Path.Combine(rutaPrincipal, @"fotos"); //IMPORTAMOS SYSTEM.IO // 
                var extension = Path.GetExtension(archivos[0].FileName); //


                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreFoto + extension), FileMode.Create)) //
                {
                    archivos[0].CopyTo(fileStreams);
                }
                PeliculaDto.RutaImagen = @"\fotos\" + nombreFoto + extension;
                    
            }                        

            var Pelicula = _mapper.Map<Pelicula>(PeliculaDto);

            if (!_pelRepo.CrearPelicula(Pelicula))
            {
                ModelState.AddModelError("", $"Algo Salio Mal Guardando el Registro{Pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }

            //return Ok();
            return CreatedAtRoute("GetPelicula", new { PeliculaId = Pelicula.Id, Pelicula });// para que nos devuelva un 201 created y muestre el registro creado

        }


        [HttpPatch("{PeliculaId:int}", Name = "ActualizarPelicula")]
        public IActionResult ActualizarPelicula(int PeliculaId, [FromBody] PeliculaDto PeliculaDto)
        {
            if (PeliculaDto == null || PeliculaId != PeliculaDto.Id)
            {
                return BadRequest(ModelState);
            }

            var Pelicula = _mapper.Map<Pelicula>(PeliculaDto);

            if (!_pelRepo.ActualizarPelicula(Pelicula))
            {
                ModelState.AddModelError("", $"Algo Salio Mal Actualizando el Registro{Pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{PeliculaId:int}", Name = "BorrarPelicula")]
        public IActionResult BorrarPelicula(int PeliculaId)
        {


            //var Pelicula = _mapper.Map<Pelicula>(PeliculaDto);

            if (!_pelRepo.ExistePelicula(PeliculaId))
            {
                return NotFound();
            }


            var Pelicula = _pelRepo.GetPelicula(PeliculaId);

            if (!_pelRepo.ActualizarPelicula(Pelicula))
            {
                ModelState.AddModelError("", $"Algo Salio Mal Borrando el Registro{Pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
