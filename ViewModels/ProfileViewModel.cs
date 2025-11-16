using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.ViewModels
{
    public class ProfileViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string PhoneNumber { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public string Objetivo { get; set; }
    }
}