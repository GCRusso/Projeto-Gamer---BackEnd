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
            //Data Source : O nome do gerenciador do banco de dados // NOME DO SERVIDOR COPIAR E COLAR O QUE ESTA LÁ no caso aqui é NOTE04-S15
            //initial catalog : nome do banco de dados
            /*optionsBuilder.UseSqlServer("Data Source = NOTE04-S15 ; Initial Catalog = gamerManha; Integrated Security = true; TrustServerCertificate = true;");*/
            optionsBuilder.UseSqlServer("Data Source = NOTE04-S15 ; Initial Catalog = gamerManha; User Id = sa; pwd = Senai@134; TrustServerCertificate = true");

            //autenticação pelo windows
            //Integrated Security : Autenticação pelo Windows
            //TrustServerCertificate : Autenticação pelo windows
            //Integrated Security = true

            //Autenticação pelo SQLServer
            //user Id = "Nome do seu usuário de login"
            //password(PWD) = "senha do seu usuario"
            //nao possui o Integrated Security e inserir User Id = sa; pwd = Senai@134; (no caso é o usuario e senha da autenticação do SQL)
         }  
        }
//Referência das tabelas, inserir todas as classes(tabelas) criar o DbSet de cada uma
        public DbSet<Jogador> Jogador {get; set;}
        public DbSet<Equipe> Equipe {get; set;}
    }
}