using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FitnessManager.Models;
using FitnessManager.Services;
using FitnessManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FitnessManager.Controllers
{
    public class PesajeController : Controller
    {
        private readonly ILogger<PesajeController> _logger;
        private readonly IPesajeService _pesajeService;
        

        public PesajeController(ILogger<PesajeController> logger, IPesajeService pesajeService)
        {
            _logger = logger;
            _pesajeService = pesajeService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try{
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var model = await _pesajeService.GetUserPesajeViewModelsAsync(userId);
                return View(model);
            }catch(Exception)
            {
                TempData["ErrorMessage"] = "Ha ocurrido un error al cargar los pesajes.";
                return View(new List<Pesaje>());
            }
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PesajeViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Por favor, revise los datos ingresados");
                return View(model);
            }
            try
            {
                var pesaje = new Pesaje
                {
                    UsuarioId = userId,
                    Peso = model.Peso,
                    Fecha = model.Fecha
                };
                await _pesajeService.AddPesajeAsync(pesaje); 
                TempData["SuccessMessage"] = "Pesaje creado correctamente.";           
                return RedirectToAction("Index");
            }catch(Exception)
            {
                TempData["ErrorMessage"] = "Ha ocurrido un error al crear el pesaje.";
                return View(model);
            }
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await _pesajeService.GetPesajeViewModelAsync(id);
                return View(model);    
            }catch(Exception)
            {
                TempData["ErrorMessage"] = "Ha ocurrido un error al cargar el pesaje.";
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PesajeViewModel model)
        {
            if(!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Por favor, revise los datos ingresados";
                return View(model);
            }
            try
            {
                await _pesajeService.UpdatePesajeAsync(model);
                TempData["SuccessMessage"] = "Pesaje actualizado correctamente.";
                return RedirectToAction("Index");

            }catch(Exception)
            {
                TempData["ErrorMessage"] = "Ha ocurrido un error al actualizar el pesaje.";
                return View(model);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var pesaje =  await _pesajeService.GetPesajeByIdAsync(id);
                if(pesaje == null)
                {
                    TempData["ErrorMessage"] = "No se encontró el pesaje especificado.";
                    return RedirectToAction("Index");
                }
                await _pesajeService.DeletePesajeAsync(pesaje);
                TempData["SuccessMessage"] = "Pesaje eliminado correctamente.";
                return RedirectToAction("Index");
            }catch(Exception)
            {
                TempData["ErrorMessage"] = "Ha ocurrido un error al eliminar el pesaje.";
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