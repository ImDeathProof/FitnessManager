using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.Models
{
    public class Ejercicio
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }
        public string? VideoUrl { get; set; }
        public int MusculoId { get; set; }
        public Musculo? Musculo { get; set; }
        
    }
}