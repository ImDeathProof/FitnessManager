using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.ViewModels
{
    public class DeleteAccountViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirmar eliminación de cuenta")]
        public bool ConfirmDeletion { get; set; }
        
    }
}