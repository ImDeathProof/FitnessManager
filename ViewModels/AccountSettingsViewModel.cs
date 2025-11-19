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
        public DateTime FechaNacimiento { get; set; }
    }
}