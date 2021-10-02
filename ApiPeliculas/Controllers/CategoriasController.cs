using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/[CategoriasController]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _ctRepo;
        public CategoriasController(ICategoriaRepository ctRepo)
        {
            _ctRepo = ctRepo;//para poder usarlo en toda la aplicacion
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctRepo.GetCategorias();
            return Ok(listaCategorias);
        }


    }
}
