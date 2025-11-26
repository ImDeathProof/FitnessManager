using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FitnessManager.Models
{
    public class Rutina
    {
        public int Id { get; set; }
        [BindNever]
        public string? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        public ICollection<DetalleRutina>? DetalleRutina { get; set; }
    }
}