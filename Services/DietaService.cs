using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;
using FitnessManager.Repositories;

namespace FitnessManager.Services
{
    public class DietaService : IDietaService
    {
        private readonly IDietaRepository _dietaRepository;

        public DietaService(IDietaRepository dietaRepository)
        {
            _dietaRepository = dietaRepository;
        }
        public async Task AddDietaAsync(Dieta dieta)
        {
            try
            {
                if (dieta == null)
                {
                    throw new ArgumentNullException(nameof(dieta));
                }
                await _dietaRepository.AddDietaAsync(dieta);
            }
            catch (ArgumentNullException)
            {
                throw new DietaServiceException("La dieta no puede ser nula");
            }
            catch (Exception ex)
            {
                // Manejo de la excepción (puedes registrar el error, lanzar una excepción personalizada, etc.)
                throw new DietaServiceException("Error al agregar la dieta", ex);
            }
        }

        public async Task DeleteDietaAsync(int id)
        {
            try
            {
                if (!await _dietaRepository.exists(id))
                {
                    throw new KeyNotFoundException($"No se encontró una dieta con ID {id}");
                }
                await _dietaRepository.DeleteDietaAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new DietaServiceException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Manejo de la excepción (puedes registrar el error, lanzar una excepción personalizada, etc.)
                throw new DietaServiceException("Error al eliminar la dieta", ex);
            }
        }

        public async Task<bool> exists(int id)
        {
            try{
                return await _dietaRepository.exists(id);
            }
            catch(Exception ex)
            {
                throw new DietaServiceException("Error al verificar la existencia de la dieta", ex);
            }
        }

        public async Task<IEnumerable<Dieta>> GetAllDietasAsync()
        {
            try
            {
                return await _dietaRepository.GetAllDietasAsync();
            }
            catch (Exception ex)
            {
                throw new DietaServiceException("Error al obtener las dietas", ex);
            }    
            
        }

        public async Task<Dieta> GetDietaByIdAsync(int id)
        {
            try
            {
                if (!await _dietaRepository.exists(id))
                {
                    throw new KeyNotFoundException($"No se encontró una dieta con ID {id}");
                }
                return await _dietaRepository.GetDietaByIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new DietaServiceException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new DietaServiceException("Error al obtener la dieta", ex);
            }
        }

        public async Task UpdateDietaAsync(Dieta dieta)
        {
            try
            {
                if (dieta == null)
                {
                    throw new ArgumentNullException(nameof(dieta));
                }
                if (!await _dietaRepository.exists(dieta.Id))
                {
                    throw new KeyNotFoundException($"No se encontró una dieta con ID {dieta.Id}");
                }
                await _dietaRepository.UpdateDietaAsync(dieta);
            }
            catch (ArgumentNullException)
            {
                throw new DietaServiceException("La dieta no puede ser nula");
            }
            catch (KeyNotFoundException ex)
            {
                throw new DietaServiceException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new DietaServiceException("Error al actualizar la dieta", ex);
            }
        }
    }
}
public class DietaServiceException : Exception
{
    public DietaServiceException(string message) : base(message) { }

    public DietaServiceException(string message, Exception innerException) : base(message, innerException) { }
}