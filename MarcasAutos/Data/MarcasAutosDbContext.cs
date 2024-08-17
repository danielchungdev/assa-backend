using MarcasAutos.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarcasAutos.Data
{
    public class MarcasAutosDbContext : DbContext
    {
        public MarcasAutosDbContext(DbContextOptions<MarcasAutosDbContext> options) : base(options)
        {

        }

        public DbSet<MarcaAuto> MarcasAutos {get; set;}

        //Data Seeding prepopula la base de datos con valores, corre en un migration script diferent.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarcaAuto>().HasData(
                new MarcaAuto() { Id = 1, Nombre = "Audi", Creador = "August Horch"},
                new MarcaAuto() { Id = 2, Nombre = "Porsche"},
                new MarcaAuto() {Id = 3, Nombre = "Mercedes Benz", Creador = "Karl Benz"},
                new MarcaAuto() {Id = 4, Nombre = "Ferrari", Creador = "Enzo Ferrari"},
                new MarcaAuto() {Id = 5, Nombre = "Subaru"}
            );
        }
    }
}