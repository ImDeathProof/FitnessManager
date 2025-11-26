using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.Models
{
    public class DetalleRutina
    {
        public int Id { get; set; }
        public int RutinaId { get; set; }
        public Rutina? Rutina { get; set; }
        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
        public int Series { get; set; }
        public int Repeticiones { get; set; }
    }
}