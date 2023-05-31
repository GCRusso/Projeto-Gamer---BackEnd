using Microsoft.AspNetCore.Mvc;
using Projeto_Gamer___BackEnd.Infra;
using Projeto_Gamer___BackEnd.Models;

//! ************FOI CRIADO UM NEW C# CONTROLLER***********

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

        //TODO Método LISTAR
        //instanciar um novo objeto da classe Context para ter acesso ao BANCO DE DADOS e inserir o using para conseguir o acesso.
        Context c = new Context();
        [Route("Listar")]//isto é um rota chamada Listar funcionamento da rota-> http://localhost/Equipe/Listar
        public IActionResult Index()
        {
            //nome do objeto, chamar o que desejar listar e invocar o método ToList();
            ViewBag.Equipe = c.Equipe.ToList(); //em uma mala de equipe = adiciona as equipes, armazena as equipes
            return View(); //encaminha para a view de equipe
        }



        //TODO Método CADASTRAR
        //form = formulário
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //instanciar o objeto para enxergar a classe e adicionar o USING
            Equipe novaEquipe = new Equipe();

            //pegamos o campo Nome e alteramos com o form e transformou ele em string, pq o form não é uma string.
            novaEquipe.NomeEquipe = form["Nome"].ToString();
            novaEquipe.Imagem = form["Imagem"].ToString();

            //puxar o context que é o nosso banco de dados, e inserir o nosso objeto que desejamos colocar no banco de dados
            c.Equipe.Add(novaEquipe);

            //Salvar a lista no banco de dados depois que é adicionado um objeto novo
            c.SaveChanges();

            //Return LocalRedirect = ira redirecionar para um local, quando finalizar a aba cadastrar ele ira voltar para a mesma pagina porem atualizada com o novo objeto adicionado.
            // ~ = LOCALHOST
            return LocalRedirect("~/Equipe/Listar");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}