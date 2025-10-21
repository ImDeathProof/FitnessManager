using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;
using FitnessManager.Repositories;

namespace FitnessManager.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        public ExerciseService(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }
        public async Task AddExerciseAsync(Exercise exercise)
        {
            if (exercise == null)
            {
                throw new ExerciseServiceException("El ejercicio no puede ser nulo");
            }
            if (await _exerciseRepository.ExistsAsync(exercise))
            {
                throw new ExerciseServiceException("El ejercicio ya existe");
            }
            try
            {
                await _exerciseRepository.AddExerciseAsync(exercise);
            }
            catch(DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al agregar el ejercicio", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al agregar el ejercicio", ex);
            }

        }

        public async Task DeleteExerciseAsync(int id)
        {
            if (!await _exerciseRepository.ExistsAsync(id))
            {
                throw new ExerciseServiceException("El ejercicio no existe");
            }
            try
            {
                await _exerciseRepository.DeleteExerciseAsync(id);
            }
            catch (DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al eliminar el ejercicio", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al eliminar el ejercicio", ex);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                return await _exerciseRepository.ExistsAsync(id);
            }
            catch (DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al verificar la existencia del ejercicio", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al verificar la existencia del ejercicio", ex);
            }
        }
        public async Task<bool> ExistsAsync(Exercise exercise)
        {
            if (exercise == null)
            {
                throw new ExerciseServiceException("El ejercicio no puede ser nulo");
            }
            try
            {
                return await _exerciseRepository.ExistsAsync(exercise);
            }
            catch (DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al verificar la existencia del ejercicio", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al verificar la existencia del ejercicio", ex);
            }
        }
        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
        {
            try
            {
                return await _exerciseRepository.GetAllExercisesAsync();
            }
            catch (DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al obtener todos los ejercicios", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al obtener todos los ejercicios", ex);
            }
        }

        public async Task<Exercise> GetExerciseByIdAsync(int id)
        {
            try
            {
                var exercise = await _exerciseRepository.GetExerciseByIdAsync(id);
                if (exercise == null)
                {
                    throw new ExerciseServiceException("El ejercicio no existe");
                }
                return exercise;
            }
            catch (DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al obtener el ejercicio por ID", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al obtener el ejercicio por ID", ex);
            }
        }

        public async Task<Exercise> GetExerciseByNameAsync(string name)
        {
            try
            {
                var exercise = await _exerciseRepository.GetExerciseByNameAsync(name);
                if (exercise == null)
                {
                    throw new ExerciseServiceException("El ejercicio no existe");
                }
                return exercise;
            }
            catch (DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al obtener el ejercicio por nombre", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al obtener el ejercicio por nombre", ex);
            }
        }

        public async Task<Exercise> GetExerciseByPrimaryMusclesAsync(string primaryMuscles)
        {
            try
            {
                var exercise = await _exerciseRepository.GetExerciseByPrimaryMusclesAsync(primaryMuscles);
                if (exercise == null)
                {
                    throw new ExerciseServiceException("El ejercicio no existe");
                }
                return exercise;
            }
            catch (DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al obtener el ejercicio por músculos primarios", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al obtener el ejercicio por músculos primarios", ex);
            }
        }

        public async Task<Exercise> GetExerciseBySecondaryMusclesAsync(string secondaryMuscles)
        {
            try
            {
                var exercise = await _exerciseRepository.GetExerciseBySecondaryMusclesAsync(secondaryMuscles);
                if (exercise == null)
                {
                    throw new ExerciseServiceException("El ejercicio no existe");
                }
                return exercise;
            }
            catch (DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al obtener el ejercicio por músculos secundarios", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al obtener el ejercicio por músculos secundarios", ex);
            }
        }

        public async Task<IEnumerable<Exercise>> SearchExerciseAsync(string searchTerm)
        {
            try
            {
                var exercise = await _exerciseRepository.SearchExerciseAsync(searchTerm);
                if (exercise == null)
                {
                    throw new ExerciseServiceException("El ejercicio no existe");
                }
                return exercise;
            }
            catch (DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al buscar el ejercicio", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al buscar el ejercicio", ex);
            }
        }

        public async Task UpdateExerciseAsync(Exercise exercise)
        {
            if (exercise == null)
            {
                throw new ExerciseServiceException("El ejercicio no puede ser nulo");
            }
            if (!await _exerciseRepository.ExistsAsync(exercise.Id))
            {
                throw new ExerciseServiceException("El ejercicio no existe");
            }
            try
            {
                await _exerciseRepository.UpdateExerciseAsync(exercise);
            }
            catch (DbException dbEx)
            {
                throw new ExerciseServiceException("Error de base de datos al actualizar el ejercicio", dbEx);
            }
            catch (Exception ex)
            {
                throw new ExerciseServiceException("Error al actualizar el ejercicio", ex);
            }
        }
    }
}
public class ExerciseServiceException : Exception
{
    public ExerciseServiceException(string message) : base(message) { }

    public ExerciseServiceException(string message, Exception inner) : base(message, inner) { }
}