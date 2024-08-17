using MarcasAutos.Data;
using Microsoft.EntityFrameworkCore;

namespace MarcasAutos.Tests
{
    public class MarcasAutosDbContextTests
    {
        [Fact]
        public void MarcasAutosDbContext_HasMarcasAutosDbSet()
        {
            var options = new DbContextOptionsBuilder<MarcasAutosDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase1")
                .Options;

            using var context = new MarcasAutosDbContext(options);

            //Verificar que nuestro contexto no sea null
            Assert.NotNull(context.MarcasAutos);
        }

        [Fact]
        public void OnModelCreating_SeedsData()
        {
            var options = new DbContextOptionsBuilder<MarcasAutosDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase2")
                .Options;

            using var context = new MarcasAutosDbContext(options);
            context.Database.EnsureCreated();

            // Verifica que contiente 5 resultados
            Assert.Equal(5, context.MarcasAutos.Count());
            
            //Verifica resultados individualtes
            Assert.Contains(context.MarcasAutos, m => m.Id == 1 && m.Nombre == "Audi" && m.Creador == "August Horch");
            Assert.Contains(context.MarcasAutos, m => m.Id == 2 && m.Nombre == "Porsche" && m.Creador == "");
            Assert.Contains(context.MarcasAutos, m => m.Id == 3 && m.Nombre == "Mercedes Benz" && m.Creador == "Karl Benz");
            Assert.Contains(context.MarcasAutos, m => m.Id == 4 && m.Nombre == "Ferrari" && m.Creador == "Enzo Ferrari");
            Assert.Contains(context.MarcasAutos, m => m.Id == 5 && m.Nombre == "Subaru" && m.Creador == "");
        }
    }
}