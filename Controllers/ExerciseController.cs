using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FitnessManager.Models;
using FitnessManager.Services;

namespace FitnessManager.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ILogger<ExerciseController> _logger;
        private readonly IExerciseService _exerciseService;

        public ExerciseController(ILogger<ExerciseController> logger, IExerciseService exerciseService)
        {
            _logger = logger;
            _exerciseService = exerciseService;
        }
        // GET: Exercise
        public async Task<IActionResult> Index(string search)
        {
            ViewData["CurrentSearch"] = search;
            var exercises = await _exerciseService.GetAllExercisesAsync();
            if (!string.IsNullOrEmpty(search))
            {
                exercises = await _exerciseService.SearchExerciseAsync(search);
            }
            return View(exercises);
        }
        // GET: Exercise/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID null.";
                return RedirectToAction("Index");
            }
            try
            {
                var exercise = await _exerciseService.GetExerciseByIdAsync(id.Value);
                if (exercise == null)
                {
                    TempData["ErrorMessage"] = "Ejercicio no encontrado.";
                    return RedirectToAction("Index");
                }
                return View(exercise);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving exercise details for ID {Id}", id);
                TempData["ErrorMessage"] = "Ocurrió un error al obtener los detalles del ejercicio.";
                return RedirectToAction("Index");
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}