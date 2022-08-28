using System.Diagnostics;
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
    [Route("api/libros")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly AplicationDbContext context;

        public  LibrosController(AplicationDbContext context)
        {
            this.context=context;
        } 

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id){
            return await this.context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await this.context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if(!existeAutor){
                return BadRequest("El autor no se encuentra en el registro");
            }   
            this.context.Add(libro);
            await this.context.SaveChangesAsync();
            return Ok();
        }

    }
}