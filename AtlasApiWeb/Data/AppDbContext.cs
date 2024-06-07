using AtlasApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AtlasApiWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } //opciones por defecto //el string de conexion podriamos ponerlo aqui, pero como estamos trabajando en ASP.NET lo mejor es ocultarlo dentro de los appSetting.json e invocarlo directamente desde el programa (-ConnectionStrings)

        public DbSet<AtlasPhoto> Photos { get; set; } //DbSet<AtlasPhoto>: va a usar para mapear la clase AtrasPhoto en la base de datos //Photos_ la vautizamos "Photos"
    }
}
