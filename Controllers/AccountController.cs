using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using FitnessManager.Models;
using FitnessManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using FitnessManager.Controllers;
using FitnessManager.Repositories;
using FitnessManager.Services;

public class AccountController : Controller
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly IUsuarioService _usuarioService;

    public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() => View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

        if (result.Succeeded) return RedirectToAction("Index", "Home");

        ModelState.AddModelError("", "Correo o contraseña incorrectos");
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register() => View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = new Usuario
        {
            UserName = model.Username,
            Email = model.Email,
            Nombre = model.Nombre,
            Apellido = model.Apellido,
            IsActive = true,
            FechaRegistro = DateTime.UtcNow,
            FechaNacimiento = DateTime.UtcNow
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
        return View(model);
    }
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        ViewData["Title"] = "Perfil";
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Login", "Account");
        var model = new ProfileViewModel
        {
            Username = user.UserName,
            Email = user.Email,
            Nombre = user.Nombre,
            Apellido = user.Apellido,
            PhoneNumber = user.PhoneNumber,
            Edad = user.Edad ?? 0,
            Peso = (float)(user.Peso ?? 0),
            Altura = (float)(user.Altura ?? 0),
            Objetivo = user.Objetivo
        };
        return View(model);
    }
    [Authorize]
    public async Task<IActionResult>  Settings(string view)
    {
        ViewData["Title"] = "Configurariones";
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Login", "Account");
        ViewData["UserName"] = user.UserName;
        ViewData["Avatar"] = user.AvatarUrl;
        if (string.IsNullOrEmpty(view))
        {
            view = "profile";
        }
        return View(model : view);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Settings", model: "password-change");
        }
        try
        {
            
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (result.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("Settings", new { view = "password-change" });
        }
        return View("Settings", model: "password-change");
        }
        catch (Exception)
        {
            ViewData["ErrorMessage"] = "Ocurrió un error al cambiar la contraseña. Por favor, inténtelo de nuevo.";
            return View("Settings", model: "password-change");
        }
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DeleteAccount(DeleteAccountViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Settings", model: "delete-account");
        }
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["ErrorMessage"] = "Ocurrió un error al eliminar la cuenta. Por favor, inténtelo de nuevo.";
            return View("Settings", model: "delete-account");
        }
        catch (Exception)
        {
            ViewData["ErrorMessage"] = "Ocurrió un error al eliminar la cuenta. Por favor, inténtelo de nuevo.";
            return View("Settings", model: "delete-account");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}