using Microsoft.AspNetCore.Mvc;
using Projeto_Gamer___BackEnd.Infra;
using Projeto_Gamer___BackEnd.Models;

namespace Projeto_Gamer___BackEnd.Controllers
{
    [Route("[controller]")]
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }
        //instanciando o objeto do banco de dados, para utilizar o banco de dados.
        
        //TODO Método Listar
        Context c = new Context();
        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.Jogador = c.Jogador.ToList();
            ViewBag.Equipe = c.Equipe.ToList();

            return View();
        }

        //TODO Método Cadastrar
        [Route("Cadastrar")]

        public IActionResult Cadastrar (IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.IdJogador = 0;
            novoJogador.NomeJogador = Console.ReadLine();
            novoJogador.Email = Console.ReadLine();
            novoJogador.Senha = Console.ReadLine();
            
            c.Jogador.Add(novoJogador);
            c.SaveChanges();
            return LocalRedirect("~/Equipe/Listar");
            
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}