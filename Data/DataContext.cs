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
    }
}