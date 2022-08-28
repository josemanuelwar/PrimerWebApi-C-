using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAutores.Entidades;
namespace WebAPIAutores
{
    public class AplicationDbContext : DbContext
    {
      public AplicationDbContext(DbContextOptions options):base(options){

      }
      public DbSet<Autor> Autores { get; set; }
      public DbSet<Libro> Libros { get; set; }    
    }
}