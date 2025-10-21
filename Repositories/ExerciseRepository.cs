using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Data;
using FitnessManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly DataContext _context;
        public ExerciseRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddExerciseAsync(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExerciseAsync(int id)
        {
            _context.Exercises.Remove(await GetExerciseByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Exercises.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> ExistsAsync(Exercise exercise)
        {

            string name = exercise.Name?.ToLower().Trim() ?? "";
            string primary = exercise.PrimaryMuscles?.ToLower().Trim() ?? "";
            string secondary = exercise.SecondaryMuscles?.ToLower().Trim() ?? "";
            string equipment = exercise.Equipment?.ToLower().Trim() ?? "";
            string mechanic = exercise.Mechanic?.ToLower().Trim() ?? "";

            return await _context.Exercises.AnyAsync(e =>
            e.Name.ToLower().Trim() == name &&
            e.PrimaryMuscles.ToLower().Trim() == primary &&
            e.SecondaryMuscles.ToLower().Trim() == secondary &&
            e.Equipment.ToLower().Trim() == equipment &&
            e.Mechanic.ToLower().Trim() == mechanic);
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
        {
            return await _context.Exercises.ToListAsync();
        }

        public async Task<Exercise> GetExerciseByIdAsync(int id)
        {
            return await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Exercise> GetExerciseByNameAsync(string name)
        {
            return await _context.Exercises.FirstOrDefaultAsync(e => e.Name == name);
        }

        public async Task<Exercise> GetExerciseByPrimaryMusclesAsync(string primaryMuscles)
        {
            return await _context.Exercises.FirstOrDefaultAsync(e => e.PrimaryMuscles == primaryMuscles);
        }

        public async Task<Exercise> GetExerciseBySecondaryMusclesAsync(string secondaryMuscles)
        {
            return await _context.Exercises.FirstOrDefaultAsync(e => e.SecondaryMuscles == secondaryMuscles);
        }

        public async Task<IEnumerable<Exercise>> SearchExerciseAsync(string searchTerm)
        {
            return await _context.Exercises
                .Where(e => (e.Name ?? "").Contains(searchTerm) ||
                            (e.PrimaryMuscles ?? "").Contains(searchTerm) ||
                            (e.SecondaryMuscles ?? "").Contains(searchTerm) ||
                            (e.Equipment ?? "").Contains(searchTerm) ||
                            (e.Mechanic ?? "").Contains(searchTerm) ||
                            (e.Instructions ?? "").Contains(searchTerm))
                .ToListAsync();
        }

        public async Task UpdateExerciseAsync(Exercise exercise)
        {
            _context.Exercises.Update(exercise);
            await _context.SaveChangesAsync();
        }
    }
}