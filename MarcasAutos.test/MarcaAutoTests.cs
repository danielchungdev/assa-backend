using MarcasAutos.Entities;
using System;
using Xunit;

namespace MarcasAutos.Tests
{
    public class MarcaAutoTests
    {
        [Fact]
        public void MarcaAuto_IdProperty_ShouldSetAndGetCorrectly()
        {
            var marcaAuto = new MarcaAuto { Nombre = "Test" };

            marcaAuto.Id = 1;

            Assert.Equal(1, marcaAuto.Id);
        }

        [Fact]
        public void MarcaAuto_NombreProperty_ShouldSetAndGetCorrectly()
        {
            var marcaAuto = new MarcaAuto { Nombre = "Toyota" };

            Assert.Equal("Toyota", marcaAuto.Nombre);
        }

        [Fact]
        public void MarcaAuto_CreadorProperty_ShouldHaveDefaultValue()
        {
            var marcaAuto = new MarcaAuto { Nombre = "Test" };

            Assert.Equal(string.Empty, marcaAuto.Creador);
        }

        [Fact]
        public void MarcaAuto_CreadorProperty_ShouldSetAndGetCorrectly()
        {
            var marcaAuto = new MarcaAuto { Nombre = "Test" };

            marcaAuto.Creador = "John Doe";

            Assert.Equal("John Doe", marcaAuto.Creador);
        }
    }
}