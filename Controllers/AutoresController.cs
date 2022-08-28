using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAutores.Entidades;
namespace WebAPIAutores.Controllers
{
    [Route("api/autores")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly AplicationDbContext context;
        public AutoresController(AplicationDbContext context)
        {
            this.context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            var lista_autores = await this.context.Autores.Include(x => x.Libros).ToListAsync();
            return lista_autores; 
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            this.context.Add(autor);
            await this.context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult>Put(Autor autor,int id){
            if(autor.Id != id){
                return BadRequest("El id del autor no concide con el id de la url");
            }

            var existe = await this.context.Autores.AnyAsync(x => x.Id == id);
            if(!existe){
                return NotFound();
            }

            this.context.Update(autor);
            await this.context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult>Delete(int id){
            var existe = await this.context.Autores.AnyAsync(x => x.Id == id);
            if(!existe){
                return NotFound();
            }
            this.context.Remove(new Autor(){Id = id});
            await this.context.SaveChangesAsync();
            return Ok();
        }
    }
}