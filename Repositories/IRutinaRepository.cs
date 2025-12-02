using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;

namespace FitnessManager.Repositories
{
    public interface IRutinaRepository
    {
        //Rutinas
        Task<IEnumerable<Rutina>> GetRutinasByUserAsync(string usuarioId);
        Task<Rutina> GetRutinaByIdAsync(int id);
        Task<Rutina> GetRutinaByNombreAsync(string nombre, string usuarioId);
        void UpdateRutina(Rutina rutina);
        void DeleteRutina(Rutina rutina);
        void AddRutina(Rutina rutina);
        Task<bool> RutinaExistsAsync(int id);


        //Detalles
        Task<IEnumerable<DetalleRutina>> GetDetallesRutinaAsync(int rutinaId);
        Task<DetalleRutina> GetDetalleRutinaAsync(int rutinaId, int id);
        void UpdateDetalleRutina(DetalleRutina detalle);
        void UpdateDetalleRutina(List<DetalleRutina> detalles);
        void DeleteDetalleRutina(DetalleRutina detalle);
        void DeleteAllDetalleRutina(int rutinaId);
        void AddDetalleRutina(DetalleRutina detalle);
        void AddDetalleRutina(List<DetalleRutina> detalles);
        Task<bool> DetalleRutinaExistsAsync(int rutinaId, int id);

        Task SaveChangesAsync();
    }
}