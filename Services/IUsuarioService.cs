using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;

namespace FitnessManager.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUserByEmailAsync(string email);
        Task<Usuario> GetUserByIdAsync(string userId);
        Task<bool> UsuarioExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
        Task<DateTime> GetFechaRegistroAsync(string userId);
        Task<DateTime> GetFechaNacimientoAsync(string userId);
        Task UpdateUserAsync(Usuario user);
        Task UpdatePesoAsync(string userId, float nuevoPeso);
        Task<int> GetEdadAsync(string userId);
        Task SetEdadAsync(string userId, int edad);
        Task DeleteUserAsync(Usuario user);
        Task ActivateUserAsync(Usuario user);
        Task<bool> GetUserStatusAsync(string username);
    }
}