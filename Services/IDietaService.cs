using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;

namespace FitnessManager.Services
{
    public interface IDietaService
    {
        Task<IEnumerable<Dieta>> GetAllDietasAsync(); 
        Task<Dieta> GetDietaByIdAsync(int id); 
        Task AddDietaAsync(Dieta dieta); 
        Task UpdateDietaAsync(Dieta dieta);
        Task DeleteDietaAsync(int id);
        Task<bool> exists(int id);
    }
}