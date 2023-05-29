using System.ComponentModel.DataAnnotations;
namespace Projeto_Gamer___BackEnd.Models
{
    public class Equipe
    {
        //chave primária(PK Primary Key) e foi inserido o using System.ComponentModel.DataAnnotations; 
        [Key] //Data annotation - IdEquipe
        public int IdEquipe { get; set; }
        [Required] //utilizado quando o item é obrigatório, então não é possível fazer um cadastro isoladamente, apenas todos juntos
        public string NomeEquipe { get; set; }
        public string Imagem { get; set; }

        //ICollection é uma referência que a classe Equipe irá ter acesso a ICollection(classe) "Jogador" para conseguir pegar o dado necessesário que é o IdEquipe
        public ICollection<Jogador> Jogador { get; set; }

    }
}