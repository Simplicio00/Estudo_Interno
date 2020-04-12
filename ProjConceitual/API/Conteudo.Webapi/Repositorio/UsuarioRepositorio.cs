using Conteudo.Webapi.Interfaces;
using Conteudo.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Conteudo.Webapi.Repositorio
{
	public class UsuarioRepositorio : IUsuario
	{

        public List<Usuario> Get()
		{
            try
            {
                //Faz uma lista vazia de usuários
                List<Usuario> listaDeUsuarios = new List<Usuario>();

                //Estabelece uma conexão, puxando a string de conexão manual
                SqlConnection connect = new SqlConnection("Data Source=LUCASSOLIVEIRA\\SQLEXPRESS; " +
                    "initial catalog=BancoUConteudo; integrated security=true;");

                //Abre a conexão com o banco de dados
                connect.Open();

                //Faz o comando interno do banco de dados para a busca de informação
                var pergunta = "select IdUsuario, Nome, Email, Senha from Usuario";

                //Estabelece um comando autenticado para o banco de dados 
                SqlCommand sqlCommand = new SqlCommand(pergunta, connect);

                //Faz um leitor de dados para o banco
                SqlDataReader leitor;

                //Executa a leitura dos dados executando o comando.
                leitor = sqlCommand.ExecuteReader();

                //Enquanto o leitor ainda estiver informação para ler...
                while (leitor.Read())
                {
                    //Um novo usuário é buscado
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(leitor[0]),
                        Nome = Convert.ToString(leitor[1]),
                        Email = Convert.ToString(leitor[2]),
                        Senha = Convert.ToString(leitor[3])
                    };
                    //E é adicionado na lista de usuários
                    listaDeUsuarios.Add(usuario);
                }
                //Após a busca, a informação é armazenada e a conexão é fechada. 
                connect.Close();
                //A lista de usuários é então retornada com a informação requerida
                return listaDeUsuarios;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public void Post(Usuario usuario)
        {
            //Comando para inserir dados na tabela.
            string post = $"insert into Usuario(Nome,Email,Senha)values('{usuario.Nome}','{usuario.Email}','{usuario.Senha}');";

            //Estabelece uma conexão, puxando a string de conexão manual
            SqlConnection connection = new SqlConnection("Data Source=LUCASSOLIVEIRA\\SQLEXPRESS;" +
                " initial catalog=BancoUConteudo; integrated security=true;");

            //Cria um comando, e puxa a conexão para autenticar o comando
            SqlCommand command = new SqlCommand(post,connection);

            //Abre a conexão
            connection.Open();

            //executa o comando da string POST no banco de dados
            command.ExecuteNonQuery();

            //Fecha a conexão após a inserção dos dados
            connection.Close();
        }
    }
}
