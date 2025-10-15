using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.Models
{
    public class Pesaje
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public double Peso { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}