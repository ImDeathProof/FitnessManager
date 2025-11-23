using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.ViewModels
{
    public class PesajeViewModel
    {
        public int Id { get; set; }
        [DisplayName("Peso (kg)")]
        public double Peso { get; set; }
        [DisplayName("Fecha de pesaje")]
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}