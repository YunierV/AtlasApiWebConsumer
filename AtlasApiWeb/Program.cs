
using AtlasApiWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace AtlasApiWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //inyectamos el string de conexion en nuestro inyector de dependencias "Program.cs"
            //para realizar la inyeccion, ingresamos el contexto de la base de datos en el contenedor, asi como su strinfg de conecion
            //esto lo que va a hacer es añadir la ConnectioString al contenedor de inyecion de dependencias (lo que hace que el servicio este disponible para toda la aplicacion) por ello cuando se trabaje con la controladorea podremos pasar una instancia de la "AppDbContext" automaticamente
            //realizamos la migracion (esto se hace en la console del administrador de paquetes) -Add-Migration InitDb- //para reflejar lo cambio en el servidor local -update-database-
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
