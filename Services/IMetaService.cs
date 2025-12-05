using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;

namespace FitnessManager.Services
{
    public interface IMetaService
    {
        Task<IEnumerable<Meta>> GetAllMetasByUserIDAsync(string UsuarioId);
        Task<Meta> GetMetaByIDAsync(int metaId);
        Task AddMetaAsync(Meta meta);
        Task UpdateMetaAsync(Meta meta);
        Task DeleteMetaAsync(int metaId);
    }
}