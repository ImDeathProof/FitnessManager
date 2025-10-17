using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Data;
using FitnessManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.Repositories
{
    public class DietaRepository : IDietaRepository
    {
        private readonly DataContext _context;
        public DietaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddDietaAsync(Dieta dieta)
        {
            _context.Dietas.Add(dieta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDietaAsync(int id)
        {
            _context.Dietas.Remove(await GetDietaByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dieta>> GetAllDietasAsync()
        {
            return await _context.Dietas.ToListAsync();
        }

        public async Task<Dieta> GetDietaByIdAsync(int id)
        {
            return await _context.Dietas.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateDietaAsync(Dieta dieta)
        {
            _context.Dietas.Update(dieta);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> exists(int id)
        {
            return await _context.Dietas.AnyAsync(e => e.Id == id);
        }
    }
}