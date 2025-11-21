using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;
using FitnessManager.Repositories;
using FitnessManager.ViewModels;

namespace FitnessManager.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task ActivateUserAsync(Usuario user)
        {
            if (user == null)
            {
                throw new UsuarioServiceException("Usuario nulo");
            }
            try
            {
                user.IsActive = true;
                await _usuarioRepository.UpdateUserAsync(user);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al activar el usuario", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al activar el usuario", ex);
            }
        }

        public async Task DeleteUserAsync(Usuario user)
        {
            if (user == null)
            {
                throw new UsuarioServiceException("Usuario nulo");
            }
            try
            {
                user.IsActive = false;
                await _usuarioRepository.UpdateUserAsync(user);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al eliminar el usuario", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al eliminar el usuario", ex);
            }
        }

        public async Task<AccountSettingsViewModel> GetAccountSettingsAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UsuarioServiceException("ID Nulo o vacio");
            }
            try
            {
                var usuario = await _usuarioRepository.GetUserByIdAsync(userId);
                if (usuario == null)
                {
                    throw new UsuarioServiceException("Usuario no encontrado");
                }
                var accountSettings = new AccountSettingsViewModel
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Username = usuario.UserName,
                    Email = usuario.Email,
                    Altura = (float)(usuario.Altura ?? 0),
                    Peso = (float)(usuario.Peso ?? 0),
                    Objetivo = usuario.Objetivo,
                    FechaNacimiento = usuario.FechaNacimiento
                };
                return accountSettings;
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al obtener la configuración de la cuenta", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al obtener la configuración de la cuenta", ex);
            }
        }

        public async Task<int> GetEdadAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UsuarioServiceException("ID Nulo o vacio");
            }
            try
            {
                var fechaNacimiento =  await _usuarioRepository.GetFechaNacimientoAsync(userId);
                var today = DateTime.Today;
                var edad = today.Year - fechaNacimiento.Year;
                if (fechaNacimiento.Date > today.AddYears(-edad)) edad--;
                if(edad < 0)
                {
                    throw new UsuarioServiceException("Fecha de nacimiento invalida");
                }
                //setear la edad calculada para mantener actualizada la edad en la base de datos
                await SetEdadAsync(userId, edad);
                return edad;
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al obtener la edad", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al obtener la edad", ex);
            }
        }

        public async Task<DateTime> GetFechaNacimientoAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UsuarioServiceException("ID Nulo o vacio");
            }
            try
            {
                return await _usuarioRepository.GetFechaNacimientoAsync(userId);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al obtener la fecha de nacimiento", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al obtener la fecha de nacimiento", ex);
            }
        }

        public async Task<DateTime> GetFechaRegistroAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UsuarioServiceException("ID Nulo o vacio");
            }
            try
            {
                return await _usuarioRepository.GetFechaRegistroAsync(userId);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al obtener la fecha de registro", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al obtener la fecha de nacimiento", ex);
            }
        }

        public async Task<Usuario> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new UsuarioServiceException("Email nulo o vacio");
            }
            try
            {
                var usuario = await _usuarioRepository.GetUserByEmailAsync(email);
                if (usuario == null)
                {
                    throw new UsuarioServiceException("Usuario no encontrado");
                }
                return usuario;
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al obtener el usuario por email", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al obtener el usuario por email", ex);
            }
        }

        public async Task<Usuario> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UsuarioServiceException("ID Nulo o vacio");
            }
            try
            {
                var usuario = await _usuarioRepository.GetUserByIdAsync(userId);
                if (usuario == null)
                {
                    throw new UsuarioServiceException("Usuario no encontrado");
                }
                return usuario;
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al obtener el usuario por ID", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al obtener el usuario por ID", ex);
            }
        }

        public async Task<bool> GetUserStatusAsync(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                throw new UsuarioServiceException("Username nulo o vacio");
            }
            try
            {
                return await _usuarioRepository.GetUserStatusAsync(username);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al obtener el estado del usuario", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al obtener el estado del usuario", ex);
            }
        }

        public async Task SetEdadAsync(string userId, int edad)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UsuarioServiceException("ID Nulo o vacio");
            }
            try
            {
                var aux = await _usuarioRepository.GetUserByIdAsync(userId);
                if (aux == null)
                {
                    throw new UsuarioServiceException("Usuario no encontrado");
                }
                aux.Edad = edad;
                await _usuarioRepository.UpdateUserAsync(aux);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al actualizar la edad", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al actualizar la edad", ex);
            }
        }

        public async Task UpdatePesoAsync(string userId, float nuevoPeso)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new UsuarioServiceException("ID Nulo o vacio");
            }
            try
            {
                var aux = await _usuarioRepository.GetUserByIdAsync(userId);
                if (aux == null)
                {
                    throw new UsuarioServiceException("Usuario no encontrado");
                }
                aux.Peso = nuevoPeso;
                await _usuarioRepository.UpdateUserAsync(aux);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al actualizar el peso", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al actualizar el peso", ex);
            }
        }

        public async Task UpdateUserAsync(Usuario user)
        {
            if (user == null)
            {
                throw new UsuarioServiceException("Usuario nulo");
            }
            try
            {
                await _usuarioRepository.UpdateUserAsync(user);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al actualizar el usuario", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al actualizar el usuario", ex);
            }
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new UsuarioServiceException("Username nulo o vacio");
            }
            try
            {
                return await _usuarioRepository.UsernameExistsAsync(username);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al verificar si el username existe", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al verificar si el username existe", ex);
            }
        }

        public async Task<bool> UsernameExistsAsync(string username, string excludeUserId)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new UsuarioServiceException("Username nulo o vacio");
            }
            try
            {
                return await _usuarioRepository.UsernameExistsAsync(username, excludeUserId);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al verificar si el username existe", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al verificar si el username existe", ex);
            }
        }

        public async Task<bool> UsuarioExistsAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new UsuarioServiceException("Email nulo o vacio");
            }
            try
            {
                return await _usuarioRepository.UsuarioExistsAsync(email);
            }
            catch (DbException dbEx)
            {
                throw new UsuarioServiceException("Error de base de datos al verificar si el usuario existe", dbEx);
            }
            catch (Exception ex)
            {
                throw new UsuarioServiceException("Error al verificar si el usuario existe", ex);
            }
        }
    }
}
public class UsuarioServiceException : Exception
{
    public UsuarioServiceException(string message) : base(message)
    {
    }
    public UsuarioServiceException(string message, Exception innerException) : base(message, innerException)
    {
    }
}