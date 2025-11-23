using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Data;
using FitnessManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.Repositories
{
    public class PesajeRepository : IPesajeRepository
    {
        private readonly DataContext _context;
        public PesajeRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddPesajeAsync(Pesaje pesaje)
        {
            await _context.Pesajes.AddAsync(pesaje);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePesajeAsync(Pesaje pesaje)
        {
            _context.Pesajes.Remove(pesaje);
            await _context.SaveChangesAsync();
        }

        public async Task<Pesaje> GetPesajeByIdAsync(int pesajeId)
        {
            return await _context.Pesajes.FirstOrDefaultAsync(p => p.Id == pesajeId);
        }

        public async Task<Pesaje> GetUserLatestPesajeAsync(string usuarioId)
        {
            return await _context.Pesajes
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.Fecha)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pesaje>> GetUserPesajesAsync(string usuarioId)
        {
            return await _context.Pesajes
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();
        }

        public async Task UpdatePesajeAsync(Pesaje pesaje)
        {
            _context.Pesajes.Update(pesaje);
            await _context.SaveChangesAsync();
        }
    }
}