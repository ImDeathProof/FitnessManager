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
            ViewData["Title"] = "Crear nueva dieta";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Dieta dieta)
        {
            try
            {
                ViewData["Title"] = "Crear nueva dieta";
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
                TempData["ErrorMessage"] = "Error al crear la dieta. E:0003";
                return View(dieta);
            }
        }
        public async Task<IActionResult> Read()
        {
            ViewData["Title"] = "Mis dietas";
            var dietas = await _dietaService.GetAllDietasAsync();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dietas = dietas.Where(d => d.UsuarioId == userId).ToList();
            return View(dietas);
        }

        //GET : Dieta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Console.WriteLine("Details method called with id: " + id);
            if (id == null)
            {
                return NotFound();
            }
            var dieta = await _dietaService.GetDietaByIdAsync(id.Value);
            if (dieta == null)
            {
                return NotFound();
            }
            ViewData["Title"] = dieta.Nombre;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(dieta.UsuarioId + " " + userId + " " + id.Value + " " + dieta.Nombre);
            return View(dieta);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}