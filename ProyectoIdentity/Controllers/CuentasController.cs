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
    public async Task<IActionResult> Registro()
    {
        var registroVM = new RegistroViewModel();
        return View(registroVM);
    }

    [HttpPost]
    public async Task<IActionResult> Registro(RegistroViewModel model)
    {
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
                return RedirectToAction("Index", "Home");
            }

            ValidarErrores(resultado);
        }

        return View(model);
    }

    //Metodo para mostrar formulario de acceso
    [HttpGet]
    public IActionResult Acceso()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Acceso(AccesoViewModel model)
    {
        if (ModelState.IsValid)
        {
            var resultado = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Acceso invalido");
                return View();
            }
        }

        return View(model);
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
