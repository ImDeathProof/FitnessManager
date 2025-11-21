using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;

namespace FitnessManager.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUserByEmailAsync(string email);
        Task<Usuario> GetUserByIdAsync(string userId);
        Task<Usuario> GetUserByUsernameAsync(string username);
        Task<bool> GetUserStatusAsync(string username);
        Task<bool> UsuarioExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> UsernameExistsAsync(string username, string excludeUserId);
        Task<DateTime> GetFechaRegistroAsync(string userId);
        Task<DateTime> GetFechaNacimientoAsync(string userId);
        Task UpdateUserAsync(Usuario user);
    }
}