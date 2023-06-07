using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Gamer___BackEnd.Models;

namespace Projeto_Gamer___BackEnd.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
            //*esta viewbag foi copiada e colada em todas VIEW INDEX E EDITAR em todos os controllers existentes, HOME, JOGADOR E EQUIPE
            //*Tudo que retornar uma VIEW tem que inserir este dado para mostrar para todas as VIEWS que o jogador esta logado.
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
