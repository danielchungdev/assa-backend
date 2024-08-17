using MarcasAutos.Data;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigurarWebHost(builder);
        ConfigurarServicios(builder);

        var app = builder.Build();
        AplicarMigrations(app);
        ConfigurarApp(app);

        app.Run();
    }
    
    //Configure webhost
    private static void ConfigurarWebHost(WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ListenAnyIP(80);
        });
    }
    
    //Funcion para agregar los servicios
    private static void ConfigurarServicios(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        AgregarDatabase(builder);
    }

    //Funcion para agregar PostgreSQL y poder conectarse a la base de datos

    private static void AgregarDatabase(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("psqlconn");
        builder.Services.AddDbContext<MarcasAutosDbContext>(options =>
            options.UseNpgsql(connectionString)
        );
    }

    // Funcion para aplicar las migraciones.
    private static void AplicarMigrations(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<MarcasAutosDbContext>();
            context.Database.Migrate();
            Console.WriteLine("Migrations aplicadas correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hubo un error con los migrations: {ex.Message}");
        }
    }

    //Funcion para agregar swagger solamente en development
    private static void ConfigurarApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapControllers();
    }
}