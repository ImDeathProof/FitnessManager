using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;

namespace FitnessManager.ViewModels
{
    public class RutinaViewModel
    {
        public Rutina Rutina { get; set; }
        public List<DetalleRutina>? Detalles { get; set; }
    }
}