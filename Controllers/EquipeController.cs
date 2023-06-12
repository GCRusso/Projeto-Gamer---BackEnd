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
            //*esta viewbag foi copiada e colada em todas VIEW INDEX E EDITAR em todos os controllers existentes, HOME, JOGADOR E EQUIPE
            //*Tudo que retornar uma VIEW tem que inserir este dado para mostrar para todas as VIEWS que o jogador esta logado.
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            //nome do objeto, chamar o que desejar listar e invocar o método ToList();
            ViewBag.Equipe = c.Equipe.ToList(); //em uma mala de equipe = adiciona as equipes, armazena as equipes
            return View(); //encaminha para a view de equipe
        }
        //todo // ------------------------------------------------------------------------------------------------




        //TODO Método CADASTRAR
        //form = formulário
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //instanciar o objeto para enxergar a classe e adicionar o USING
            Equipe novaEquipe = new Equipe();

            //pegamos o campo Nome e alteramos com o form e transformou ele em string, pq o form não é uma string.
            novaEquipe.NomeEquipe = form["Nome"].ToString();

            //novaEquipe.Imagem = form["Imagem"].ToString(); // aqui estava chegando a imagem como STRING não queremos assim


            //TODO // Inicio da lógica de UPLOAD da imagem.
            //Se existir uma imagem dentro do FORM ele cadastra dentro da variável FILE no array de número 0
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                //Path.Combine se caso tiver um caminho anterior ele ignora e só visualiza dali pra frente, cria um caminho para criar uma pasta, ele da o ponta pé de inicio do caminho que você deseja
                //GetCurrentDirectory = pega a pasta corrente que é a pasta principal do arquivo que no caso a nossa é PROJETO GAMER - BACKEND
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");
                //se a pasta FOLDER nao for criada, cria uma nova pasta chamada FOLDER
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //gera o caminho completo até o caminho do arquivo(imagem - com extensão) abre a pasta FOLDER(Equipes) e vai até o seu arquivo(FILE) carregado no seu interior.
                var path = Path.Combine(folder, file.FileName);

                //pegou o objeto que foi criado(cadastro) e jogou na variavel STREAM e copiou para ser salvo
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;
            }


            //se caso nao for carregado uma imagem, carregar uma imagem padrão para não ser barrado o cadastro.
            else
            {
                //inserir uma imagem na pasta e colocar com o mesmo nome daqui e formato tambem!
                novaEquipe.Imagem = "padrao.png";
            }
            //TODO // FIM DA LÓGICA PARA UPLOAD DE IMAGEM



            //puxar o context que é o nosso banco de dados, e inserir o nosso objeto que desejamos colocar no banco de dados
            c.Equipe.Add(novaEquipe);

            //Salvar a lista no banco de dados depois que é adicionado um objeto novo
            c.SaveChanges();

            //Return LocalRedirect = ira redirecionar para um local, quando finalizar a aba cadastrar ele ira voltar para a mesma pagina porem atualizada com o novo objeto adicionado.
            // ~ = LOCALHOST
            return LocalRedirect("~/Equipe/Listar");
        }
        //todo // ------------------------------------------------------------------------------------------------




        //todo Método EXCLUIR
        //inserido o id para sinalizar que está utilizando um parâmetro para exclusão, por exemplo: http://localhost:5041/Equipe/Excluir/21 irá excluir o ID 21
        [Route("Excluir/{id}")]
        //Vamos apagar pelo ID
        public IActionResult Excluir(int id)
        {
            //adicionar em um objeto equipeBuscada, unir o c que é o nosso context(banco de dados) com a Equipe e insiro First porque é o primeiro objeto encontrado com este id, guarda no "e" verificamos se o "e" é igual a algum id no bancos de dados, se for igual guardar no "equipeBuscada"
            Equipe equipeBuscada = c.Equipe.First(e => e.IdEquipe == id);
            //remover a equipeBuscada
            c.Remove(equipeBuscada);
            //salvar a lista
            c.SaveChanges();
            //este return faz a função de listar novamente a lista, mas já atualizada.
            return LocalRedirect("~/Equipe/Listar");
        }
        //todo // ------------------------------------------------------------------------------------------------




        //todo Método EDITAR
        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            //*esta viewbag foi copiada e colada em todas VIEW INDEX E EDITAR em todos os controllers existentes, HOME, JOGADOR E EQUIPE
            //*Tudo que retornar uma VIEW tem que inserir este dado para mostrar para todas as VIEWS que o jogador esta logado.
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            Equipe equipe = c.Equipe.First(e => e.IdEquipe == id);

            //guarda na mochila o nosso id procurado em equipe.
            ViewBag.Equipe = equipe;

            return View("Edit");
        }
        //todo // ------------------------------------------------------------------------------------------------




        //todo Método ATUALIZAR
        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();

            novaEquipe.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            novaEquipe.NomeEquipe = form["Nome"].ToString();

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                novaEquipe.Imagem = file.FileName;
            }

            else
            {
                novaEquipe.Imagem = "padrao.png";
            }

            Equipe equipeBuscada = c.Equipe.First(x => x.IdEquipe == novaEquipe.IdEquipe);
            equipeBuscada.NomeEquipe = novaEquipe.NomeEquipe;
            equipeBuscada.Imagem = novaEquipe.Imagem;
            c.Equipe.Update(equipeBuscada);
            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
    //todo // ------------------------------------------------------------------------------------------------



    
}