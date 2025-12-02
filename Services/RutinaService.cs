using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;
using FitnessManager.ViewModels;
using FitnessManager.Repositories;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.Services
{
    public class RutinaService : IRutinaService
    {
        private readonly IRutinaRepository _rutinaRepository;
        public RutinaService(IRutinaRepository rutinaRepository)
        {
            _rutinaRepository = rutinaRepository;
        }
        public async Task AddDetalleRutinaAsync(DetalleRutina detalle)
        {
            if(detalle == null)
                throw new ArgumentNullException("El detalle no puede ser nulo.");
            try
            {
                _rutinaRepository.AddDetalleRutina(detalle);
                await _rutinaRepository.SaveChangesAsync();
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al agregar DetalleRutina", ex);
            }
        }

        public async Task AddDetalleRutinaAsync(List<DetalleRutina> detalles)
        {
            if(detalles == null || detalles.Count == 0)
                throw new ArgumentNullException("La lista de detalles no puede ser nula o vacía.");
            try
            {
                _rutinaRepository.AddDetalleRutina(detalles);
                await _rutinaRepository.SaveChangesAsync();
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al agregar DetallesRutina", ex);
            }
        }

        public async Task AddRutinaAsync(Rutina rutina)
        {
            if(rutina == null)
                throw new ArgumentNullException("La rutina no puede ser nula.");
            try
            {
                _rutinaRepository.AddRutina(rutina);
                await _rutinaRepository.SaveChangesAsync();
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al agregar Rutina", ex);
            }
        }

        public async Task DeleteAllDetalleRutinaAsync(int rutinaId)
        {
            if(rutinaId <= 0)
                throw new ArgumentException("El ID de la rutina debe ser mayor a cero.");
            try
            {
                _rutinaRepository.DeleteAllDetalleRutina(rutinaId);
                await _rutinaRepository.SaveChangesAsync();
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al eliminar DetallesRutina", ex);
            }
        }

        public async Task DeleteDetalleRutinaAsync(DetalleRutina detalle)
        {
            if(detalle == null)
                throw new ArgumentNullException("El detalle no puede ser nulo.");
            try
            {
                _rutinaRepository.DeleteDetalleRutina(detalle);
                await _rutinaRepository.SaveChangesAsync();
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al eliminar DetalleRutina", ex);
            }
        }

        public async Task DeleteRutinaAsync(Rutina rutina)
        {
            if(rutina == null)
                throw new ArgumentNullException("La rutina no puede ser nula.");
            try
            {
                _rutinaRepository.DeleteRutina(rutina);
                await _rutinaRepository.SaveChangesAsync();
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al eliminar Rutina", ex);
            }
        }

        public async Task<DetalleRutina> GetDetalleRutinaAsync(int rutinaId, int id)
        {
            if(rutinaId <= 0 || id <= 0)
                throw new ArgumentException("Los IDs deben ser mayores a cero.");
            try
            {
                var detalle = await _rutinaRepository.GetDetalleRutinaAsync(rutinaId, id);
                if(detalle == null)
                    throw new RutinasServiceException("El Detalle no existe.");
                
                return detalle;
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al obtener DetalleRutina", ex);
            }
        }

        public async Task<IEnumerable<DetalleRutina>> GetDetallesRutinaAsync(int rutinaId)
        {
            if(rutinaId <= 0)
                throw new ArgumentException("El ID de la rutina debe ser mayor a cero.");
            try
            {
                var detalles = await _rutinaRepository.GetDetallesRutinaAsync(rutinaId);
                if(detalles == null)
                    throw new RutinasServiceException("No se encontraron detalles para la rutina.");
                
                return detalles;
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al obtener DetallesRutina", ex);
            }
        }

        public async Task<Rutina> GetRutinaByIdAsync(int id)
        {
            if(id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");
            try
            {
                var rutina = await _rutinaRepository.GetRutinaByIdAsync(id);
                if(rutina == null)
                    throw new RutinasServiceException("La Rutina no existe.");
                
                return rutina;
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al obtener Rutina", ex);
            }
        }

        public async Task<Rutina> GetRutinaByNombreAsync(string nombre, string usuarioId)
        {
            if(string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(usuarioId))
                throw new ArgumentException("El nombre y el ID de usuario no pueden ser nulos o vacíos.");
            try
            {
                var rutina = await _rutinaRepository.GetRutinaByNombreAsync(nombre, usuarioId);
                if(rutina == null)
                    throw new RutinasServiceException("La Rutina no existe.");
                
                return rutina;
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al obtener Rutina por nombre", ex);
            }
        }

        public async Task<IEnumerable<Rutina>> GetRutinasByUserAsync(string usuarioId)
        {
            if(string.IsNullOrWhiteSpace(usuarioId))
                throw new ArgumentException("El ID de usuario no puede ser nulo o vacío.");
            try
            {
                var rutinas = await _rutinaRepository.GetRutinasByUserAsync(usuarioId);
                return rutinas;
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al obtener Rutinas por usuario", ex);
            }
        }

        public async Task<RutinaViewModel> GetRutinaViewModelAsync(int rutinaId)
        {
            if(rutinaId <= 0)
                throw new ArgumentException("El ID de la rutina debe ser mayor a cero.");
            try
            {
                var rutina = await GetRutinaByIdAsync(rutinaId);
                var detalles = await GetDetallesRutinaAsync(rutinaId);

                var viewModel = new RutinaViewModel
                {
                    Rutina = rutina,
                    Detalles = detalles.ToList()
                };

                return viewModel;
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al obtener RutinaViewModel", ex);
            }
        }

        public async Task UpdateDetalleRutinaAsync(DetalleRutina detalle)
        {
            if(detalle == null)
                throw new ArgumentNullException("El detalle no puede ser nulo.");
            try
            {
                //1. Obtener el Detalle existente
                var existente = await _rutinaRepository.GetDetalleRutinaAsync(detalle.RutinaId, detalle.Id);
                if(existente == null)
                    throw new RutinasServiceException("El Detalle no existe.");
                //2. Actualizar los campos especificados
                existente.ExerciseId = detalle.ExerciseId;
                existente.Series = detalle.Series;
                existente.Repeticiones = detalle.Repeticiones;
                _rutinaRepository.UpdateDetalleRutina(existente);
                //3. Guardar los cambios
                await _rutinaRepository.SaveChangesAsync();
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al actualizar DetalleRutina", ex);
            }
        }

        public async Task UpdateDetalleRutinaAsync(List<DetalleRutina> detalles)
        {
            if(detalles == null || detalles.Count == 0)
                throw new ArgumentNullException("La lista de detalles no puede ser nula o vacía.");
            try
            {
                int rutinaId = detalles.First().RutinaId;
                var detallesExistentes = await _rutinaRepository.GetDetallesRutinaAsync(rutinaId)
                                        ?? new List<DetalleRutina>();
                // 1. Actualizar o eliminar existentes
                foreach (var existente in detallesExistentes)
                {
                    var entrante = detalles.FirstOrDefault(d => d.Id == existente.Id);
                    if (entrante != null)
                    {
                        // actualizar
                        existente.ExerciseId = entrante.ExerciseId;
                        existente.Series = entrante.Series;
                        existente.Repeticiones = entrante.Repeticiones;
                        _rutinaRepository.UpdateDetalleRutina(existente);
                    }
                    else
                    {
                        // eliminar
                        _rutinaRepository.DeleteDetalleRutina(existente);
                    }
                }
                // 2. Agregar nuevos (los que no tienen Id o no están en la lista existente)
                foreach (var entrante in detalles)
                {
                    var yaExiste = detallesExistentes.Any(e => e.Id == entrante.Id);
                    if (!yaExiste)
                    {
                        _rutinaRepository.AddDetalleRutina(entrante);
                    } 
                }
                await _rutinaRepository.SaveChangesAsync();
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(DbUpdateException dbuex)
            {
                throw new RutinasServiceException("Error de actualización en la base de datos.", dbuex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al actualizar", ex);
            }
        }

        public async Task UpdateRutinaAsync(Rutina rutina)
        {
            if(rutina == null)
                throw new ArgumentNullException("La rutina no puede ser nula.");
            try
            {
                var rutinaExistente = await _rutinaRepository.GetRutinaByIdAsync(rutina.Id);
                if(rutinaExistente == null)
                    throw new RutinasServiceException("La Rutina no existe.");
                rutinaExistente.Nombre = rutina.Nombre;
                rutinaExistente.Descripcion = rutina.Descripcion;
                _rutinaRepository.UpdateRutina(rutinaExistente);
                await _rutinaRepository.SaveChangesAsync();
            }
            catch(DbException dbex)
            {
                throw new RutinasServiceException("Ocurrio un error en la base de datos.", dbex);
            }
            catch(Exception ex)
            {
                throw new RutinasServiceException("Error al actualizar Rutina", ex);
            }
        }
    }
}
public class RutinasServiceException : Exception
{
    public RutinasServiceException() { }

    public RutinasServiceException(string message) : base(message) { }

    public RutinasServiceException(string message, Exception inner) : base(message, inner) { }
}

