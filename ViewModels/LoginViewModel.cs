using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}