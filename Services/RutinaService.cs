using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;
using FitnessManager.ViewModels;
using FitnessManager.Repositories;
using System.Data.Common;

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
                await _rutinaRepository.AddDetalleRutinaAsync(detalle);
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
                await _rutinaRepository.AddDetalleRutinaAsync(detalles);
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
                await _rutinaRepository.AddRutinaAsync(rutina);
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

        public async Task DeleteDetalleRutinaAsync(DetalleRutina detalle)
        {
            if(detalle == null)
                throw new ArgumentNullException("El detalle no puede ser nulo.");
            try
            {
                await _rutinaRepository.DeleteDetalleRutinaAsync(detalle);
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
                await _rutinaRepository.DeleteRutinaAsync(rutina);
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
                await _rutinaRepository.UpdateDetalleRutinaAsync(detalle);
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

        public async Task UpdateRutinaAsync(Rutina rutina)
        {
            if(rutina == null)
                throw new ArgumentNullException("La rutina no puede ser nula.");
            try
            {
                await _rutinaRepository.UpdateRutinaAsync(rutina);
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
