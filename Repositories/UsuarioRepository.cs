using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Data;
using FitnessManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _context;
        public UsuarioRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<DateTime> GetFechaNacimientoAsync(string userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.FechaNacimiento)
                .FirstOrDefaultAsync();
        }

        public async Task<DateTime> GetFechaRegistroAsync(string userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.FechaRegistro)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> GetUserByIdAsync(string userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<Usuario> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> GetUserStatusAsync(string username)
        {
            return await _context.Users
                .Where(u => u.UserName == username)
                .Select(u => u.IsActive)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateUserAsync(Usuario user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users
                .AnyAsync(u => u.UserName == username);
        }

        public async Task<bool> UsuarioExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }
    }
}