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

                //Ativa a leitura dos dados executando o comando.
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


        public void Update(int id, Usuario usuario)
        {
            //Estabelece uma conexão, puxando a string de conexão manual
            SqlConnection connection = new SqlConnection("Data Source=LUCASSOLIVEIRA\\SQLEXPRESS;" +
                " initial catalog=BancoUConteudo; integrated security=true;");

            //Comando interno para atualizar somente o nome
            string updateNome = $"update Usuario set Nome = '{usuario.Nome}' where IdUsuario={id}";

            //Comando interno para atualizar somente a senha
            string updateSenha = $"update Usuario set Senha = '{usuario.Senha}' where IdUsuario={id}";

            //Comando interno para atualizar o Nome e a Senha
            string update = $"update Usuario set Nome = '{usuario.Nome}', Senha = '{usuario.Senha}' where IdUsuario={id}";

            //validação
            if (usuario.Nome != null && usuario.Senha != null)
            {
                //Cria um comando autenticado
                SqlCommand command = new SqlCommand(update, connection);
                //Abre a conexão com o banco de dados
                connection.Open();
                //Executa a atualização dos dados
                command.ExecuteNonQuery();
                //Fecha a conexão após o término da execução
                connection.Close();
            }
            else if (usuario.Nome != null)
            {
                //Cria um comando autenticado
                SqlCommand command = new SqlCommand(updateNome, connection);
                //Abre a conexão com o banco de dados
                connection.Open();
                //Executa a atualização dos dados
                command.ExecuteNonQuery();
                //Fecha a conexão após o término da execução
                connection.Close();
            }
            else if (usuario.Senha != null)
            {
                //Cria um comando autenticado
                SqlCommand command = new SqlCommand(updateSenha, connection);
                //Abre a conexão com o banco de dados
                connection.Open();
                //Executa a atualização dos dados
                command.ExecuteNonQuery();
                //Fecha a conexão após o término da execução
                connection.Close();
            }
        }



        public Usuario GetById(int id)
        {
            try
            {
                //Faz a instância de um usuário vazio
                Usuario usuarioBuscado = new Usuario();

                //Estabelece uma conexão, puxando a string de conexão manual
                SqlConnection connection = new SqlConnection("Data Source=LUCASSOLIVEIRA\\SQLEXPRESS;" +
                    " initial catalog=BancoUConteudo; integrated security=true;");

                //Busca dados na tabela pelo id de um elemento
                string buscaIndividual = $"select * from Usuario where IdUsuario = {id}";

                //Cria um comando autenticado para conexão com o banco
                SqlCommand command = new SqlCommand(buscaIndividual,connection);

                //Abre a conexão com o banco
                connection.Open();

                //Faz o leitor de dados do banco
                SqlDataReader leitor;

                //Ativa a leitura dos dados executando o comando
                leitor = command.ExecuteReader();

                //Se o leitor tiver informação para ler...
                if (leitor.Read())
                {
                    //Um usuário é buscado pelo seu id
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(leitor[0]),
                        Nome = Convert.ToString(leitor[1]),
                        Email = Convert.ToString(leitor[2]),
                        Senha = Convert.ToString(leitor[3])
                    };
                    //O usuário vazio recebe um usuário válido
                    usuarioBuscado = usuario;
                }

                //Validação para saber se o usuário existe
                if (usuarioBuscado.Nome == null && usuarioBuscado.Email == null && usuarioBuscado.Senha == null )
                {
                    usuarioBuscado = null;
                }

                //Fecha a conexão com o banco de dados e armazena o usuário.
                connection.Close();

                //Retorna o usuário armazenado para o controlador
                return usuarioBuscado;

            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
