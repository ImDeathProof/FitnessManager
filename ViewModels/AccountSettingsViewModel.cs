using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.ViewModels
{
    public class AccountSettingsViewModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        public string? Objetivo { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}