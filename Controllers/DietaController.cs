using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FitnessManager.Models;
using FitnessManager.Services;
using System.Security.Claims;

namespace FitnessManager.Controllers
{
    public class DietaController : Controller
    {
        private readonly ILogger<DietaController> _logger;
        private readonly IDietaService _dietaService;
        public DietaController(ILogger<DietaController> logger, IDietaService dietaService)
        {
            _logger = logger;
            _dietaService = dietaService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Dietas";
            return View();
        }
        public IActionResult Create()
        {
            ViewData["Title"] = "Nueva dieta";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Dieta dieta)
        {
            try
            {
                ViewData["Title"] = "Nueva dieta";
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                dieta.UsuarioId = userId;
                if (ModelState.IsValid)
                {
                    Console.WriteLine("ModelState is valid");
                    // Lógica para guardar la nueva dieta
                    if (dieta == null)
                    {
                        TempData["ErrorMessage"] = "Debe completar todos los datos.E:0001";
                        return View(dieta);
                    }
                    await _dietaService.AddDietaAsync(dieta);
                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = "Debe completar todos los datos.E:0002";
                return View(dieta);
            }
            catch (DietaServiceException ex)
            {
                TempData["ErrorMessage"] = ex.Message + " E:0004";
                return View(dieta);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error en la creación. E:0003";
                return View(dieta);
            }
        }
        public async Task<IActionResult> Read()
        {
            try
            {
                ViewData["Title"] = "Mis dietas";
                var dietas = await _dietaService.GetAllDietasAsync();

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                dietas = dietas.Where(d => d.UsuarioId == userId).ToList();
                
                return View(dietas);
            }
            catch (DietaServiceException ex)
            {
                TempData["ErrorMessage"] = ex.Message + " E:0008";
                return RedirectToAction(nameof(Read));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error en la carga. E:0009";
                return RedirectToAction(nameof(Read));
            }
        }

        //GET : Dieta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID Null. E:0005";
                    return RedirectToAction(nameof(Read));
                }
                var dieta = await _dietaService.GetDietaByIdAsync(id.Value);
                if (dieta == null)
                {
                    TempData["ErrorMessage"] = "Dieta no encontrada. E:0006";
                    return RedirectToAction(nameof(Read));
                }
                ViewData["Title"] = dieta.Nombre;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Console.WriteLine(dieta.UsuarioId + " " + userId + " " + id.Value + " " + dieta.Nombre);
                return View(dieta);
            }
            catch (DietaServiceException ex)
            {
                TempData["ErrorMessage"] = ex.Message + " E:0008";
                return RedirectToAction(nameof(Read));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error en la carga. E:0009";
                return RedirectToAction(nameof(Read));
            }
        }
        public async Task<IActionResult> DeleteConfirmation(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID Null. E:0005";
                    return RedirectToAction(nameof(Read));
                }
                var dieta = await _dietaService.GetDietaByIdAsync(id.Value);
                if (dieta == null)
                {
                    TempData["ErrorMessage"] = "Dieta no encontrada. E:0006";
                    return RedirectToAction(nameof(Read));
                }
                return View(dieta);
            }
            catch (DietaServiceException ex)
            {
                TempData["ErrorMessage"] = ex.Message + " E:0008";
                return RedirectToAction(nameof(Read));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error en la carga. E:0009";
                return RedirectToAction(nameof(Read));
            }
        }
        //POST : Dieta/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID Null. E:0005";
                    return RedirectToAction(nameof(Read));
                }
                await _dietaService.DeleteDietaAsync(id.Value);
                TempData["SuccessMessage"] = "Dieta eliminada correctamente.";
                return RedirectToAction(nameof(Read));
            }
            catch (DietaServiceException ex)
            {
                TempData["ErrorMessage"] = ex.Message + " E:0008";
                return RedirectToAction(nameof(Read));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error en la eliminación E:0007";
                return RedirectToAction(nameof(Read));
            }
        }
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID Null. E:0005";
                    return RedirectToAction(nameof(Read));
                }
                var dieta = await _dietaService.GetDietaByIdAsync(id.Value);
                if (dieta == null)
                {
                    TempData["ErrorMessage"] = "Dieta no encontrada. E:0006";
                    return RedirectToAction(nameof(Read));
                }
                return View(dieta);
            }
            catch (DietaServiceException ex)
            {
                TempData["ErrorMessage"] = ex.Message + " E:0008";
                return RedirectToAction(nameof(Read));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error en la carga. E:0009";
                return RedirectToAction(nameof(Read));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Dieta dieta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Datos inválidos. E:0011";
                    return View(dieta);
                }
                var dietaExistente = await _dietaService.GetDietaByIdAsync(dieta.Id);
                if (dietaExistente == null)
                {
                    TempData["ErrorMessage"] = "Dieta no encontrada. E:0006";
                    return View(dieta);
                }
                dietaExistente.Nombre = dieta.Nombre;
                dietaExistente.CaloriasDiarias = dieta.CaloriasDiarias;
                dietaExistente.Descripcion = dieta.Descripcion;

                await _dietaService.UpdateDietaAsync(dietaExistente);
                TempData["SuccessMessage"] = "Dieta actualizada correctamente.";
                return View(dietaExistente);
            }
            catch (DietaServiceException ex)
            {
                TempData["ErrorMessage"] = ex.Message + " E:0008";
                return View(dieta);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error en la edición. E:0012";
                return View(dieta);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}