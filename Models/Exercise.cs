using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Force { get; set; }
        public string? Level { get; set; }
        public string? Mechanic { get; set; }
        public string? Equipment { get; set; }
        public string? PrimaryMuscles { get; set; }
        public string? SecondaryMuscles { get; set; }
        public string? Instructions { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
    }
}