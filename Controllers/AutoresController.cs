using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiPrimerWebApiM3.Contexts;
using MiPrimerWebApiM3.Entities;
using MiPrimerWebApiM3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Controllers
{
    // api/autores
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context,
            ILogger<AutoresController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        // GET api/autores
        [HttpGet]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            throw new NotImplementedException();
            logger.LogInformation("Obteniendo los actores");
            return context.Autores.Include(l => l.Libros).ToList();
        }

        // GET api/autores/primer
        [HttpGet("primer")]
        public ActionResult<Autor> GetPrimerAutor()
        {
            return context.Autores
                   .Include(l => l.Libros)
                   .FirstOrDefault();
        }

        // GET api/autores/1
        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<Autor>> Get(int id) 
        {
            logger.LogDebug("Buscando autor de Id " + id.ToString());
            var autor = await context.Autores.Include(l => l.Libros).FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
            {
                logger.LogWarning($"El autor de Id {id} no ha sido encontrado");
                return NotFound();
            }

            return autor;
        }

        // POST api/autores
        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            context.Autores.Add(autor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autor);
        }

        // PUT api/autores/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [ FromBody] Autor value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        // DELETE api/autores/1
        [HttpDelete("{id}")]
        public ActionResult <Autor> Delete(int id)
        {
            var autor = context.Autores.FirstOrDefault(a => a.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }
    }
}
