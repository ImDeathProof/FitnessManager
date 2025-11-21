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
using Microsoft.AspNetCore.Mvc.Rendering;

public class AccountController : Controller
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly IUsuarioService _usuarioService;

    public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IUsuarioService usuarioService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() => View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid){
            ModelState.AddModelError(string.Empty, "Complete todos los campos requeridos.");
            return View(model);
            }
        try
        {
            var status = await _usuarioService.GetUserStatusAsync(model.Username);
            if (!status)
            {
                ModelState.AddModelError(string.Empty, "La cuenta está desactivada. Por favor, contacte al soporte.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded) return RedirectToAction("Profile", "Account");
            
            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos");
            return View(model);
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocurrió un error al iniciar sesión. Por favor, inténtelo de nuevo más tarde.";
            return View(model);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register() => View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Por favor, complete todos los campos requeridos.");
            return View(model);
        }
        try
        {
            if(await _usuarioService.UsernameExistsAsync(model.Username))
            {
                ModelState.AddModelError(string.Empty, "El nombre de usuario ya está en uso. Por favor, elija otro.");
                return View(model);
            }
            var user = new Usuario
            {
                UserName = model.Username,
                Email = model.Email,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                IsActive = true,
                FechaRegistro = DateTime.UtcNow,
                FechaNacimiento = DateTime.UtcNow,
                Objetivo = "Objetivo no definido",
                AvatarUrl = "/avatars/Default.png"
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Profile", "Account");
            }
            TempData["ErrorMessage"] = "Ocurrió un error al registrar la cuenta. Por favor, inténtelo de nuevo.";
            return View(model);
        }catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocurrió un error al registrar la cuenta. Por favor, inténtelo de nuevo más tarde.";
            return View(model);
        }

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
            Edad = await _usuarioService.GetEdadAsync(user.Id),
            Peso = (float)(user.Peso ?? 0),
            Altura = (float)(user.Altura ?? 0),
            Objetivo = user.Objetivo
        };
        return View(model);
    }
    [Authorize]
    public async Task<IActionResult>  Settings(string view)
    {
        try{
            var user =  await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            TempData["Avatar"]= user.AvatarUrl;
            TempData["Username"]= user.UserName;
            if(string.IsNullOrEmpty(view)|| string.IsNullOrWhiteSpace(view))
            {
                view = "account-settings";
            }
            var model = await BuildSettingsViewModel(view);
            // var model = new SettingsViewModel
            // {
            //     SelectedOption = view,
            //     AccountSettings = new AccountSettingsViewModel()

            // };
            // model.AccountSettings.Nombre = user.Nombre;
            // model.AccountSettings.Apellido = user.Apellido;
            // model.AccountSettings.Username = user.UserName;
            // model.AccountSettings.Email = user.Email;
            // model.AccountSettings.Altura = (float)(user.Altura ?? 0);
            // model.AccountSettings.Peso = (float)(user.Peso ?? 0);
            // model.AccountSettings.Objetivo = user.Objetivo;
            // model.AccountSettings.FechaNacimiento = user.FechaNacimiento;

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Ocurrió un error al cargar la configuración. Por favor, inténtelo de nuevo más tarde.";
            Console.WriteLine("Error loading settings: " + ex.Message);
            return View();
        }
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Por favor, complete todos los campos requeridos.");
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
                TempData["SuccessMessage"] = "Contraseña cambiada exitosamente.";
                return RedirectToAction("Settings", new { view = "password-change" });
            }
            TempData["ErrorMessage"] = "Ocurrió un error al cambiar la contraseña. Por favor, inténtelo de nuevo.";
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
        ViewData["ErrorMessage"] = null;
        ViewData["successMessage"] = null;
        if (!ModelState.IsValid)
        {
            ViewData["ErrorMessage"] = "Todos los campos son obligatorios.";
            return View("Settings", model: "delete-account");
        }
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordCheck)
            {
                ViewData["ErrorMessage"] = "La contraseña es incorrecta.";
                return View("Settings", model: "delete-account");
            }

            await _usuarioService.DeleteUserAsync(user);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            Console.WriteLine("error: " + ex.Message);
            ViewData["ErrorMessage"] = "Ocurrió un error al eliminar la cuenta. Por favor, inténtelo de nuevo.";
            return View("Settings", model: "delete-account");
        }

    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AccountSettingsUpdate(AccountSettingsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Por favor, complete todos los campos requeridos.");
            var settingsModel = await BuildSettingsViewModel("account-settings", new SettingsViewModel
            {
                AccountSettings = model
            });
            return View("Settings", settingsModel);
        }
        if(await _usuarioService.UsernameExistsAsync(model.Username, _userManager.GetUserId(User)))
        {
            ModelState.AddModelError(string.Empty, "El nombre de usuario ya está en uso. Por favor, elija otro.");
            var settingsModel = await BuildSettingsViewModel("account-settings", new SettingsViewModel
            {
                AccountSettings = model
            });
            return View("Settings", settingsModel);
        }
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            user.UserName = model.Username;
            user.Nombre = model.Nombre;
            user.Apellido = model.Apellido;
            user.Email = model.Email;
            user.Altura = model.Altura;
            user.Peso = model.Peso;
            user.Objetivo = model.Objetivo;
            user.FechaNacimiento = model.FechaNacimiento;

            await _userManager.UpdateAsync(user);

            TempData["SuccessMessage"] = "Configuración de la cuenta actualizada exitosamente.";
            return RedirectToAction("Settings", new { view = "account-settings" });

        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocurrió un error al actualizar la configuración. Por favor, inténtelo de nuevo.";
            return RedirectToAction("Settings", new { view = "account-settings" });
        }
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> SelectAvatar(AvatarSelectorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Por favor, seleccione un avatar.");
            var settingsModel = await BuildSettingsViewModel("avatar-selector", new SettingsViewModel
            {
                AvatarSelector = model
            });
            return View("Settings", settingsModel);
        }
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            user.AvatarUrl = model.SelectedAvatarUrl;
            await _userManager.UpdateAsync(user);

            TempData["SuccessMessage"] = "Avatar actualizado exitosamente.";
            return RedirectToAction("Settings", new { view = "avatar-selector" });

        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocurrió un error al actualizar el avatar. Por favor, inténtelo de nuevo.";
            return RedirectToAction("Settings", new { view = "avatar-selector" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    private async Task<SettingsViewModel> BuildSettingsViewModel(string selectedOption, SettingsViewModel incoming = null)
    {
        try{
            var model = incoming ?? new SettingsViewModel();

            model.SelectedOption = selectedOption;

            model.AccountSettings ??= await _usuarioService.GetAccountSettingsAsync(_userManager.GetUserId(User));
            model.ChangePassword ??= new ChangePasswordViewModel();
            model.DeleteAccount ??= new DeleteAccountViewModel();
            model.AvatarSelector ??= await _usuarioService.GetAvatarSelectorAsync(_userManager.GetUserId(User));
            model.AvatarSelector.AvailableAvatarUrls = new List<string>
            {
                "/avatars/Default_1.png",
                "/avatars/Default_2.png",
                "/avatars/Default_3.png",
                "/avatars/Default_4.png",
                "/avatars/Default_5.png",
                "/avatars/Default_6.png",
                "/avatars/Default.png"
            };
            return model;
        }catch(Exception ex)
        {
            Console.WriteLine("Error building settings view model: " + ex.Message);
            throw;
        }
    }
}