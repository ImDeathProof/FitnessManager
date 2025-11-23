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
    public class PesajeService : IPesajeService
    {
        private readonly IPesajeRepository _pesajeRepository;
        public PesajeService(IPesajeRepository pesajeRepository)
        {
            _pesajeRepository = pesajeRepository;
        }
        public async Task AddPesajeAsync(Pesaje pesaje)
        {
            if(pesaje == null)
            {
                throw new PesajeServiceException("El pesaje no puede ser nulo.");
            }
            try
            {
                await _pesajeRepository.AddPesajeAsync(pesaje);
            }
            catch(DbException dbEx)
            {
                throw new PesajeServiceException("Error de base de datos al agregar el pesaje.", dbEx);
            }
            catch(Exception ex)
            {
                throw new PesajeServiceException("Error al agregar el pesaje.", ex);
            }
        }

        public async Task DeletePesajeAsync(Pesaje pesaje)
        {
            if(pesaje == null)
            {
                throw new PesajeServiceException("El pesaje no puede ser nulo.");
            }
            try
            {
                await _pesajeRepository.DeletePesajeAsync(pesaje);
            }
            catch(DbException dbEx)
            {
                throw new PesajeServiceException("Error de base de datos al eliminar el pesaje.", dbEx);
            }
            catch(Exception ex)
            {
                throw new PesajeServiceException("Error al eliminar el pesaje.", ex);
            }
        }

        public async Task<Pesaje> GetPesajeByIdAsync(int pesajeId)
        {
            if(pesajeId <= 0)
            {
                throw new PesajeServiceException("El ID del pesaje no es válido.");
            }
            try
            {
                return await _pesajeRepository.GetPesajeByIdAsync(pesajeId) ?? throw new PesajeServiceException("No se encontró ningún pesaje con el ID especificado.");
            }
            catch(DbException dbEx)
            {
                throw new PesajeServiceException("Error de base de datos al obtener el pesaje por ID.", dbEx);
            }
            catch(Exception ex)
            {
                throw new PesajeServiceException("Error al obtener el pesaje por ID.", ex);
            }
        }

        public async Task<Pesaje> GetUserLatestPesajeAsync(string usuarioId)
        {
            if(string.IsNullOrEmpty(usuarioId))
            {
                throw new PesajeServiceException("El ID del usuario no puede ser nulo o vacío.");
            }
            try
            {
                return await _pesajeRepository.GetUserLatestPesajeAsync(usuarioId) ?? throw new PesajeServiceException("No se encontró ningún pesaje para el usuario especificado.");
            }
            catch(DbException dbEx)
            {
                throw new PesajeServiceException("Error de base de datos al obtener el último pesaje del usuario.", dbEx);
            }
            catch(Exception ex)
            {
                throw new PesajeServiceException("Error al obtener el último pesaje del usuario.", ex);
            }
        }

        public async Task<IEnumerable<Pesaje>> GetUserPesajesAsync(string usuarioId)
        {
            if(string.IsNullOrEmpty(usuarioId))
            {
                throw new PesajeServiceException("El ID del usuario no puede ser nulo o vacío.");
            }
            try
            {
                return await _pesajeRepository.GetUserPesajesAsync(usuarioId) ?? throw new PesajeServiceException("No se encontraron pesajes para el usuario especificado.");
            }
            catch(DbException dbEx)
            {
                throw new PesajeServiceException("Error de base de datos al obtener los pesajes del usuario.", dbEx);
            }
            catch(Exception ex)
            {
                throw new PesajeServiceException("Error al obtener los pesajes del usuario.", ex);
            }
        }

        public async Task UpdatePesajeAsync(Pesaje pesaje)
        {
            if(pesaje == null)
            {
                throw new PesajeServiceException("El pesaje no puede ser nulo.");
            }
            try
            {
                await _pesajeRepository.UpdatePesajeAsync(pesaje);
            }
            catch(DbException dbEx)
            {
                throw new PesajeServiceException("Error de base de datos al actualizar el pesaje.", dbEx);
            }
            catch(Exception ex)
            {
                throw new PesajeServiceException("Error al actualizar el pesaje.", ex);
            }
        }
        public async Task<PesajeViewModel> GetPesajeViewModelAsync(int pesajeId)
        {
            try
            {
                var pesaje =  await GetPesajeByIdAsync(pesajeId);
                if(pesaje == null)
                {
                    throw new PesajeServiceException("No se encontró ningún pesaje con el ID especificado.");
                }
                return new PesajeViewModel
                {
                    Id = pesaje.Id,
                    Peso = pesaje.Peso,
                    Fecha = pesaje.Fecha
                };
            }
            catch(DbException dbEx)
            {
                throw new PesajeServiceException("Error de base de datos al actualizar el pesaje.", dbEx);
            }
            catch(Exception ex)
            {
                throw new PesajeServiceException("Error al obtener el PesajeViewModel.", ex);
            }
        }

        public async Task<PesajeViewModel> GetLatestPesajeViewModelAsync(string usuarioId)
        {
            if(string.IsNullOrEmpty(usuarioId))
            {
                throw new PesajeServiceException("El ID del usuario no puede ser nulo o vacío.");
            }
            try
            {
                var pesaje = await GetUserLatestPesajeAsync(usuarioId);
                return new PesajeViewModel
                {
                    Id = pesaje.Id,
                    Peso = pesaje.Peso,
                    Fecha = pesaje.Fecha
                };
            }
            catch(DbException dbEx)
            {
                throw new PesajeServiceException("Error de base de datos al obtener el último PesajeViewModel del usuario.", dbEx);
            }
            catch(Exception ex)
            {
                throw new PesajeServiceException("Error al obtener el último PesajeViewModel del usuario.", ex);
            }
        }

        public async Task UpdatePesajeAsync(PesajeViewModel model)
        {
            try
            {
                var pesaje = await GetPesajeByIdAsync(model.Id);
                if(pesaje == null)
                {
                    throw new PesajeServiceException("No se encontró ningún pesaje con el ID especificado.");
                }
                pesaje.Peso = model.Peso;
                pesaje.Fecha = model.Fecha;
                await _pesajeRepository.UpdatePesajeAsync(pesaje);
            }
            catch(DbException dbEx)
            {
                throw new PesajeServiceException("Error de base de datos al actualizar el pesaje.", dbEx);
            }
            catch(Exception ex)
            {
                throw new PesajeServiceException("Error al actualizar el pesaje.", ex);
            }
        }

        public async Task<IEnumerable<PesajeViewModel>> GetUserPesajeViewModelsAsync(string usuarioId)
        {
            if(string.IsNullOrEmpty(usuarioId))
            {
                throw new PesajeServiceException("El ID del usuario no puede ser nulo o vacío.");
            }
            try
            {
                var pesajes = await GetUserPesajesAsync(usuarioId);
                return pesajes.Select(p => new PesajeViewModel
                {
                    Id = p.Id,
                    Peso = p.Peso,
                    Fecha = p.Fecha
                });
            }
            catch(DbException dbEx)
            {
                throw new PesajeServiceException("Error de base de datos al obtener los PesajeViewModels del usuario.", dbEx);
            }
            catch(Exception ex)
            {
                throw new PesajeServiceException("Error al obtener los PesajeViewModels del usuario.", ex);
            }
        }
    }
}
public class PesajeServiceException : Exception
{
    public PesajeServiceException(string message) : base(message) { }

    public PesajeServiceException(string message, Exception inner) : base(message, inner) { }
}