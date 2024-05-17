using CrudBet8MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudBet8MVC.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Agregar los modelos aqui(cada modelo corresponde a una tabla en la BD)
        public DbSet<Contacto> Contacto { get; set; }
        
    }
}
