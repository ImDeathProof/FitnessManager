using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;
using FitnessManager.ViewModels;

namespace FitnessManager.Services
{
    public interface IPesajeService
    {
        Task<IEnumerable<Pesaje>> GetUserPesajesAsync(string usuarioId);
        Task<IEnumerable<PesajeViewModel>> GetUserPesajeViewModelsAsync(string usuarioId);
        Task AddPesajeAsync(Pesaje pesaje);
        Task<Pesaje> GetUserLatestPesajeAsync(string usuarioId);
        Task<PesajeViewModel> GetLatestPesajeViewModelAsync(string usuarioId);
        Task DeletePesajeAsync(Pesaje pesaje);
        Task UpdatePesajeAsync(Pesaje pesaje);
        Task UpdatePesajeAsync(PesajeViewModel model);
        Task<Pesaje> GetPesajeByIdAsync(int pesajeId);
        Task<PesajeViewModel> GetPesajeViewModelAsync(int pesajeId);
    }
}