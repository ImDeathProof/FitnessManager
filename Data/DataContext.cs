using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FitnessManager.Models;

namespace FitnessManager.Data
{
    public class DataContext : IdentityDbContext<Usuario>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Models.Musculo> Musculos { get; set; }
        public DbSet<Models.Ejercicio> Ejercicios { get; set; }
        public DbSet<Models.TipoEjercicio> TipoEjercicios { get; set; }
        public DbSet<Models.Rutina> Rutinas { get; set; }
        public DbSet<Models.Dieta> Dietas { get; set; }
        public DbSet<Models.Pesaje> Pesajes { get; set; }
        public DbSet<Models.Exercise> Exercises { get; set; }
        public DbSet<Models.DetalleRutina> DetalleRutinas { get; set; }
        public DbSet<Models.Meta> Metas { get; set; }
        public DbSet<Models.TipoMeta> TipoMetas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔥 Configuración de herencia TPH
            modelBuilder.Entity<Meta>()
                .HasDiscriminator<string>("MetaType")
                .HasValue<MetaPeso>("MetaPeso");

            // Opcional: especificar Decimal precision
            modelBuilder.Entity<MetaPeso>()
                .Property(m => m.ValorMeta)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<MetaPeso>()
                .Property(m => m.ValorInicial)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<MetaPeso>()
                .Property(m => m.ValorActual)
                .HasColumnType("decimal(5,2)");
        }
    }
    
}