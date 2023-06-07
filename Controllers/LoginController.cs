using Microsoft.AspNetCore.Mvc;
using Projeto_Gamer___BackEnd.Infra;
using Projeto_Gamer___BackEnd.Models;

namespace Projeto_Gamer___BackEnd.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        //todo // INDEX 
        [Route("Login")]
        public IActionResult Index()
        {
            HttpContext.Session.GetString("UserName");
            return View();
        }

        //todo // Método LOGAR
        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
            //Recebendo os dados do usuario para efetuar o login   
            string email = form["Email"].ToString();
            string senha = form["Senha"].ToString();

            //se o objeto buscado "J" for igual ao o objeto cadastrado, para ver se existe um usuario cadastrado com estes dados
            Jogador jogadorBuscado = c.Jogador.First(j => j.Email == email && j.Senha == senha);

            //! Aqui precisamos implementar a sessão // FOI IMPLEMENTADO PARTE DA DOCUMENTAÇÃO NA PROGRAM MAIS INFORMAÇÕES NELA.
            //se for diferente de nulo
            if (jogadorBuscado != null)
            {
                //username = pode ser qualquer nome, mas foi escolhido username. foi inserido o nomeJogador dentro do UserName
                @HttpContext.Session.SetString("UserName",jogadorBuscado.NomeJogador);
                return LocalRedirect("~/");
            }

            return LocalRedirect("~/Login/Login/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}