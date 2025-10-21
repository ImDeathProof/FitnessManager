using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Models;

namespace FitnessManager.Services
{
    public interface IExerciseService
    {
        Task<IEnumerable<Exercise>> GetAllExercisesAsync();
        Task<Exercise> GetExerciseByIdAsync(int id);
        Task<Exercise> GetExerciseByNameAsync(string name);
        Task<Exercise> GetExerciseByPrimaryMusclesAsync(string primaryMuscles);
        Task<Exercise> GetExerciseBySecondaryMusclesAsync(string secondaryMuscles);
        Task<IEnumerable<Exercise>> SearchExerciseAsync(string searchTerm);
        Task AddExerciseAsync(Exercise exercise); 
        Task UpdateExerciseAsync(Exercise exercise);
        Task DeleteExerciseAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Exercise exercise);
    }
}