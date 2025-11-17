using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña actual")]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Nueva contraseña")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        public string ConfirmNewPassword { get; set; }
    }
}