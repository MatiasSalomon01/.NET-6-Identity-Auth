using Microsoft.AspNetCore.Mvc;
using ProyectoIdentity.Models;

namespace ProyectoIdentity.Controllers;

public class CuentasController : Controller
{
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
}
