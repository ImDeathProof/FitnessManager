using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Data;
using FitnessManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.Repositories
{
    public class MetaRepository : IMetaRepository
    {
        private readonly DataContext _context;
        public MetaRepository(DataContext context)
        {
            _context = context;
        }
        public void AddMeta(Meta meta)
        {
            _context.Metas.Add(meta);
        }

        public void DeleteMeta(int metaId)
        {
            var meta = _context.Metas.Find(metaId);
            if (meta != null)
            {
                _context.Metas.Remove(meta);
            }
        }

        public async Task<IEnumerable<Meta>> GetAllMetasByUserIDAsync(string UsuarioId)
        {
            return await _context.Metas
                .Where(m => m.UsuarioId == UsuarioId)
                .ToListAsync();
        }

        public async Task<Meta> GetMetaByIDAsync(int metaId)
        {
            return await _context.Metas.FindAsync(metaId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateMeta(Meta meta)
        {
            _context.Metas.Update(meta);
        }
    }
}