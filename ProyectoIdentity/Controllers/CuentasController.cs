using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoIdentity.Models;

namespace ProyectoIdentity.Controllers;

public class CuentasController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public CuentasController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Registro(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        var registroVM = new RegistroViewModel();
        return View(registroVM);
    }

    [HttpPost]
    public async Task<IActionResult> Registro(RegistroViewModel model, string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        returnUrl = returnUrl ?? Url.Content("~/");

        if (ModelState.IsValid)
        {
            var usuario = new AppUsuario
            {
                UserName = model.Email,
                Email = model.Email,
                Nombre = model.Nombre,
                Url = model.Url,
                CodigoPais = model.CodigoPais,
                Telefono = model.Telefono,
                Pais = model.Pais,
                Ciudad = model.Ciudad,
                Direccion = model.Direccion,
                FechaNacimiento = model.FechaNacimiento,
                Estado = model.Estado,
            };

            var resultado = await _userManager.CreateAsync(usuario, model.Password);

            if (resultado.Succeeded)
            {
                await _signInManager.SignInAsync(usuario, isPersistent: false);
                return LocalRedirect(returnUrl);
            }

            ValidarErrores(resultado);
        }

        return View(model);
    }

    //Metodo para mostrar formulario de acceso
    [HttpGet]
    public IActionResult Acceso(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Acceso(AccesoViewModel model, string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        returnUrl = returnUrl ?? Url.Content("~/");

        if (ModelState.IsValid)
        {
            var resultado = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Acceso invalido");
                return View();
            }
        }

        return View(model);
    }

    //Salir o cerrar sesion de la aplicacion 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SalirAplicacion()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    //Manejador de errores
    private void ValidarErrores(IdentityResult resultado)
    {
        foreach (var error in resultado.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
