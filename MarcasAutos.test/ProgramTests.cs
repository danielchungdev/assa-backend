using Microsoft.AspNetCore.Builder;
using MarcasAutos.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MarcasAutos.Tests
{
    public class ProgramTests
    {

        [Fact]
        public void ConfigureServices_RegistersRequiredServices()
        {
            var builder = WebApplication.CreateBuilder();
            Program.ConfigurarServicios(builder);
            var services = builder.Services;
            // Asegurarnos que tiene nuestro db context
            Assert.Contains(services, s => s.ServiceType == typeof(MarcasAutosDbContext));
        }

    }
}