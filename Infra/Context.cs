using Microsoft.EntityFrameworkCore;
using Projeto_Gamer___BackEnd.Models;

namespace Projeto_Gamer___BackEnd.Infra
{
    // herdar o DbContext e chamar o using using Microsoft.EntityFrameworkCore;
    public class Context : DbContext
    {
        public Context()
        {
        }
        public Context(DbContextOptions<Context> options) : base(options)
        {  
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         if (!optionsBuilder.IsConfigured)
         {
            //string de conexão com o banco, aqui no caso vamos utilizar o SQL SERVER, mas se fosse utilizar outro, tinha que chamar o método de acordo com o gerenciador que for utilizar e instalar os pacotes tambem de acordo com qual gerenciador for utilizar.
            //Data Source : O nome do gerenciador do banco de dados
            //initial catalog : nome do banco de dados
            optionsBuilder.UseSqlServer("Data Source = ; initial catalog = projetoGamer; Integrated Security = true; TrustServerCertificate = true;");

            //autenticação pelo windows
            //Integrated Security : Autenticação pelo Windows[
            //TrustServerCertificate : Autenticação pelo windows

            //Autenticação pelo SQLServer
            //user Id = "Nome do seu usuário de login"
            //password(PWD) = "senha do seu usuario"
         }  
        }
//Referência das tabelas, inserir todas as classes(tabelas) criar o DbSet de cada uma
        public DbSet<Jogador> Jogador {get; set;}
        public DbSet<Equipe> Equipe {get; set;}
    }
}