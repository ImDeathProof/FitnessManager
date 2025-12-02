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
        public void AddDetalleRutina(DetalleRutina detalle)
        {
            _context.DetalleRutinas.Add(detalle);
        }

        public void AddDetalleRutina(List<DetalleRutina> detalles)
        {
            _context.DetalleRutinas.AddRange(detalles);
        }

        public void AddRutina(Rutina rutina)
        {
            _context.Rutinas.Add(rutina);
        }

        public void DeleteAllDetalleRutina(int rutinaId)
        {
            var detalles = _context.DetalleRutinas.Where(d => d.RutinaId == rutinaId);
            _context.DetalleRutinas.RemoveRange(detalles);
        }

        public void DeleteDetalleRutina(DetalleRutina detalle)
        {
            _context.DetalleRutinas.Remove(detalle);
        }

        public void DeleteRutina(Rutina rutina)
        {
            _context.Rutinas.Remove(rutina);
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
                .Include(d => d.Exercise)
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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public  void UpdateDetalleRutina(DetalleRutina detalle)
        {
            _context.DetalleRutinas.Update(detalle);
        }

        public void UpdateDetalleRutina(List<DetalleRutina> detalles)
        {
            _context.DetalleRutinas.UpdateRange(detalles);
        }

        public void UpdateRutina(Rutina rutina)
        {
            _context.Rutinas.Update(rutina);
        }
    }
}