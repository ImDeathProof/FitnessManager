using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.Models
{
    public class DetalleRutina
    {
        public int Id { get; set; }
        public int RutinaId { get; set; }
        public Rutina? Rutina { get; set; }
        [Required(ErrorMessage = "Seleccione un ejercicio.")]
        public int? ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
        [Required(ErrorMessage = "El campo 'Series' es obligatorio.")]
        [Range(1, 50, ErrorMessage = "Las series deben ser entre 1 y 50.")]
        public int Series { get; set; }
        [Required(ErrorMessage = "El campo 'Repeticiones' es obligatorio.")]
        [Range(1, 200, ErrorMessage = "Las repeticiones deben ser entre 1 y 200.")]
        public int Repeticiones { get; set; }
    }
}