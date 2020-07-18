using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.Contexts;
using MiPrimerWebApiM3.Entities;
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
        public AutoresController(ApplicationDbContext context)
        {
            this.context = context; 
        }

        // GET api/autores
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
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
            var autor = await context.Autores.Include(l => l.Libros).FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
            {
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
