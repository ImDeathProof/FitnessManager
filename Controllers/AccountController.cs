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

public class AccountController : Controller
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;

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

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

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
            IsActive = true
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

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}