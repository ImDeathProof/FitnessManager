using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Repositories;
using FitnessManager.Models;
using FitnessManager.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.Repositories
{
    public class RutinaRepository : IRutinaRepository
    {
        private readonly DataContext _context;
        public RutinaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddDetalleRutinaAsync(DetalleRutina detalle)
        {
            _context.DetalleRutinas.Add(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task AddDetalleRutinaAsync(List<DetalleRutina> detalles)
        {
            _context.DetalleRutinas.AddRange(detalles);
            await _context.SaveChangesAsync();
        }

        public async Task AddRutinaAsync(Rutina rutina)
        {
            _context.Rutinas.Add(rutina);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDetalleRutinaAsync(DetalleRutina detalle)
        {
            _context.DetalleRutinas.Remove(detalle);
        }

        public async Task DeleteRutinaAsync(Rutina rutina)
        {
            _context.Rutinas.Remove(rutina);
            await _context.SaveChangesAsync();
        }

        public  async Task<bool> DetalleRutinaExistsAsync(int rutinaId, int id)
        {
            return await _context.DetalleRutinas.AnyAsync(d => d.RutinaId == rutinaId && d.Id == id);
        }

        public async Task<DetalleRutina> GetDetalleRutinaAsync(int rutinaId, int id)
        {
            return await _context.DetalleRutinas.FindAsync(rutinaId, id);
        }

        public async Task<IEnumerable<DetalleRutina>> GetDetallesRutinaAsync(int rutinaId)
        {
            return await _context.DetalleRutinas
                .Where(d => d.RutinaId == rutinaId)
                .ToListAsync();
        }

        public async Task<Rutina> GetRutinaByIdAsync(int id)
        {
            return await _context.Rutinas.FindAsync(id);
        }

        public async Task<Rutina> GetRutinaByNombreAsync(string nombre, string usuarioId)
        {
            return await _context.Rutinas
                .FirstOrDefaultAsync(r => r.Nombre == nombre && r.UsuarioId == usuarioId);
        }

        public async Task<IEnumerable<Rutina>> GetRutinasByUserAsync(string usuarioId)
        {
            return await _context.Rutinas
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<bool> RutinaExistsAsync(int id)
        {
            return await _context.Rutinas.AnyAsync(r => r.Id == id);
        }

        public async Task UpdateDetalleRutinaAsync(DetalleRutina detalle)
        {
            _context.DetalleRutinas.Update(detalle);
        }

        public async Task UpdateRutinaAsync(Rutina rutina)
        {
            _context.Rutinas.Update(rutina);
            await _context.SaveChangesAsync();
        }
    }
}