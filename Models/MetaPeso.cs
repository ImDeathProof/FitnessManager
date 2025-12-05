using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.Models
{
    public class MetaPeso : Meta
    {
        public decimal ValorMeta { get; set; }
        public decimal ValorInicial { get; set; }
        public decimal ValorActual { get; set; }
    }
}