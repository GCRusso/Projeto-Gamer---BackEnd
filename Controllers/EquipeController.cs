using Microsoft.AspNetCore.Mvc;
using Projeto_Gamer___BackEnd.Infra;

namespace Projeto_Gamer___BackEnd.Controllers
{
    [Route("[controller]")]
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }
        //instanciar um novo objeto da classe Context para ter acesso ao BANCO DE DADOS e inserir o using para conseguir o acesso.
        Context c = new Context();
        [Route("Listar")]//isto é um rota chamada Listar funcionamento da rota-> http://localhost/Equipe/Listar
        public IActionResult Index()
        {
            //nome do objeto, chamar o que desejar listar e invocar o método ToList();
            ViewBag.Equipe = c.Equipe.ToList(); //em uma mala de equipe = adiciona as equipes, armazena as equipes
            return View(); //encaminha para a view de equipe
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}