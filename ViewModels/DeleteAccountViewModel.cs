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
        [Required (ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña")]
        public string Password { get; set; }

        [Required (ErrorMessage = "Debe confirmar la eliminación de la cuenta.")]
        [DisplayName("Confirmar eliminación de cuenta")]
        public bool ConfirmDeletion { get; set; }
        
    }   
}