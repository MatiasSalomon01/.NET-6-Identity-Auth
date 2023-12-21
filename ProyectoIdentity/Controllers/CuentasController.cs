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

    //Manejador de errores
    private void ValidarErrores(IdentityResult resultado)
    {
        foreach (var error in resultado.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
