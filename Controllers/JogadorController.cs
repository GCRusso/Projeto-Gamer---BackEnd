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
            //*esta viewbag foi copiada e colada em todas VIEW INDEX E EDITAR em todos os controllers existentes, HOME, JOGADOR E EQUIPE
            //*Tudo que retornar uma VIEW tem que inserir este dado para mostrar para todas as VIEWS que o jogador esta logado.
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            ViewBag.Jogador = c.Jogador.ToList();
            ViewBag.Equipe = c.Equipe.ToList();

            return View();
        }

        //TODO Método Cadastrar
        [Route("Cadastrar")]

        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();
            //AQUI ESTAMOS INSERINDO OS DADOS MANUALMENTE PELO USUARIO COMO SE FOSSE UM "CONSOLE READ LINE"
            novoJogador.NomeJogador = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            c.Jogador.Add(novoJogador);
            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        //todo Método EXCLUIR
        //inserido o id para sinalizar que está utilizando um parâmetro para exclusão, por exemplo: http://localhost:5041/Equipe/Excluir/21 irá excluir o ID 21
        [Route("Excluir/{id}")]
        //Vamos apagar pelo ID
        public IActionResult Excluir(int id)
        {
            //adicionar em um objeto equipeBuscada, unir o c que é o nosso context(banco de dados) com a Equipe e insiro First porque é o primeiro objeto encontrado com este id, guarda no "e" verificamos se o "e" é igual a algum id no bancos de dados, se for igual guardar no "equipeBuscada"
            Jogador jogadorBuscado = c.Jogador.First(e => e.IdJogador == id);
            //remover a equipeBuscada
            c.Remove(jogadorBuscado);
            //salvar a lista
            c.SaveChanges();
            //este return faz a função de listar novamente a lista, mas já atualizada.
            return LocalRedirect("~/Jogador/Listar");
        }

        //todo Método EDITAR
        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            //*esta viewbag foi copiada e colada em todas VIEW INDEX E EDITAR em todos os controllers existentes, HOME, JOGADOR E EQUIPE
            //*Tudo que retornar uma VIEW tem que inserir este dado para mostrar para todas as VIEWS que o jogador esta logado.
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            Jogador jogador = c.Jogador.First(e => e.IdJogador == id);

            //guarda na mochila o nosso id procurado em equipe.
            ViewBag.Jogador = jogador;
            ViewBag.Equipe = c.Equipe.ToList();

            return View("Edit");
        }

        //todo Método ATUALIZAR
        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Jogador jogadorAtualizado = new Jogador();

            jogadorAtualizado.IdJogador = int.Parse(form["IdJogador"].ToString());

            jogadorAtualizado.NomeJogador = form["Nome"].ToString();
            jogadorAtualizado.Email = form["Email"].ToString();
            jogadorAtualizado.Senha = form["Senha"].ToString();

            Jogador jogadorBuscado = c.Jogador.First(x => x.IdJogador == jogadorAtualizado.IdJogador);


            jogadorBuscado.NomeJogador = jogadorAtualizado.NomeJogador;
            jogadorBuscado.Email = jogadorAtualizado.Email;
            jogadorBuscado.Senha = jogadorAtualizado.Senha;

            c.Jogador.Update(jogadorBuscado);
            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}