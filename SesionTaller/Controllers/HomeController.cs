using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SesionTaller.Models;

namespace SesionTaller.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        return View("Index");
    }

    [HttpGet]
    [Route("privacy")]
    public IActionResult Privacy()
    {
        return View("Privacy");
    }

    [HttpPost]
    public IActionResult Operacion(string operacion)
    {
        int valor = GetSessionValue();

        switch (operacion)
        {
            case "+":
                valor++;
                break;
            case "-":
                valor--;
                break;
            case "x2":
                valor *= 2;
                break;
            case "Random":
                Random rnd = new Random();
                valor += rnd.Next(1, 11);
                break;
        }

        SetSessionValue(valor);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult GuardarNombre(string nombre)
    {
        HttpContext.Session.SetString("Nombre", nombre);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Inicio");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
