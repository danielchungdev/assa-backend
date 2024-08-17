using MarcasAutos.Controllers;
using MarcasAutos.Data;
using MarcasAutos.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcasAutos.test
{
    public class MarcasAutosControllerTest
    {
        //Para crear base de datos en memoria diferentes.
        private MarcasAutosDbContext CreateContext(string dbName)
        {
            return new MarcasAutosDbContext(new DbContextOptionsBuilder<MarcasAutosDbContext>()
                .UseInMemoryDatabase(databaseName: dbName).Options);
        }

        //Para determinar que la lista esta vacia si no hay nada en la base de datos
        [Fact]
        public async Task GetTodasMarcas_regresa_lista_vacia_cuando_base_de_datos_vacia()
        {
            var dbContext = CreateContext("Vacia-test");
            var controller = new MarcasAutosController(dbContext);

            var actionResult = await controller.GetTodasMarcas();

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var marcas = Assert.IsType<List<MarcaAuto>>(okResult.Value);
            Assert.Empty(marcas);
        }

        //Para determinar si la cantidad correcta de marcas es regresada.
        [Fact]
        public async Task GetTodasMarcas_regresa_cantidad_correcta()
        {
            // Arrange
            var dbContext = CreateContext("tres-totales-test");

            dbContext.MarcasAutos.AddRange(
                new MarcaAuto { Id = 1, Nombre = "Toyota", Creador = "Kiichiro Toyoda" },
                new MarcaAuto { Id = 2, Nombre = "Honda", Creador = "Soichiro Honda" },
                new MarcaAuto { Id = 3, Nombre = "Ford", Creador = "Henry Ford" }
            );
            await dbContext.SaveChangesAsync();

            var controller = new MarcasAutosController(dbContext);

            var actionResult = await controller.GetTodasMarcas();

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var marcas = Assert.IsType<List<MarcaAuto>>(okResult.Value);
            Assert.Equal(3, marcas.Count);
        }

        //Asegurarnos que tire una exepcion cuando hay un error.
        [Fact]
        public async Task GetTodasMarcas_intentar_accion_en_objeto_destruido()
        {
            var dbContext = CreateContext("TestDb3");
            var controller = new MarcasAutosController(dbContext);

            dbContext.Dispose();

            await Assert.ThrowsAsync<ObjectDisposedException>(() => controller.GetTodasMarcas());
        }

        [Fact]
        public void Constructor_es_valido()
        {
            var dbContext = CreateContext("ValidDbContext");

            var controller = new MarcasAutosController(dbContext);

            Assert.NotNull(controller);
        }
    }
}
