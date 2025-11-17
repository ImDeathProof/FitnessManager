using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Apellido { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string ConfirmPassword { get; set; }
    }
}