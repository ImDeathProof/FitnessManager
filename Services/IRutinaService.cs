using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;
using FitnessManager.ViewModels;
using Microsoft.AspNetCore.Http;


namespace FitnessManager.Services
{
    public interface IRutinaService
    {
                //Rutinas
        Task<IEnumerable<Rutina>> GetRutinasByUserAsync(string usuarioId);
        Task<Rutina> GetRutinaByIdAsync(int id);
        Task<Rutina> GetRutinaByNombreAsync(string nombre, string usuarioId);
        Task UpdateRutinaAsync(Rutina rutina);
        Task DeleteRutinaAsync(Rutina rutina);
        Task AddRutinaAsync(Rutina rutina);
        //Detalles
        Task<IEnumerable<DetalleRutina>> GetDetallesRutinaAsync(int rutinaId);
        Task<DetalleRutina> GetDetalleRutinaAsync(int rutinaId, int id);
        Task UpdateDetalleRutinaAsync(DetalleRutina detalle);
        Task DeleteDetalleRutinaAsync(DetalleRutina detalle);
        Task AddDetalleRutinaAsync(DetalleRutina detalle);
        Task AddDetalleRutinaAsync(List<DetalleRutina> detalles);

        //ViewModels
        Task<RutinaViewModel> GetRutinaViewModelAsync(int rutinaId);
    }
}