using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessManager.Models
{
    public class Meta
    {
        public int Id { get; set; }
        
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaLimite { get; set; }
        public bool Cumplida { get; set; }
        public DateTime? FechaCumplida { get; set; }

        public int TipoMetaId { get; set; }
        public TipoMeta TipoMeta { get; set; }
    }
}