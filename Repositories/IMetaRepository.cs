using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;

namespace FitnessManager.Repositories
{
    public interface IMetaRepository
    {
        Task<IEnumerable<Meta>> GetAllMetasByUserIDAsync(string UsuarioId);
        Task<Meta> GetMetaByIDAsync(int metaId);
        void AddMeta(Meta meta);
        void UpdateMeta(Meta meta);
        void DeleteMeta(int metaId);
        Task SaveChangesAsync();
    }
}