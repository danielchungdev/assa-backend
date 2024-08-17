using MarcasAutos.Data;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ListenAnyIP(80);
        });

        ConfigurarServicios(builder);

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MarcasAutosDbContext>();
            try
            {
                // Try to connect to the database and perform a simple query
                dbContext.Database.ExecuteSqlRaw("SELECT 1");
                Console.WriteLine("Successfully connected to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
            }
        }

        // Agrega swagger si esta en dev.
        if (app.Environment.IsDevelopment())
        {
            // Swagger documentation
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Endpoint Routing
        app.MapControllers();

        // Empieza la applicacion
        app.Run();
    }

    public static void ConfigurarServicios(WebApplicationBuilder builder)
    {
        // Agregar los controlees 
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        // Agregar Swagger alos servicios
        builder.Services.AddSwaggerGen();

        // Conseguir el connection string de la base de datos, del configuration file.
        var connectionString = builder.Configuration.GetConnectionString("psqlconn");

        // Agregar el database context, y configurarlo para usar PostgreSQL
        builder.Services.AddDbContext<MarcasAutosDbContext>(options =>
            options.UseNpgsql(connectionString)
        );
    }
}