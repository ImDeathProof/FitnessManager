using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.Models
{
    public class Dieta
    {
        public int Id { get; set; }
        public string? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? CaloriasDiarias { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}