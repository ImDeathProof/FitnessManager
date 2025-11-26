using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FitnessManager.Models;
using FitnessManager.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using FitnessManager.ViewModels;
using System.Security.Claims;

namespace FitnessManager.Controllers
{
    public class RutinaController : Controller
    {
        private readonly ILogger<RutinaController> _logger;
        private readonly IRutinaService _rutinaService;
        private readonly IExerciseService _exerciseService;
        public RutinaController(ILogger<RutinaController> logger, IRutinaService rutinaService, IExerciseService exerciseService)
        {
            _logger = logger;
            _rutinaService = rutinaService;
            _exerciseService = exerciseService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = await _rutinaService.GetRutinasByUserAsync(userId);
            return View(viewModel);
        }
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var viewModel = new ViewModels.RutinaViewModel()
            {
                Rutina = new Rutina(),
                Detalles = new List<DetalleRutina>{ new DetalleRutina() }
            };
            ViewData["Ejercicios"] = new SelectList(await _exerciseService.GetAllExercisesAsync(), "Id", "Name");
            return View(viewModel);
        }
        [Authorize] 
        [HttpPost]
        public async Task<IActionResult> Create(RutinaViewModel model)
        {
            ViewData["Ejercicios"] = new SelectList(await _exerciseService.GetAllExercisesAsync(), "Id", "Name");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.Rutina.UsuarioId = userId;
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Por favor corrija los errores en el formulario.");
                return View(model);
            }
            try
            {
                await _rutinaService.AddRutinaAsync(model.Rutina);
                var detallesAntes = model.Detalles;
                foreach(var d in model.Detalles)
                {
                    Console.WriteLine($"ExerciseId = {d.ExerciseId}");
                    d.RutinaId = model.Rutina.Id;
                }
                await _rutinaService.AddDetalleRutinaAsync(model.Detalles);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating Rutina");
                TempData["ErrorMessage"]="Ocurrió un error al crear la rutina. Intente nuevamente.";
                return View(model);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}