using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.Models
{
    public class Usuario : IdentityUser
    {
        public Usuario()
        {
            Id = Guid.NewGuid().ToString(); 
        }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public double? Peso { get; set; }
        public double? Altura { get; set; }
        public string? Objetivo { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public string? AvatarUrl { get; set; }
    }
}
