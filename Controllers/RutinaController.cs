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
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rutina = await _rutinaService.GetRutinaByIdAsync(id);
            if(rutina == null || rutina.UsuarioId != userId)
            {
                TempData["ErrorMessage"]="La rutina a la que intenta acceder no esta disponible.";
                return NotFound();
            }
            var detalles = await _rutinaService.GetDetallesRutinaAsync(rutina.Id);
            var viewModel = new RutinaViewModel
            {
                Rutina = rutina,
                Detalles = detalles.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Ejercicios = new SelectList(await _exerciseService.GetAllExercisesAsync(), "Id", "Name");
            try{
                var viewModel = await _rutinaService.GetRutinaViewModelAsync(id);
                return View(viewModel);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error loading Rutina for edit");
                TempData["ErrorMessage"]="Ocurrió un error al cargar la rutina. Intente nuevamente.";
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(RutinaViewModel model)
        {
            ViewBag.Ejercicios = new SelectList(await _exerciseService.GetAllExercisesAsync(), "Id", "Name");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Por favor corrija los errores en el formulario.");
                return View(model);
            }
            try
            {
                await _rutinaService.UpdateRutinaAsync(model.Rutina);
                //Si no hay detalles, eliminar todos los detalles existentes y returnar
                if(model.Detalles == null || !model.Detalles.Any())
                {
                    await _rutinaService.DeleteAllDetalleRutinaAsync(model.Rutina.Id);
                    TempData["SuccessMessage"]="Rutina actualizada correctamente.";
                    return RedirectToAction("Index");
                }
                //Sino, actualizar
                foreach(var detalle in model.Detalles)
                {
                    detalle.RutinaId = model.Rutina.Id;
                }
                await _rutinaService.UpdateDetalleRutinaAsync(model.Detalles);
                TempData["SuccessMessage"]="Rutina actualizada correctamente.";
                return RedirectToAction("Index");
            }
            catch(RutinasServiceException rse)
            {
                _logger.LogError(rse, "Error en el servicio");
                TempData["ErrorMessage"]=rse.Message;
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating Rutina");
                TempData["ErrorMessage"]="Ocurrió un error al actualizar la rutina. Intente nuevamente.";
                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            try
            {
                var rutina = await _rutinaService.GetRutinaByIdAsync(id);
                if(rutina == null)
                {
                    TempData["ErrorMessage"]="La rutina que intenta eliminar no existe.";
                    return RedirectToAction("Index");
                }
                return View(rutina);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error loading Rutina for deletion");
                TempData["ErrorMessage"]="Ocurrió un error al cargar la rutina. Intente nuevamente.";
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var rutina = await _rutinaService.GetRutinaByIdAsync(id);
                if(rutina == null)
                {
                    TempData["ErrorMessage"]="La rutina que intenta eliminar no existe.";
                    return RedirectToAction("Index");
                }
                var detalles = await _rutinaService.GetDetallesRutinaAsync(rutina.Id);
                if(detalles != null && detalles.Any())
                {
                    await _rutinaService.DeleteAllDetalleRutinaAsync(rutina.Id);
                }
                await _rutinaService.DeleteRutinaAsync(rutina);
                TempData["SuccessMessage"]="Rutina eliminada correctamente.";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error deleting Rutina");
                TempData["ErrorMessage"]="Ocurrió un error al eliminar la rutina. Intente nuevamente.";
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