using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_Gamer___BackEnd.Models
{
    public class Jogador
    {
        [Key]//Data Annotation - IdJogador
        public int IdJogador { get; set; }
        public string NomeJogador { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        //aqui é a FK onde irá herdar, inserir o using System.ComponentModel.DataAnnotations.Schema; e colocar o nome da classe que existe a chave primária, no caso aqui é a classe "Equipe" porque queremos herdar o IdEquipe desta lista.
        [ForeignKey("Equipe")]//inserido no formato de string pois toda classe é inserida como string
        public int IdEquipe { get; set; }
        //este objeto guarda as informações da Equipe para usar este objeto para ter acesso as Equipes aqui na classe Jogadores.
        public Equipe Equipe { get; set; }

    }
}